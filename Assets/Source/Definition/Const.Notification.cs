using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Const
{
    public static class Notification
    {
        public const string TRY_LOGIN = "TryLogin";
        public const string LOGIN_SUCCESS = "LoginSuccess";
        public const string LOGOUT_SUCCESS = "LogoutSuccess";
        public const string LOGIN_FAIL = "LoginFail";
        public const string LOGOUT_FAIL = "LogoutFail";
        public const string SEND_LOGIN = "SendLogin";
        public const string SEND_LOGOUT = "SendLogout";
        public const string CONNECT_TO_WS_SERVER = "ConnectToWsServer";
        public const string WS_SEND = "WsSend";
        public const string CHANGE_GAME_STATUS = "ChangeGameStatus";
        public const string GAME_STATUS_CHANGED = "GameStatusChanged";
        public const string GAME_STATUS_CHANGE_ERROR = "GameStatusChangeError";
        public const string UPDATE_DEVICE_ID_TO_USER_INFO = "UpdateDeviceIdToUserInfo";
        public const string RECV_ALL_GAME_CONDITIONS = "RecvALLGameConditions";
        public const string RECV_GAME_CONDITION_CHANGE = "RecvGameConditionChange";
        public const string ALL_CONDITION_UPDATED = "AllConditionUpdated";
        public const string ALL_MCU_UPDATED = "AllMcuUpdated";
        public const string UPDATE_MCU_REQUEST = "UpdateMcuRequest";
        public const string LOAD_SCENE = "LoadScene";
        public const string REQUEST_FOR_GAME_STATUS = "RequestForGameSessions";
        public const string RECEIVED_GAME_STATUS = "ReceivedGameStatus";
        public const string GAME_STARTED = "GameStarted";
        public const string GAME_CLOSED = "GameClosed";
        public const string LOAD_UI_FORM = "LoadUIForm";
        public const string SHOW_MAIN_PANEL_CONTENT = "ShowMainPanelContent";
        public const string TRY_UPDATE_GAME_STATUS = "TryUpdateGameStatus";
        public const string SERVER_MSG_ARRIVED = "ServerMsgArrived";
    }
}
