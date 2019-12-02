using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Newtonsoft.Json;

public class GameStatusProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "GameStatusProxy";

    public GameStatusProxy() : base(NAME) { }

    public void ChangeGameStatus(object _data)
    {
        GameStatusDelegate gameStatusDelegate = new GameStatusDelegate(this, (GameStatusVO)_data);
        gameStatusDelegate.ChangeGameStatusService();
    }

    public void OnResult(object _data)
    {
        SendNotification(Constants.Notification.GAME_STATUS_CHANGED, _data);

        if ((_data as ChangeGameStatusResponse).device_id_2_user_info != null)
        {
            SendNotification(Constants.Notification.UPDATE_DEVICE_ID_TO_USER_INFO, (_data as ChangeGameStatusResponse).device_id_2_user_info);
        }
    }

    public void OnFault(object _data)
    {
        SendNotification(Constants.Notification.GAME_STATUS_CHANGE_ERROR, _data);
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

    public ChangeGameStatusResponse(int _errCode, string _errMsg) : base(_errCode, _errMsg)
    {
    }
}

