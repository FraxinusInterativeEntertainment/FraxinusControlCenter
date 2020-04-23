using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Const
{
    public static class Url
    {
        public const string HTTP_SERVER_ADDRESS = "http://testgame.fraxinusmothership.cn";
        public const string WEB_SOCKET_HOST_URI = "http://testgame.fraxinusmothership.cn";
        public const string WEB_SOCKET_SERVER_ADDRESS = "ws://testgame.fraxinusmothership.cn/ws/control_center/";
        public const string TCP_SOCKET_IP = "192.168.1.106";

        public const string CONTROL_CENTER_LOGIN = HTTP_SERVER_ADDRESS + "/game_auth/control_center_login/";
        public const string CONTROL_CENTER_LOGOUT = HTTP_SERVER_ADDRESS + "/game_auth/device_logout/";
        public const string REQUEST_WS_TOKEN = HTTP_SERVER_ADDRESS + "/game_auth/get_ws_token/";

        public const string CHANGE_GAME_STATUS = HTTP_SERVER_ADDRESS + "/game_map/change_game_status/";
        public const string GET_AVAILABLE_GAME_SESSIONS = HTTP_SERVER_ADDRESS + "/game_map/get_available_game_sessions/";


    }
}
