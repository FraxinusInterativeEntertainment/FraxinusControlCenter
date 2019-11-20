using System;
using System.Text;
using System.Threading;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{
    private TCPServer m_tcpServerSocket;

    private void Start()
    {
        ConnectWebSocket();
        m_tcpServerSocket = new TCPServer(Constants.URL.TCP_SOCKET_IP, Constants.NetworkRelated.TCP_SOCKET_PORT, Constants.NetworkRelated.MAX_TCP_CONNECTIONS);
        m_tcpServerSocket.InitSocket();
    }

    private void ConnectWebSocket()
    {
        WebSocket ws = new WebSocket(Constants.URL.WEB_SOCKET_HOST);
        ws.OnMessage += (sender, e) => WebSocketMessageHandler(e.Data);
        ws.Connect();
    }

    private void WebSocketMessageHandler(string message)
    {
        Debug.Log("Message Arrived: " + message);
    }

    private void SetupSocketServer()
    {

    }
}