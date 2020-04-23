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
        RegisterCommand(Const.Notification.SEND_LOGOUT, typeof(LoginCommand));
        RegisterCommand(Const.Notification.CONNECT_TO_WS_SERVER, typeof(ServerCommunicationCommand));
        RegisterCommand(Const.Notification.SERVER_MSG_ARRIVED, typeof(ServerCommunicationCommand));
        RegisterCommand(Const.Notification.WS_SEND, typeof(ServerCommunicationCommand));
        RegisterCommand(Const.Notification.CHANGE_GAME_STATUS, typeof(GameStatusCommand));
        RegisterCommand(Const.Notification.REQUEST_FOR_GAME_STATUS, typeof(GameStatusCommand));
        RegisterCommand(Const.Notification.RECV_PLAYER_POS_INFOS, typeof(GameMapCommand));
        RegisterCommand(Const.Notification.RECEIVED_GAME_STATUS, typeof(MainFSMCommand));
        RegisterCommand(Const.Notification.LOGIN_SUCCESS, typeof(MainFSMCommand));
        RegisterCommand(Const.Notification.LOAD_SCENE, typeof(SceneCommand));
        RegisterCommand(Const.Notification.SHOW_MAIN_PANEL_CONTENT, typeof(UICommand));
        RegisterCommand(Const.Notification.RECV_ALL_GAME_CONDITIONS, typeof(ConditionCommand));
        RegisterCommand(Const.Notification.RECV_GAME_CONDITION_CHANGE, typeof(ConditionCommand));
        RegisterCommand(Const.Notification.UPDATE_MCU_REQUEST, typeof(McuCommand));
        RegisterCommand(Const.Notification.UPDATE_MCU_STATUS, typeof(McuCommand));
        RegisterCommand(Const.Notification.TRY_SEND_MCU_MSG, typeof(McuCommand));
        RegisterCommand(Const.Notification.TRY_CONFIRM_MCU_DISCONNECTED, typeof(McuCommand));
        RegisterCommand(Const.Notification.LOGIN_SUCCESS, typeof(McuCommand));
        RegisterCommand(Const.Notification.GAME_STARTED, typeof(UpdateGameServerCommand));
        RegisterCommand(Const.Notification.GAME_CLOSED, typeof(UpdateGameServerCommand));
    }

    public void startup()
    {
        SendNotification(STARTUP);
    }
}
