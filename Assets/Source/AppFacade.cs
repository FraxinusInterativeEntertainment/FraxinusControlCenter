using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class AppFacade : Facade, IFacade
{
    public const string STARTUP = "Startup";
    public const string LOGIN = "login";

    private static AppFacade m_instance;

    public static AppFacade instance
    {
        get{
            if (m_instance == null)
            {
                m_instance = new AppFacade();
            }
            return m_instance;
        }
    }

    protected override void InitializeController()
    {
        base.InitializeController();
        RegisterCommand(STARTUP, typeof(StartupCommand));
        RegisterCommand(Constants.Notification.SEND_LOGIN, typeof(LoginCommand));
        RegisterCommand(Constants.Notification.CONNECT_TO_WS_SERVER, typeof(ServerCommunicationCommand));
        RegisterCommand(Constants.Notification.WS_SEND, typeof(ServerCommunicationCommand));
    }

    public void startup()
    {
        SendNotification(STARTUP);
    }
}
