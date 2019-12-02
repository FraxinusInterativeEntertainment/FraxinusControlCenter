using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap
{
    public readonly Dictionary<string, DeviceLocationInfo> deviceLocationInfo = new Dictionary<string, DeviceLocationInfo>();
    public readonly Dictionary<string, UwbUserInfo> deviceIdToUserInfo = new Dictionary<string, UwbUserInfo>();
}

public class DeviceLocationInfo
{
    public float x;
    public float y;
    public string rid;

    public DeviceLocationInfo(float _x, float _y, string _rid)
    {
        x = _x;
        y = _y;
        rid = _rid;
    }
}

public class UwbUserInfo
{
    public string user_id { get; set; }
    public string nickname { get; set; }

    public UwbUserInfo(string _user_id, string _nickname)
    {
        user_id = _user_id;
        nickname = _nickname;
    }
}