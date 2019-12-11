using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Const
{
    public static class Url
    {
        public const string SERVER_ADDRESS = "http://152.136.99.117";
        public const string WEB_SOCKET_SERVER_ADDRESS = "ws://www.fraxinusmothership.cn/ws/control_center/?token=";

        public const string CONTROL_CENTER_LOGIN = SERVER_ADDRESS + "/auth/check_control_center_login/";
        public const string REQUEST_WS_TOKEN = SERVER_ADDRESS + "/auth/get_ws_token/";

        public const string CHANGE_GAME_STATUS = SERVER_ADDRESS + "/game_map/change_game_status/";
        public const string GET_AVAILABLE_GAME_SESSIONS = SERVER_ADDRESS + "/game_map/get_available_game_sessions/";


    }
}
