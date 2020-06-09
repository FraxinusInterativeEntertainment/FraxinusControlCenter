using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;


public class GameMapProxy : Proxy, IProxy
{
    public const string NAME = "GameMapProxy";
    public const string HSV_REF_MAP_PATH = "Textures/Other/HsvRefMap2";
    public const string VISUAL_MAP_PATH = "Textures/Other/HsvRefMap2";
    public const float REAL_WORLD_LENGTH = 100f;
    public const float READ_WORLD_WIDTH = 100f;
    public const float MAP_LENGTH = 1024f;
    public const float MAP_WIDTH = 724f;

    public GameMapProxy() : base(NAME, new GameMap())
    {
        LoadHsvRefMap(new ResourcesService().Load<Texture2D>(HSV_REF_MAP_PATH));
        LoadVisualMap(new ResourcesService().Load<Texture2D>(HSV_REF_MAP_PATH));
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

    public Texture2D GetVisualMap()
    {
        return (m_data as GameMap).visualMap;
    }

    private void LoadHsvRefMap(Texture2D _map)
    {
        (m_data as GameMap).hsvRefMap = _map;
    }
    private void LoadVisualMap(Texture2D _map)
    {
        (m_data as GameMap).visualMap = _map;
    }
}
