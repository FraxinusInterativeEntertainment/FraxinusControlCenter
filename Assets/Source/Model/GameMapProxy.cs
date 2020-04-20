using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;


public class GameMapProxy : Proxy, IProxy
{
    public const string NAME = "GameMapProxy";
    public const string HSV_REF_MAP_PATH = "Textures/Other/HsvRefMap2";

    private GameMap m_gameMap;

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
        //TODO: request from game server
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

    /*
    public void SendDeviceLocationInfo()
    {
        SendNotification("sfsd", m_gameMap);
    }

    public void UpdateDeviceLocationInfoDict()
    {

    }

    public void UpdateDeviceIdToUserInfoDict(object _data)
    {
        Dictionary<string, UwbUserInfo> _didToUserInfo = _data as Dictionary<string, UwbUserInfo>;

        foreach (KeyValuePair<string, UwbUserInfo> keyValuePair in _didToUserInfo)
        {
            if (!m_gameMap.deviceIdToUserInfo.ContainsKey(keyValuePair.Key))
            {
                m_gameMap.deviceIdToUserInfo.Add(keyValuePair.Key, keyValuePair.Value);
            }
            else
            {
                m_gameMap.deviceIdToUserInfo[keyValuePair.Key] = keyValuePair.Value;
            }
        }
    }
    */
}
