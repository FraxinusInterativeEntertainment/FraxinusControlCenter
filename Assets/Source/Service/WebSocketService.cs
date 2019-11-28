using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebSocketService
{
    private WebSocket m_ws;

    public WebSocketService()
    {

    }

    public void Connect(string _url, System.Action<string> WebSocketMessageHandler, System.Action<string> WebSocketCloseHandler = null, System.Action WebSocketOpenHandler = null, System.Action<string> WebSocketErrorHandler = null)
    {
        m_ws = new WebSocket(_url);

        m_ws.OnOpen += (sender, e) => WebSocketOpenHandler();
        m_ws.OnError += (sender, e) => WebSocketErrorHandler("Error: " + e.Message + e.Exception.ToString());
        m_ws.OnClose += (sender, e) => WebSocketCloseHandler(e.Reason);
        m_ws.OnMessage += (sender, e) => WebSocketMessageHandler(e.Data);
        m_ws.ConnectAsync();
    }

    public void Send(string _message)
    {
        m_ws.Send(_message);
        Debug.Log("Sent: " + _message);
    }
}
