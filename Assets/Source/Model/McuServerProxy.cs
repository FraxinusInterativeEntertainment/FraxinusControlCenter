﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;

public class McuServerProxy : Proxy, IProxy
{
    public const string NAME = "McuServerProxy";
    public const int MAX_UNSEND_QUEUE = 20;
    public const float CHECK_HEART_BEAT_INTERVAL = 5f;

    public object locker = 0;

    private TcpServerService m_MicroControllerService;
    private readonly Dictionary<string, Queue<string>> m_unsendMsgQueues = new Dictionary<string, Queue<string>>();

    public McuServerProxy() : base(NAME, new McuServerVO())
    {
        StartMicroControllerService();
    }
    public void StartMicroControllerService()
    {
        m_MicroControllerService = new TcpServerService(IPAddress.Any, Const.NetworkRelated.TCP_SOCKET_PORT, Const.NetworkRelated.MAX_TCP_CONNECTIONS);

        m_MicroControllerService.AddMessageListener(OnMessageArrived)
                                .AddNewClientListener(OnNewClientConnected)
                                .AddClientDisconnectedListener(OnClientDisconnected)
                                .AddSendMessageFailedListener(OnMsgSendFailed);

        m_MicroControllerService.StartListening();
        CheckHeartbeatRecord();
    }

    public TcpClient GetClientByID(string _id)
    {
        return McuServerData().connectedMcus[_id].client;
    }

    private McuServerVO McuServerData()
    {
        return m_data as McuServerVO;
    }

    private void OnMessageArrived(TcpClient _client, string _msg)
    {
        MainThreadCall.SafeCallback(() => MessageHandler(_client, _msg));

    }

    private void MessageHandler(TcpClient _client, string _msg)
    {
        Debug.Log(_msg);
        try
        {
            McuMessage mcuMsg = JsonConvert.DeserializeObject<McuMessage>(_msg);

            switch (mcuMsg.MsgType)
            {
                case "Heart":
                    McuHeartbeatHandler(_client, _msg);
                    break;
                case "Sensor":
                    SensorMsgHandler(_msg);
                    break;
                case "Signal":

                    break;
            }
        }
        catch(JsonReaderException exception)
        {
            Debug.Log(exception);
        }
    }

    public void OnClientDisconnected(TcpClient _client)
    {
        foreach (KeyValuePair<string, McuClient> kvp in McuServerData().connectedMcus)
        {
            if (kvp.Value.client == _client)
            {
                kvp.Value.isAlive = false;
                kvp.Value.disconnected = true;
                SendNotification(Const.Notification.UPDATE_MCU_STATUS, new McuVO(kvp.Key, McuStatus.Disconnnected));
                SendNotification(Const.Notification.DEBUG_LOG, kvp.Key + " disconnected.");
                break;
            }
        }
    }

    public void SendAreYouOK(string _mcuID)
    {
        m_MicroControllerService.SendMessage(McuServerData().connectedMcus[_mcuID].client, "Are you OK?");
    }

    private void SensorMsgHandler(string _msg)
    {
        McuConditionMsg conditionMsg = JsonConvert.DeserializeObject<McuConditionMsg>(_msg);
        SendNotification(Const.Notification.WS_SEND, new SensorMessage(conditionMsg.MsgContent.moduleName, 
                                                                       conditionMsg.MsgContent.value));
    }

    private void McuHeartbeatHandler(TcpClient _client, string _msg)
    {
        McuHeartbeatMsg hbMsg = JsonConvert.DeserializeObject<McuHeartbeatMsg>(_msg);
        string mcuID = hbMsg.MsgContent.Mcuid;

        if (!McuServerData().connectedMcus.ContainsKey(mcuID))
        {
            McuServerData().connectedMcus.Add(mcuID, new McuClient(mcuID, _client));
        }

        if (McuServerData().connectedMcus[mcuID].disconnected)
        {
            McuServerData().connectedMcus[mcuID].disconnected = false;
           
            SendNotification(Const.Notification.DEBUG_LOG, mcuID + " connected!");
            SendNotification(Const.Notification.DEBUG_LOG, "已连接单片机数量: " + McuServerData().connectedMcus.Count);
        }

        McuServerData().connectedMcus[mcuID].isAlive = true;
        McuServerData().connectedMcus[mcuID].client = _client;
        SendNotification(Const.Notification.UPDATE_MCU_STATUS, new McuVO(mcuID, McuStatus.Connected));

        TryClearUnsendMsgQueue(mcuID);
    }

    private void OnNewClientConnected(TcpClient _client)
    {
        Debug.Log(_client.ToString());
    }

    private void OnMsgSendFailed(TcpClient _client, string _msg)
    {
        string mcuID = GetIdByClient(_client);

        UnsendMsgHandler(mcuID, _msg);
    }

    public void UnsendMsgHandler(string _mcuID, string _msg)
    {
        if (!m_unsendMsgQueues.ContainsKey(_mcuID))
        {
            m_unsendMsgQueues.Add(_mcuID, new Queue<string>());
        }

        if (m_unsendMsgQueues[_mcuID].Count >= MAX_UNSEND_QUEUE)
        {
            m_unsendMsgQueues[_mcuID].Dequeue();
        }

        m_unsendMsgQueues[_mcuID].Enqueue(_msg);

        SendNotification(Const.Notification.DEBUG_LOG, "发送至单片机 " + _mcuID + " 失败。失败信息 queue 数量：" + m_unsendMsgQueues[_mcuID].Count);
    }

    private void TryClearUnsendMsgQueue(string _mcuID)
    {
        if (m_unsendMsgQueues.ContainsKey(_mcuID) && m_unsendMsgQueues[_mcuID] != null && m_unsendMsgQueues[_mcuID].Count > 0)
        {
            int msgCount = m_unsendMsgQueues[_mcuID].Count;
            for (int i = 0; i < msgCount; i++)
            {
                SendNotification(Const.Notification.TRY_SEND_MCU_MSG, new McuMsgQueueItem(_mcuID, m_unsendMsgQueues[_mcuID].Dequeue()));
            }
        }
    }

    private void TrySendMsg(McuMsgQueueItem _msg)
    {
        SendNotification(Const.Notification.TRY_SEND_MCU_MSG, _msg);
    }

    public void SendMsg(string _mcuID, string _msg)
    {
        m_MicroControllerService.SendMessage(McuServerData().connectedMcus[_mcuID].client, _msg);
    }

    private void CheckHeartbeatRecord()
    {
        foreach (KeyValuePair<string, McuClient> kvp in McuServerData().connectedMcus)
        {
            if (!kvp.Value.isAlive)
            {

                SendNotification(Const.Notification.TRY_CONFIRM_MCU_DISCONNECTED, kvp.Key);
            }
            kvp.Value.isAlive = false;
        }
        Timer.Instance.AddTimerTask(CHECK_HEART_BEAT_INTERVAL, CheckHeartbeatRecord);
    }

    private string GetIdByClient(TcpClient _client)
    {
        foreach (KeyValuePair<string, McuClient> kvp in McuServerData().connectedMcus)
        {
            if (kvp.Value.client == _client)
            {
                return kvp.Key;
            }
        }
        return null;
    }
}