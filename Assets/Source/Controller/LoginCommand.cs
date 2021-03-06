﻿using System.Collections;
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
        LogoutProxy logoutProxy;
        logoutProxy = Facade.RetrieveProxy(LogoutProxy.NAME) as LogoutProxy;
        string name = notification.Name;

        switch (name)
        {
            case Const.Notification.SEND_LOGIN:
                loginProxy.SendLogin(obj);
                break;
            case Const.Notification.SEND_LOGOUT:
                logoutProxy.SendLogout();
                break;
        }
    }
}
