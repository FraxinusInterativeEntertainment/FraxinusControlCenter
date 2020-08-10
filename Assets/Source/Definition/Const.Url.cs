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
        public const string TCP_SOCKET_IP = "192.168.0.106";

        public const string CONTROL_CENTER_LOGIN = HTTP_SERVER_ADDRESS + "/game_auth/control_center_login/";
        public const string CONTROL_CENTER_LOGOUT = HTTP_SERVER_ADDRESS + "/game_auth/device_logout/";
        public const string REQUEST_WS_TOKEN = HTTP_SERVER_ADDRESS + "/game_auth/get_ws_token/";

        public const string GET_ALL_GROUP_INFOS = HTTP_SERVER_ADDRESS + "/control_center/get_all_group_infos/";
        public const string CHANGE_GAME_STATUS = HTTP_SERVER_ADDRESS + "/control_center/change_game_status/";
        public const string GET_AVAILABLE_GAME_SESSIONS = HTTP_SERVER_ADDRESS + "/game_map/get_available_game_sessions/";
        public const string GET_ALL_MCU_INFO = HTTP_SERVER_ADDRESS + "/control_center/get_all_mcu_info/";
        public const string GET_ALL_MCU_MODULE_INFO = HTTP_SERVER_ADDRESS + "/control_center/get_all_mcu_module_info/";
        public const string CHANGE_GROUP_QUEST_NODE = HTTP_SERVER_ADDRESS + "/control_center/change_group_quest_node/";

        public const string GET_ALL_CONDITION_DETAIL = HTTP_SERVER_ADDRESS + "/control_center/get_all_condition_detail/";
        public const string TRY_ADD_PLAYER_TO_GROUP = HTTP_SERVER_ADDRESS + "/control_center/add_user_to_group/";
        public const string TRY_REMOVE_PLAYER_FROM_GROUP = HTTP_SERVER_ADDRESS + "/control_center/remove_player_from_group/";
        //test
        public const string ADD_USER_TO_GAME = HTTP_SERVER_ADDRESS + "/control_center/add_user_to_game_test/";
        public const string GET_TEST_USER = HTTP_SERVER_ADDRESS + "/game_map/get_test_user/";
        public const string BAND_ID_LOGIN = HTTP_SERVER_ADDRESS + "/game_auth/device_wristband_login/";


    }
}
