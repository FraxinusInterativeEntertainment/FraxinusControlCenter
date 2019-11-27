using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class LoginProxy : Proxy, IProxy
{
    public const string NAME = "LoginProxy";

    public LoginProxy() : base(NAME) { }

    public void SendLogin(object _data)
    {
        Debug.Log("Login Proxy: Received Login Command \n" + (_data as LoginVO).userName + " : " + (_data as LoginVO).password);
        ReceiveLogin(_data);
    }

    private void ReceiveLogin(object _data)
    {
        SendNotification(Constants.Notification.RECEIVE_LOGIN, _data);
    }
}
