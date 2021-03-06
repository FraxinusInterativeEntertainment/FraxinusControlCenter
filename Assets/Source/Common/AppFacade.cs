﻿using System.Collections;
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
        RegisterCommand(Const.Notification.RECV_PLAYER_POS_INFOS, typeof(GameMapCommand));
        RegisterCommand(Const.Notification.RECEIVED_GAME_STATUS, typeof(MainFSMCommand));
        RegisterCommand(Const.Notification.LOGIN_SUCCESS, typeof(MainFSMCommand));
        RegisterCommand(Const.Notification.LOGOUT_SUCCESS, typeof(MainFSMCommand));
        RegisterCommand(Const.Notification.LOAD_SCENE, typeof(SceneCommand));
        RegisterCommand(Const.Notification.SHOW_MAIN_PANEL_CONTENT, typeof(UICommand));
        RegisterCommand(Const.Notification.SHOW_MAP_PANEL_CONTENT, typeof(UICommand));
        RegisterCommand(Const.Notification.SHOW_POPUP, typeof(UICommand));
        RegisterCommand(Const.Notification.CHECK_POPUP_QUE, typeof(UICommand));
        RegisterCommand(Const.Notification.LOCK_UI, typeof(UICommand));
        RegisterCommand(Const.Notification.UNLOCK_UI, typeof(UICommand));
        RegisterCommand(Const.Notification.RECV_ALL_GAME_CONDITIONS, typeof(ConditionCommand));
        RegisterCommand(Const.Notification.RECV_GAME_CONDITION_CHANGE, typeof(ConditionCommand));
        RegisterCommand(Const.Notification.RECV_GAME_QUEST_INFO, typeof(QuestControlCommand));
        RegisterCommand(Const.Notification.REQUEST_GROUP_NAME, typeof(QuestControlCommand));
        RegisterCommand(Const.Notification.GAME_STATUS_CHANGED, typeof(ConditionCommand));
        RegisterCommand(Const.Notification.TRY_CHANGE_PLAYE_GROUP, typeof(PlayerCommand));
        RegisterCommand(Const.Notification.TRY_REMOVE_PLAYER_FROM_GROUP, typeof(PlayerCommand));
        RegisterCommand(Const.Notification.INIT_MCU, typeof(McuCommand));
        RegisterCommand(Const.Notification.UPDATE_MCU_REQUEST, typeof(McuCommand));
        RegisterCommand(Const.Notification.UPDATE_MCU_STATUS, typeof(McuCommand));
        RegisterCommand(Const.Notification.TRY_SEND_MCU_MSG, typeof(McuCommand));
        RegisterCommand(Const.Notification.GAME_STARTED, typeof(UpdateGameServerCommand));
        RegisterCommand(Const.Notification.GAME_CLOSED, typeof(UpdateGameServerCommand));
        RegisterCommand(Const.Notification.UPDATE_DEVICE_ID_TO_USER_INFO, typeof(PlayerCommand));
        RegisterCommand(Const.Notification.SEND_PLAYER_LOCATION_INFOS, typeof(PlayerCommand));
        RegisterCommand(Const.Notification.SERVER_MSG_USER_INFO, typeof(PlayerCommand));
        RegisterCommand(Const.Notification.ADD_VIRTUAL_PLAYER_TO_GAME, typeof(SimulationCommand));
        RegisterCommand(Const.Notification.GENERATE_VIRTUAL_PLAYER, typeof(SimulationCommand));
        RegisterCommand(Const.Notification.CHANGE_GAME_STATUS, typeof(GameStatusCommand));
        RegisterCommand(Const.Notification.REQUEST_FOR_GAME_STATUS, typeof(GameStatusCommand));
        RegisterCommand(Const.Notification.TRY_CHANGE_QUEST_NODE, typeof(QuestControlCommand));
        RegisterCommand(Const.Notification.DEBUG_LOG, typeof(ErrorHandlerCommand));
        RegisterCommand(Const.Notification.WARNING_POPUP, typeof(ErrorHandlerCommand));
        RegisterCommand(Const.Notification.CUSTOMIZED_POPUP, typeof(ErrorHandlerCommand));
    }


    public void startup()
    {
        SendNotification(STARTUP);
        SendNotification(Const.Notification.SEND_LOGOUT);
    }
}
