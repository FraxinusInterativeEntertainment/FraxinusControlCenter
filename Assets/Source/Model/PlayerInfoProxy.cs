using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class PlayerInfoProxy : Proxy, IProxy
{
    public const string NAME = "PlayerInfoProxy";

    public PlayerInfoProxy() : base(NAME, new PlayerInfoModel())
    {
        InitPlayerInfos();
    }

    public PlayerInfo GetPlayerInfoByTagId(string _tagId)
    {
        Dictionary<string, PlayerInfo> info = (m_data as PlayerInfoModel).connectedPlayers;

        if (!info.ContainsKey(_tagId))
        {
            return null;
        }
        return (m_data as PlayerInfoModel).connectedPlayers[_tagId];
    }

    public void SetPlayerCurrentRoomId(string _tagId, string _roomId)
    {
        //GetPlayerInfoByTagId()[_tagId].rid = _roomId;
    }

    private void InitPlayerInfos()
    {
        //TODO: remove fake data
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag1", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag2", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
    }
}
