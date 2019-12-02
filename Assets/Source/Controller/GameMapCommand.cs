using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class GameMapCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        GameMapProxy gameMapProxy;
        gameMapProxy = Facade.RetrieveProxy(GameMapProxy.NAME) as GameMapProxy;
        string name = _notification.Name;

        switch (name)
        {
            case Constants.Notification.UPDATE_DEVICE_ID_TO_USER_INFO:
                gameMapProxy.UpdateDeviceIdToUserInfoDict(obj);
                break;
        }
    }
}
