using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap
{
    public Texture2D hsvRefMap { get; set; }
    public Texture2D visualMap { get; set; }
    public readonly Dictionary<string, string> roomIdHsvRef = new Dictionary<string, string>();
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