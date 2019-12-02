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
        GameStatusProxy gameStatusProxy;
        gameStatusProxy = Facade.RetrieveProxy(GameStatusProxy.NAME) as GameStatusProxy;
        string name = _notification.Name;

        switch (name)
        {
            case Constants.Notification.CHANGE_GAME_STATUS:
                gameStatusProxy.ChangeGameStatus(obj);
                break;
        }
    }
}
