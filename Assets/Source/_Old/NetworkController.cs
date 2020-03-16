using System;
using System.Text;
using System.Threading;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{
    private TCPServerService m_tcpServerSocket;
    private WebSocket m_ws;

    public static NetworkController Instance;

    private void Start()
    {
        Instance = this;
       // m_tcpServerSocket = new TCPServer(Const.URL.TCP_SOCKET_IP, Const.NetworkRelated.TCP_SOCKET_PORT, Const.NetworkRelated.MAX_TCP_CONNECTIONS);
        //m_tcpServerSocket.InitSocket();
    }

    public void ConnectWebSocket(string _token)
    {
        m_ws = new WebSocket("ws://www.fraxinusmothership.cn/ws/player/" + "?token=" + _token);

        m_ws.OnOpen += (sender, e) => WebSocketOpenHandler();
        m_ws.OnError += (sender, e) => WebSocketCloseHandler("Error: " + e.Message + e.Exception.ToString());
        m_ws.OnClose += (sender, e) => WebSocketCloseHandler(e.Reason);
        m_ws.OnMessage += (sender, e) => WebSocketMessageHandler(e.Data);
        m_ws.ConnectAsync();
    }

    private void OnCompleted(bool completed)
    {
        Debug.Log(completed);
    }

    private void WebSocketOpenHandler()
    {
        Debug.Log("Websocket Opened!");
    }

    private void WebSocketCloseHandler(string message)
    {
        Debug.Log("Websocket closed: " + message);
        AppFacade.instance.SendNotification(Const.Notification.DEBUG_LOG, message);
    }

    private void WebSocketMessageHandler(string message)
    {
        Debug.Log("Message Arrived: " + message);
        AppFacade.instance.SendNotification(Const.Notification.DEBUG_LOG, message);
    }

    private void SetupSocketServer()
    {

    }
}