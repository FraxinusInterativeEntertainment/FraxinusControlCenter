using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Threading;

public class PlayerInfoProxy : Proxy, IProxy
{
    public const string NAME = "PlayerInfoProxy";
    //中控给游戏服务器发送玩家位置信息的频率（每秒发送次数），通常小于从定位服务器接受坐标的频率
    public const float SEND_POS_INFO_FREQ = 1f;

    private IEnumerator m_sendPosInfoCoroutine;

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

    public void StartSendingPlayerPosInfo()
    {
        if (m_sendPosInfoCoroutine == null)
        {
            m_sendPosInfoCoroutine = SendPlayerPosInfo(SEND_POS_INFO_FREQ);
            GameManager.instance.StartCoroutine(m_sendPosInfoCoroutine);
        }
    }

    public void StopSendingPlayerPosInfo()
    {
        GameManager.instance.StopCoroutine(m_sendPosInfoCoroutine);
        m_sendPosInfoCoroutine = null;
    }

    private void InitPlayerInfos()
    {
        //TODO: remove fake data
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag1", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag2", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag3", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag4", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag5", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag6", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag7", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag8", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag9", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag10", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag11", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag12", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag13", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag14", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag15", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag16", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag17", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag18", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag19", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag20", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag21", new PlayerInfo("uid11111", "1", "Mike", PlayerStatus.Connected));
        (m_data as PlayerInfoModel).connectedPlayers.Add("tag22", new PlayerInfo("uid11111", "2", "John", PlayerStatus.Connected));

    }

    IEnumerator SendPlayerPosInfo(float _freq)
    { 
        while(true)
        {
            //使用多线程打包玩家位置信息防止以后同场玩家过多的情况下造成程序卡顿
            ThreadPool.QueueUserWorkItem(PackAndSend, m_data);
            yield return new WaitForSeconds(1/_freq);
        }
    }

    private void PackAndSend(object _playerInfoModel)
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
