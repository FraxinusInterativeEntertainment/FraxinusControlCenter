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
        public const string INIT_MCU = "InitMcu";
        public const string UPDATE_MCU_REQUEST = "UpdateMcuRequest";
        public const string UPDATE_MCU_STATUS = "UpdateMcuStatus";
        public const string UPDATE_MCU_ITEM = "UpdateMcuItem";
        public const string TRY_SEND_MCU_MSG = "TrySendMcuMsg";
        public const string TRY_CONFIRM_MCU_DISCONNECTED = "TryConfirmMcuDisconnnected";
        public const string RECV_PLAYER_POS_INFOS = "RecvPlayerPosInfos";
        public const string ADD_VIRTUAL_PLAYER_TO_GAME = "AddVirtualPlayerToGame";
        public const string GENERATE_VIRTUAL_PLAYER = "GenerateVirtualPlayer";
        public const string SEND_PLAYER_LOCATION_INFOS = "SendPlayerLocationInfos";
        public const string SERVER_MSG_USER_INFO = "ServerMsgUserInfo";
        public const string SERVER_MSG_GROUP_INFO = "ServerMsgGroupInfo";

        #region System Core
        public const string LOAD_SCENE = "LoadScene";
        public const string REQUEST_FOR_GAME_STATUS = "RequestForGameSessions";
        public const string RECEIVED_GAME_STATUS = "ReceivedGameStatus";
        public const string GAME_STARTED = "GameStarted";
        public const string GAME_CLOSED = "GameClosed";
        public const string LOAD_UI_FORM = "LoadUIForm";
        public const string SHOW_MAIN_PANEL_CONTENT = "ShowMainPanelContent";
        public const string SHOW_POPUP = "ShowPopup";
        public const string TRY_UPDATE_GAME_STATUS = "TryUpdateGameStatus";
        public const string SERVER_MSG_ARRIVED = "ServerMsgArrived";
        public const string DEBUG_LOG = "DebugLog";
        public const string WARNING_POPUP = "WarningPopup";
        public const string CUSTOMIZED_POPUP = "CustomizedPopup";
        public const string CHECK_POPUP_QUE = "CheckPopuQue";
        public const string TRY_CHANGE_QUEST_NODE = "TryCHangeQuestNode";
        #endregion

        #region Testing
        public const string V_PLAYER_LOGIN_SUCCESS = "VPlayerLoginSuccess";
        #endregion
    }
}
