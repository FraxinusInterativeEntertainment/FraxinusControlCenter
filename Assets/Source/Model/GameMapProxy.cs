using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;


public class GameMapProxy : Proxy, IProxy
{
    public const string NAME = "GameMapProxy";

    private GameMap m_gameMap;

    public GameMapProxy() : base(NAME)
    {
        m_gameMap = new GameMap();
    }

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
}
