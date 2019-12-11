using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

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
        }
    }
}
