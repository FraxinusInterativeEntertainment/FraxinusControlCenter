using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class PlayerCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        object obj = notification.Body;
        PlayerInfoProxy playerInfoProxy;
        playerInfoProxy = Facade.RetrieveProxy(PlayerInfoProxy.NAME) as PlayerInfoProxy;
        string name = notification.Name;

        switch (name)
        {
            case Const.Notification.UPDATE_DEVICE_ID_TO_USER_INFO:
                playerInfoProxy.UpdatePlayerList(obj as Dictionary<string, UserInfo>);
                break;
            case Const.Notification.SEND_PLAYER_LOCATION_INFOS:
                playerInfoProxy.PackAndSend(playerInfoProxy.GetPlayerInfos());
                break;
            case Const.Notification.SERVER_MSG_USER_INFO:
                playerInfoProxy.OnMultiplePlayersJoined(obj as Dictionary<string, UserInfo>);
                break;
            case Const.Notification.TRY_CHANGE_PLAYE_GROUP:
                playerInfoProxy.TryChangePlayerGroup(obj as PlayerInfoVO);
                break;
        }
    }
}
