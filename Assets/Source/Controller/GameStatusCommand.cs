using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class GameStatusCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;

        ChangeGameStatusProxy changeGameStatusProxy;
        changeGameStatusProxy = Facade.RetrieveProxy(ChangeGameStatusProxy.NAME) as ChangeGameStatusProxy;

        GameStatusProxy gameStatusProxy;
        gameStatusProxy = Facade.RetrieveProxy(GameStatusProxy.NAME) as GameStatusProxy;

        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.CHANGE_GAME_STATUS:
                changeGameStatusProxy.ChangeGameStatus(obj);
                break;
            case Const.Notification.REQUEST_FOR_GAME_STATUS:
                gameStatusProxy.RequestForGameStatus();
                break;
        }
    }
}
