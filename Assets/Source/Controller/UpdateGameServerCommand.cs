using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class UpdateGameServerCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;

        PlayerInfoProxy playerInfoProxy;
        playerInfoProxy = Facade.RetrieveProxy(PlayerInfoProxy.NAME) as PlayerInfoProxy;

        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.GAME_STARTED:
                playerInfoProxy.StartSendingPlayerPosInfo();
                break;
            case Const.Notification.GAME_CLOSED:
                playerInfoProxy.StopSendingPlayerPosInfo();
                break;
        }

    }
}
