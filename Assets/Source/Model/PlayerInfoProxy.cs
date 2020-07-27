using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Threading;

public class PlayerInfoProxy : Proxy, IProxy,IResponder
{
    public const string NAME = "PlayerInfoProxy";
    //中控给游戏服务器发送玩家位置信息的频率（每秒发送次数），通常小于从定位服务器接受坐标的频率
    public const float SEND_POS_INFO_FREQ = 1f;
    private bool m_sendPosInfo = false;

    private IEnumerator m_sendPosInfoCoroutine;

    public PlayerInfoProxy() : base(NAME, new PlayerInfoModel())
    {
        InitPlayerInfos();
        m_sendPosInfoCoroutine = SendPlayerPosInfo(SEND_POS_INFO_FREQ);
        GameManager.instance.StartCoroutine(m_sendPosInfoCoroutine);
    }

    public PlayerInfoModel GetPlayerInfos()
    {
        return m_data as PlayerInfoModel;
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
        m_sendPosInfo = true;
    }

    public void StopSendingPlayerPosInfo()
    {
        m_sendPosInfo = false;
    }

    public void OnMultiplePlayersJoined(Dictionary<string, UserInfo> _newPlayers)
    {
        foreach (KeyValuePair<string, UserInfo> kvp in _newPlayers)
        {
            if (!(m_data as PlayerInfoModel).connectedPlayers.ContainsKey(kvp.Key))
            {
                (m_data as PlayerInfoModel).connectedPlayers.Add(kvp.Key, new PlayerInfo(kvp.Value.uid, kvp.Key, kvp.Value.nickname, PlayerStatus.Unknown));
            }
            (m_data as PlayerInfoModel).connectedPlayers[kvp.Key].status = PlayerStatus.Connected;
            SendNotification(Const.Notification.RECV_PLAYER_INFO, (m_data as PlayerInfoModel).connectedPlayers);
        }
    }
    public void UpdatePlayerList(Dictionary<string, UserInfo> _playerList)
    {
        (m_data as PlayerInfoModel).connectedPlayers.Clear();

        foreach (KeyValuePair<string, UserInfo> kvp in _playerList)
        {
            (m_data as PlayerInfoModel).connectedPlayers.Add(kvp.Key, new PlayerInfo(kvp.Value.uid, kvp.Key, kvp.Value.nickname, PlayerStatus.Unknown));
            (m_data as PlayerInfoModel).connectedPlayers[kvp.Key].status = PlayerStatus.Connected;
        }

        SendNotification(Const.Notification.PLAYER_LIST_UPDATED);
        SendNotification(Const.Notification.RECV_PLAYER_INFO, (m_data as PlayerInfoModel).connectedPlayers);
    }

    public void OnPlayerJoined()
    { 

    }

    private void NewPlayerHandler(PlayerInfo _playerInfo)
    { 

    }

    private void InitPlayerInfos()
    {
          
    }

    IEnumerator SendPlayerPosInfo(float _freq)
    { 
        while(true)
        {
            if (m_sendPosInfo)
            {
                //使用多线程打包玩家位置信息防止以后同场玩家过多的情况下造成程序卡顿
                //TODO: 可能造成deadlock
                ThreadPool.QueueUserWorkItem( (object state) => { SendNotification(Const.Notification.SEND_PLAYER_LOCATION_INFOS, m_data); });
            }
            yield return new WaitForSeconds(1/_freq);
        }
    }
    public void TryChangePlayerGroup(PlayerInfoVO _playerInfo)
    {
        ChangePlayerGroupDelegate playerGroupDelegate = new ChangePlayerGroupDelegate(this, _playerInfo);
        playerGroupDelegate.AddPlayerToGroup();

    }
    public void OnResult(object _data)
    {
        
    }
    public void OnFault(object _data)
    {

    }
}
public class PlayerInfoVO
{
    public string playerUID { get; set; }
    public string targetGroupName { get; set; }
    public PlayerInfoVO()
    {
        playerUID = "";
        targetGroupName = "";
    }

    public PlayerInfoVO(string _playerUID, string _targetGroupName)
    {
        this.playerUID = _playerUID;
        this.targetGroupName = _targetGroupName;
    }
}

