using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosInfosVO
{
    public readonly Dictionary<string, PlayerPosInfo> playerPosInfos = new Dictionary<string, PlayerPosInfo>();
}

public class PlayerPosInfo
{ 
    public string did { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public string rid { get; set; }

    public PlayerPosInfo (string _did, Coor2D _pos)
    {
        did = _did;
        x = _pos.x;
        y = _pos.y;
    }
}

public class Coor2D
{
    public float x { get; set; }
    public float y { get; set; }

    public Coor2D(float _x, float _y)
    {
        x = _x;
        y = _y;
    }
}
