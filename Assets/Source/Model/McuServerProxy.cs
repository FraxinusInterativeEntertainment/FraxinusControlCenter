using System.Collections;
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
        m_MicroControllerService = new TcpServerService(IPAddress.Parse("192.168.0.103"), Const.NetworkRelated.TCP_SOCKET_PORT, Const.NetworkRelated.MAX_TCP_CONNECTIONS);

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
        try
        {
            McuMessage mcuMsg = JsonConvert.DeserializeObject<McuMessage>(_msg);

            switch (mcuMsg.MsgType)
            {
                case "Heart":
                    McuHeartbeatHandler(_client, _msg);
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
                Debug.Log(kvp.Key + " disconnected.");
                break;
            }
        }
    }

    public void SendAreYouOK(string _mcuID)
    {
        m_MicroControllerService.SendMessage(McuServerData().connectedMcus[_mcuID].client, "Are you OK?");
    }

    private void McuHeartbeatHandler(TcpClient _client, string _msg)
    {
        McuHeartbeatMsg hbMsg = JsonConvert.DeserializeObject<McuHeartbeatMsg>(_msg);
        string mcuID = hbMsg.MsgContent.Mcuid;
        //Debug.Log("new heartbeat: " + mcuID);
        if (!McuServerData().connectedMcus.ContainsKey(mcuID))
        {
            McuServerData().connectedMcus.Add(mcuID, new McuClient(mcuID, _client));
        }
        if (McuServerData().connectedMcus[mcuID].disconnected)
        {
            McuServerData().connectedMcus[mcuID].disconnected = false;
           
            Debug.Log(mcuID + " Connected!");
            Debug.Log("Connected MCU: " + McuServerData().connectedMcus.Count);
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

        Debug.Log(_mcuID + "UnsendMsg Count: " + m_unsendMsgQueues[_mcuID].Count);
    }

    private void TryClearUnsendMsgQueue(string _mcuID)
    {
        if (m_unsendMsgQueues.ContainsKey(_mcuID) && m_unsendMsgQueues[_mcuID] != null && m_unsendMsgQueues[_mcuID].Count > 0)
        {
            int msgCount = m_unsendMsgQueues[_mcuID].Count;
            for (int i = 0; i < msgCount; i++)
            {
                SendNotification(Const.Notification.TRY_SEND_MCU_MSG, new McuMsg(_mcuID, m_unsendMsgQueues[_mcuID].Dequeue()));
                //SendMsg(_mcuID, m_unsendMsgQueues[_mcuID].Dequeue());
            }
        }
    }

    private void TrySendMsg(McuMsg _msg)
    {
        SendNotification(Const.Notification.TRY_SEND_MCU_MSG, _msg);
    }

    public void SendMsg(string _mcuID, string _msg)
    {
        m_MicroControllerService.SendMessage(McuServerData().connectedMcus[_mcuID].client, _msg);
    }

    private void CheckHeartbeatRecord()
    {
        //Debug.Log("Check heart beat");
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