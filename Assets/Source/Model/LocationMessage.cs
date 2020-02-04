using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMessage : ServerMessage
{
    public const string LOCATION_MSG_TYPE = "location";
    public Dictionary<string, LocationInfo> MsgContent { get; private set; }

    public LocationMessage(Dictionary<string, LocationInfo> _msgContent) : base(LOCATION_MSG_TYPE)
    {
        MsgContent = _msgContent;
    }
}

public class LocationInfo
{ 
    public float x { get; private set; }
    public float y { get; private set; }
    public string rid { get; private set; }

    public LocationInfo(float _x, float _y, string _roomID)
    {
        x = _x;
        y = _y;
        rid = _roomID;
    }
}