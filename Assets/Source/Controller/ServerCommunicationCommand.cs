using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Newtonsoft.Json;

public class ServerCommunicationCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        object obj = notification.Body;
        ServerCommunicationProxy serverCommunicationProxy;
        serverCommunicationProxy = Facade.RetrieveProxy(ServerCommunicationProxy.NAME) as ServerCommunicationProxy;
        string name = notification.Name;

        switch (name)
        {
            case Const.Notification.CONNECT_TO_WS_SERVER:
                serverCommunicationProxy.ConnectFraxMotherShipWs(obj);
                break;
            case Const.Notification.WS_SEND:
                serverCommunicationProxy.SendMessage(obj);
                break;
            case Const.Notification.SERVER_MSG_ARRIVED:
                ServerMsgHandler(obj as string);
                break;
        }
    }

    private void ServerMsgHandler(string _msg)
    { 
        ServerMessage serverMsg = JsonConvert.DeserializeObject<ServerMessage>(_msg);

        switch (serverMsg.MsgType)
        {
            case "condition_info":
                ServerConditionMessage conditionMsg = JsonConvert.DeserializeObject<ServerConditionMessage>(_msg);
                SendNotification(Const.Notification.RECV_GAME_CONDITION_CHANGE, conditionMsg.MsgContent);
                break;
            case "user_info":
                ServerUserInfoMessage userInfoMsg = JsonConvert.DeserializeObject<ServerUserInfoMessage>(_msg);
                SendNotification(Const.Notification.SERVER_MSG_USER_INFO, userInfoMsg.MsgContent);
                break;
            case "group_info":
                SendNotification(Const.Notification.SERVER_MSG_GROUP_INFO);
                break;
            case "quest_info":
                ServerQuestMessage questMsg = JsonConvert.DeserializeObject<ServerQuestMessage>(_msg);
                SendNotification(Const.Notification.RECV_GAME_QUEST_INFO, questMsg.MsgContent);
                break;
        }
    }
}
