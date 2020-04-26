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
        m_wsService.AddExceptionHandler((string _msg) => { 
            SendNotification(Const.Notification.WARNING_POPUP, _msg + "\n请重新登录");
            SendNotification(Const.Notification.GAME_CLOSED);
        });
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
        SendNotification(Const.Notification.DEBUG_LOG, "WS Server Error: " + _message);
    }

    private void WebSocketCloseHandler(string _message)
    {
        SendNotification(Const.Notification.DEBUG_LOG, "WS Server Closed: " + _message);
    }

    private void WebSocketMessageHandler(string _message)
    {

        MainThreadCall.SafeCallback(() => { 
            SendNotification(Const.Notification.SERVER_MSG_ARRIVED, _message);
            SendNotification(Const.Notification.DEBUG_LOG, "WS Server Message: " + _message);
        });
    }

    private string ToJson(object _data)
    {
        string json = JsonConvert.SerializeObject(_data);
        Debug.Log(json);

        return json;
    }

    /*
    private string testWsSend()
    {
        string result = "None";

        Dictionary<string, DeviceLocationInfo> gameMap = new Dictionary<string, DeviceLocationInfo>();
        gameMap.Add("did00001", new DeviceLocationInfo(1, 1, "room1"));
        gameMap.Add("did00002", new DeviceLocationInfo(2, 2, "room2"));

        WsMessage wsMsg = new WsMessage("location", gameMap);

        result = WsMsgToJson(wsMsg);

        return result;
    }
    */
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