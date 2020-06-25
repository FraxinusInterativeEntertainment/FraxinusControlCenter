using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoModel
{
    public readonly Dictionary<string, PlayerInfo> connectedPlayers = new Dictionary<string, PlayerInfo>();
}

public class PlayerInfo
{ 
    public string uid { get; private set; }
    public string bid { get; private set; }
    public string did { get; private set; }
    public string nickName { get; private set; }
    public PlayerPosInfo posInfo { get; private set; }
    public PlayerStatus status { get; set; }
    public GameGroup gameGroup { get; set; }

    public PlayerInfo(string _uid, string _did, string _nickName, PlayerStatus _status)
    {
        uid = _uid;
        nickName = _nickName;
        status = _status;
        did = _did;

        posInfo = new PlayerPosInfo(_did, new Coor2D(-99, -99));
    }
}

public enum PlayerStatus
{ 
    Unknown,
    Connected,
    Offline
}