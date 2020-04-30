using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System;

public class WebSocketService
{
    private event Action<string> OnException = delegate { };

    private WebSocket m_ws;

    public WebSocketService()
    {

    }

    public void Connect(string _url, string _host, System.Action<string> WebSocketMessageHandler, System.Action<string> WebSocketCloseHandler = null, System.Action WebSocketOpenHandler = null, System.Action<string> WebSocketErrorHandler = null)
    {
        m_ws = new WebSocket(_url);
        m_ws.Origin = _host;
        m_ws.OnOpen += (sender, e) => WebSocketOpenHandler();
        m_ws.OnError += (sender, e) => WebSocketErrorHandler("Error: " + e.Message + e.Exception.ToString());
        m_ws.OnClose += (sender, e) => WebSocketCloseHandler("Reason: " + e.Reason);
        m_ws.OnMessage += (sender, e) => WebSocketMessageHandler(e.Data);
        m_ws.ConnectAsync();
    }

    public void AddExceptionHandler(System.Action<string> _handler)
    {
        OnException += _handler;
    }

    public void p(string url)
    {

        m_ws.SetProxy(url, "", "");
    }
    public void Send(string _message)
    {
        if (m_ws == null)
        { 
            OnException("Websocket尚未初始化，请重新登录！");
            Debug.Log("Websocket not initialized yet, please login and retry sending!");
            return;
        }

        try
        {
            m_ws.Send(_message);
        }
        catch (System.Exception e)
        {
            OnException("WS Exception: " + e.Message);
        }
        
    }
}
