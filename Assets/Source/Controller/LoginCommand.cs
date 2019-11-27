using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class LoginCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        object obj = notification.Body;
        LoginProxy loginProxy;
        loginProxy = Facade.RetrieveProxy(LoginProxy.NAME) as LoginProxy;
        string name = notification.Name;

        switch (name)
        {
            case Constants.Notification.SEND_LOGIN:
                loginProxy.SendLogin(obj);
                break;
        }
    }
}
