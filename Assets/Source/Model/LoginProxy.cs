using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class LoginProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "LoginProxy";

    public LoginProxy() : base(NAME) { }

    public void SendLogin(object _data)
    {
        LoginDelegate loginDelegate = new LoginDelegate(this, _data as LoginVO);
        loginDelegate.LoginService();
    }

    public void OnResult(object _data)
    {
        SendNotification(Constants.Notification.LOGIN_SUCCESS, _data);
        SendNotification(Constants.Notification.CONNECT_TO_WS_SERVER, _data);
    }

    public void OnFault(object _data)
    {
        SendNotification(Constants.Notification.LOGIN_FAIL, _data);
    }
}

public class TokenRequestResponse : HttpResponse
{
    public string ws_token;

    public TokenRequestResponse(int _errCode, string _errMsg, string _token) : base(_errCode, _errMsg)
    {
        ws_token = _token;
    }
}
