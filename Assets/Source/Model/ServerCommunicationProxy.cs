using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class ServerCommunicationProxy : Proxy, IProxy
{
    public const string NAME = "ServerCommunicationProxy";

    private WebSocketService m_wsService;

    public ServerCommunicationProxy() : base(NAME)
    {
        m_wsService = new WebSocketService();
    }

    public void ConnectFraxMotherShipWs(object _data)
    {
        m_wsService.Connect(Constants.Url.WEB_SOCKET_SERVER_ADDRESS + (string)_data, WebSocketMessageHandler, WebSocketCloseHandler, WebSocketOpenHandler, WebSocketErrorHandler);
    }

    public void SendMessage(object _data)
    {
        m_wsService.Send((string)_data);
    }

    private void WebSocketOpenHandler()
    {
        Debug.Log("Websocket Opened!");
    }

    private void WebSocketErrorHandler(string message)
    {
        Debug.Log("Websocket closed: " + message);
    }

    private void WebSocketCloseHandler(string message)
    {
        Debug.Log("Websocket closed: " + message);
    }

    private void WebSocketMessageHandler(string message)
    {
        Debug.Log("Message Arrived: " + message);
    }
}
