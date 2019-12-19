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
        RegisterCommand(Const.Notification.SEND_LOGIN, typeof(LoginCommand));
        RegisterCommand(Const.Notification.CONNECT_TO_WS_SERVER, typeof(ServerCommunicationCommand));
        RegisterCommand(Const.Notification.WS_SEND, typeof(ServerCommunicationCommand));
        RegisterCommand(Const.Notification.CHANGE_GAME_STATUS, typeof(GameStatusCommand));
        RegisterCommand(Const.Notification.REQUEST_FOR_GAME_STATUS, typeof(GameStatusCommand));
        RegisterCommand(Const.Notification.UPDATE_DEVICE_ID_TO_USER_INFO, typeof(GameMapCommand));
        RegisterCommand(Const.Notification.RECEIVED_GAME_STATUS, typeof(MainFSMCommand));
        RegisterCommand(Const.Notification.LOGIN_SUCCESS, typeof(MainFSMCommand));
        RegisterCommand(Const.Notification.LOAD_SCENE, typeof(SceneCommand));
        RegisterCommand(Const.Notification.LOAD_UI_FORM, typeof(UICommand));
    }

    public void startup()
    {
        SendNotification(STARTUP);
    }
}
