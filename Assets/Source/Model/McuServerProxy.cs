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

    public object locker = 0;

    private TcpServerService m_MicroControllerService;

    public McuServerProxy() : base(NAME, new McuServerVO())
    {
         StartMicroControllerService();
    }
    public void StartMicroControllerService()
    {
        m_MicroControllerService = new TcpServerService(Const.Url.TCP_SOCKET_IP, Const.NetworkRelated.TCP_SOCKET_PORT, Const.NetworkRelated.MAX_TCP_CONNECTIONS);

        m_MicroControllerService.AddMessageListener(OnMessageArrived)
                                .AddNewClientListener(OnNewClientConnected)
                                .AddClientDisconnectedListener(OnClientDisconnected);

        m_MicroControllerService.StartListening();
        CheckHeartbeatRecord();
    }

    private McuServerVO McuServerData()
    {
        return m_data as McuServerVO;
    }

    private void OnMessageArrived(TcpClient _client, string _msg)
    {
        //MainThreadCall.SafeCallback(() => MessageHandler(_client, _msg));

        MainThreadCall.SafeCallback(() => MessageHandler(_client, _msg));

    }

    private void MessageHandler(TcpClient _client, string _msg)
    {
        McuMessage mcuMsg = JsonConvert.DeserializeObject<McuMessage>(_msg);

        switch (mcuMsg.MsgType)
        {
            case "Heart":
                McuHeartbeatHandler(_client, _msg);
                break;
        }

    }

    private void OnClientDisconnected(TcpClient _client)
    {
        foreach (KeyValuePair<string, McuConnectionVO> kvp in McuServerData().connectedMcus)
        { 
            if (kvp.Value.client == _client)
            {
                kvp.Value.isAlive = false;
                SendNotification(Const.Notification.UPDATE_MCU_STATUS, new McuVO(kvp.Key, McuStatus.Disconnnected));
                Debug.Log(kvp.Key + " disconnected.");
                break;
            }
        }

    }

    private void McuHeartbeatHandler(TcpClient _client, string _msg)
    {


        McuHeartbeatMsg hbMsg = JsonConvert.DeserializeObject<McuHeartbeatMsg>(_msg);
        string mcuID = hbMsg.MsgContent.Mcuid;
        Debug.Log("new heartbeat: " + mcuID);
        if (!McuServerData().connectedMcus.ContainsKey(mcuID))
        {
            McuServerData().connectedMcus.Add(mcuID, new McuConnectionVO(_client, true));
        }
        else
        {
            McuServerData().connectedMcus[mcuID].isAlive = true;
            McuServerData().connectedMcus[mcuID].client = _client;
        }

        Debug.Log("Connected MCU: " + McuServerData().connectedMcus.Count);
        SendNotification(Const.Notification.UPDATE_MCU_STATUS, new McuVO(mcuID, McuStatus.Connected));
    }

    private void OnNewClientConnected(TcpClient _client)
    {
        Debug.Log(_client.ToString());
    }

    private void CheckHeartbeatRecord()
    {
        Debug.Log("Check heart beat");
        foreach (KeyValuePair<string, McuConnectionVO> kvp in McuServerData().connectedMcus)
        {
            if (!kvp.Value.isAlive)
            {
                SendNotification(Const.Notification.UPDATE_MCU_STATUS, new McuVO(kvp.Key, McuStatus.Unknown));
                m_MicroControllerService.SendMessage(kvp.Value.client, "Are you OK?");
            }
            kvp.Value.isAlive = false;
        }
        Timer.Instance.AddTimerTask(3.0f, CheckHeartbeatRecord);
    }
}
