using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class MicroControllerServerDelegate
{
    private TCPServerService m_tcpServerService;

    public MicroControllerServerDelegate()
    {
        m_tcpServerService = new TCPServerService(Const.Url.TCP_SOCKET_IP, Const.NetworkRelated.TCP_SOCKET_PORT, Const.NetworkRelated.MAX_TCP_CONNECTIONS);
    }

    public void StartServer()
    {
        m_tcpServerService.InitSocket()
                          .AddMessageListener(OnMessageArrived)
                          .AddNewClientListener(OnNewClientConnected);
    }

    private void OnMessageArrived(Socket _client, string _msg)
    {
        Debug.Log(_msg);
    }

    private void OnNewClientConnected(Socket _client)
    {
        Debug.Log(_client.RemoteEndPoint.ToString());
    }
}
