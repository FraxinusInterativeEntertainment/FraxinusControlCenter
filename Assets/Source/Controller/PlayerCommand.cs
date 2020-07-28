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
                PackAndSend(playerInfoProxy.GetPlayerInfos());
                break;
            case Const.Notification.SERVER_MSG_USER_INFO:
                playerInfoProxy.OnMultiplePlayersJoined(obj as Dictionary<string, UserInfo>);
                break;
            case Const.Notification.TRY_CHANGE_PLAYE_GROUP:
                playerInfoProxy.TryChangePlayerGroup(obj as PlayerInfoVO);
                break;
            case Const.Notification.TRY_REMOVE_PLAYER_FROM_GROUP:
                playerInfoProxy.TryRemovePlayerFromGroup(obj as string);
                break;
        }
    }

    public void PackAndSend(object _playerInfoModel)
    {
        Dictionary<string, PlayerInfo> connectedPlayers = (_playerInfoModel as PlayerInfoModel).connectedPlayers;

        Dictionary<string, PlayerPosInfo> locationInfos = new Dictionary<string, PlayerPosInfo>();

        foreach (KeyValuePair<string, PlayerInfo> kvp in connectedPlayers)
        {
            locationInfos.Add(kvp.Key, kvp.Value.posInfo);
        }

        SendNotification(Const.Notification.WS_SEND, new LocationMessage(locationInfos));
    }
}
