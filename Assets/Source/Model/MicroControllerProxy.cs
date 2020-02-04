using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;

public class MicroControllerProxy : Proxy, IProxy
{
    public const string NAME = "MicroControllerProxy";

    private readonly Dictionary<string, MicroControllerVO> m_MicroId2MicroC = new Dictionary<string, MicroControllerVO>();
    private readonly Dictionary<string, string> m_moduleId2MicroId = new Dictionary<string, string>();
    private TCPServerService m_MicroControllerService;

    public MicroControllerProxy() : base(NAME)
    {
        m_MicroControllerService = new TCPServerService(Const.Url.TCP_SOCKET_IP, Const.NetworkRelated.TCP_SOCKET_PORT, Const.NetworkRelated.MAX_TCP_CONNECTIONS);
        StartMicroControllerService();
    }
    public void StartMicroControllerService()
    {
        m_MicroControllerService.InitSocket()
                          .AddMessageListener(OnMessageArrived)
                          .AddNewClientListener(OnNewClientConnected);
    }

    private void OnMessageArrived(Socket _client, string _msg)
    {
        Debug.Log(_client.RemoteEndPoint.ToString() + ": " + _msg);
    }

    private void OnNewClientConnected(Socket _client)
    {
        Debug.Log(_client.RemoteEndPoint.ToString());
    }
}
