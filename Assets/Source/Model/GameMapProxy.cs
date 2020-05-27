using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;


public class GameMapProxy : Proxy, IProxy
{
    public const string NAME = "GameMapProxy";
    public const string HSV_REF_MAP_PATH = "Textures/Other/HsvRefMap2";

    public GameMapProxy() : base(NAME, new GameMap())
    {
        LoadHsvRefMap(new ResourcesService().Load<Texture2D>(HSV_REF_MAP_PATH));
        InitGameMapData();
    }

    public string GetRoomIdByHsv(string _hsv)
    {
        if (RoomIdHsvRef().ContainsKey(_hsv))
        {
            return RoomIdHsvRef()[_hsv];
        }
        else
        {
            return null;
        }
    }

    private void InitGameMapData()
    {
        //TODO: 从后台处获取（后台需要添加此表）
        RoomIdHsvRef().Add("4_55_92", "room1");
        RoomIdHsvRef().Add("10_57_97", "room2");
        RoomIdHsvRef().Add("10_12_97", "room3");
        RoomIdHsvRef().Add("205_25_102", "room4");
        RoomIdHsvRef().Add("205_25_103", "room5");
    }

    private Dictionary<string, string> RoomIdHsvRef()
    {
        return (m_data as GameMap).roomIdHsvRef;
    }

    public Texture2D GetHsvRefMap()
    {
        return (m_data as GameMap).hsvRefMap;
    }

    private void LoadHsvRefMap(Texture2D _map)
    {
        (m_data as GameMap).hsvRefMap = _map;
    }
}
