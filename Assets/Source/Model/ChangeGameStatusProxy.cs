using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Newtonsoft.Json;

public class ChangeGameStatusProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "ChangeGameStatusProxy";

    private GameStatusVO m_currentGameStatus;

    public ChangeGameStatusProxy() : base(NAME) { }

    public void ChangeGameStatus(object _data)
    {
        ChangeGameStatusDelegate gameStatusDelegate = new ChangeGameStatusDelegate(this, (GameStatusVO)_data);
        gameStatusDelegate.ChangeGameStatusService();
    }
    
    public void OnResult(object _data)
    {
        SendNotification(Const.Notification.GAME_STATUS_CHANGED, 
                        new GameStatusVO((_data as ChangeGameStatusResponse).game_id, 
                                         (_data as ChangeGameStatusResponse).gameStatus,
                                         (_data as ChangeGameStatusResponse).game_time));

        if ((_data as ChangeGameStatusResponse).device_id_2_user_info != null)
        {
            SendNotification(Const.Notification.UPDATE_DEVICE_ID_TO_USER_INFO, (_data as ChangeGameStatusResponse).device_id_2_user_info);
        }
    }

    public void OnFault(object _data)
    {
        SendNotification(Const.Notification.GAME_STATUS_CHANGE_ERROR, _data);
    }

    private Dictionary<string, UwbUserInfo> DeserializeDeviceID2UserInfo(string _json)
    {
        return JsonConvert.DeserializeObject<Dictionary<string, UwbUserInfo>>(_json);
    }
}

public class ChangeGameStatusResponse : HttpResponse
{
    public Dictionary<string, UwbUserInfo> device_id_2_user_info { get; set; }
    public string game_id { get; set; }
    public GameStatus gameStatus { get; set; }
    public string game_time { get; set; }

    public ChangeGameStatusResponse(int _errCode, string _errMsg) : base(_errCode, _errMsg) { }
}

