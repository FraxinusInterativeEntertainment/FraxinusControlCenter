using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Newtonsoft.Json;

public class ServerCommunicationProxy : Proxy, IProxy
{
    public const string NAME = "ServerCommunicationProxy";

    private WebSocketService m_wsService;
   
    public ServerCommunicationProxy() : base(NAME)
    {
        m_wsService = new WebSocketService();
        m_wsService.AddExceptionHandler(WebSocketExceptionHandler);
    }

    public void ConnectFraxMotherShipWs(object _data)
    {
        m_wsService.Connect(Const.Url.WEB_SOCKET_SERVER_ADDRESS + (_data as string) + "/", Const.Url.WEB_SOCKET_HOST_URI, WebSocketMessageHandler, 
                            WebSocketCloseHandler, WebSocketOpenHandler, WebSocketErrorHandler);
    }

    public void SendMessage(object _data)
    {
        string jsonData = JsonConvert.SerializeObject(_data);
        SendNotification(Const.Notification.DEBUG_LOG, "WS Send: " + jsonData);
        m_wsService.Send(jsonData);
    }

    private void WebSocketOpenHandler()
    {
        MainThreadCall.SafeCallback(() => {
            SendNotification(Const.Notification.DEBUG_LOG, "WS Server Opened!");
        });
    }

    private void WebSocketErrorHandler(string _message)
    {
        MainThreadCall.SafeCallback(() => {
            SendNotification(Const.Notification.CUSTOMIZED_POPUP,
                              new PopupInfoVO("Websocket错误", _message, "确认", true, () => { SendNotification(Const.Notification.SEND_LOGOUT); }));
        });
    }

    private void WebSocketCloseHandler(string _message)
    {
        MainThreadCall.SafeCallback(() => {
            SendNotification(Const.Notification.CUSTOMIZED_POPUP,
                              new PopupInfoVO("Websocket关闭", _message, "确认", true, () => { SendNotification(Const.Notification.SEND_LOGOUT); }));
        });
    }

    private void WebSocketMessageHandler(string _message)
    {

        MainThreadCall.SafeCallback(() => { 
            SendNotification(Const.Notification.SERVER_MSG_ARRIVED, _message);
            SendNotification(Const.Notification.DEBUG_LOG, "WS Server Message: " + _message);
        });
    }

    private void WebSocketExceptionHandler(string _msg)
    {
        MainThreadCall.SafeCallback(() =>
        {
            SendNotification(Const.Notification.GAME_CLOSED);
            SendNotification(Const.Notification.CUSTOMIZED_POPUP,
                             new PopupInfoVO("Websocket异常", _msg, "确认", true, () => { SendNotification(Const.Notification.SEND_LOGOUT); }));
        });
    }

    private string ToJson(object _data)
    {
        string json = JsonConvert.SerializeObject(_data);

        return json;
    }
}

public class WsMessage
{
    public string MsgType { get; set; }
    public object MsgContent { get; set; }

    public WsMessage(string _msgType, object _msgContent)
    {
        MsgType = _msgType;
        MsgContent = _msgContent;
    }
}