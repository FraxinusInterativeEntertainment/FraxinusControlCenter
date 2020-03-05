using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class LogoutProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "LogoutProxy";

    public LogoutProxy() : base(NAME) { }

    public void SendLogout()
    {
        LogoutDelegate logoutDelegate = new LogoutDelegate(this);
        logoutDelegate.LoginService();
    }

    public void OnResult(object _data)
    {
        SendNotification(Const.Notification.LOGOUT_SUCCESS);
    }

    public void OnFault(object _data)
    {
        SendNotification(Const.Notification.LOGIN_FAIL, _data);
    }
}
