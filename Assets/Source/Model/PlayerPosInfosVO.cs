using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosInfosVO
{
    public readonly List<PlayerPosInfo> playerPosInfos = new List<PlayerPosInfo>();
}

public class PlayerPosInfo
{ 
    public string tagId { get; set; }
    public Coor2D position { get; set; }

    public PlayerPosInfo (string _tagId, Coor2D _pos)
    {
        tagId = _tagId;
        position = _pos;
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
