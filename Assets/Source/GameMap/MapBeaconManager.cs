using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBeaconManager
{
    private readonly Dictionary<string, PlayerMapBeacon> m_playerBeacons = new Dictionary<string, PlayerMapBeacon>();

    private GameMapView m_uiView;
    private GameObject m_beaconContainer;
    private GameObject m_beaconPrefab;
    private GameMapConfig m_mapConfig;
    private GroupColorConfig m_colorConfig;

    public MapBeaconManager (GameMapView _uiVIew, GameMapConfig _gameMapConfig, GroupColorConfig _colorConfig)
    {
        m_uiView = _uiVIew;
        m_mapConfig = _gameMapConfig;
        m_colorConfig = _colorConfig;
    }

    public void UpdatePlayerBeacon(PlayerInfo _playerInfo)
    {
        if (!m_playerBeacons.ContainsKey(_playerInfo.uid))
        {
            AddPlayerBeacon(_playerInfo);
        }

        m_playerBeacons[_playerInfo.uid].UpdatePosition(RearWorldToMapCoor(_playerInfo.posInfo));
    }

    public void ClearPlayerBeacons()
    {
        foreach (KeyValuePair<string, PlayerMapBeacon> kvp in m_playerBeacons)
        {
            kvp.Value.Destroy();
        }

        m_playerBeacons.Clear();
    }

    public void ShowPlayerUid(bool _value)
    {
        foreach (KeyValuePair<string, PlayerMapBeacon> kvp in m_playerBeacons)
        {
            kvp.Value.SetUidVisible(_value);
        }
    }

    public void ShowPlayerNickname(bool _value)
    {
        foreach (KeyValuePair<string, PlayerMapBeacon> kvp in m_playerBeacons)
        {
            kvp.Value.SetNicknameVisible(_value);
        }
    }

    private void AddPlayerBeacon(PlayerInfo _playerInfo)
    {
        PlayerMapBeacon beacon = m_uiView.GeneratePlayerMapBeacon();
        m_playerBeacons.Add(_playerInfo.uid, beacon);
        beacon.Init(_playerInfo);
        UpdateBeaconColor(beacon, _playerInfo.gameGroup);
    }

    private Vector3 RearWorldToMapCoor(PlayerPosInfo _posInfo)
    {
        Vector3 mapPos = new Vector3(-99, -99, 0);

        mapPos.x = (int)(_posInfo.x / m_mapConfig.realWorldWidth * (m_mapConfig.mapImageWidth - 1));
        mapPos.y = (int)(_posInfo.y / m_mapConfig.realWorldLength * (m_mapConfig.mapImageLength - 1));

        return mapPos;
    }

    private void UpdateBeaconColor(MapBeacon _beacon, GameGroup _group)
    {
        _beacon.SetBeaconColor(m_colorConfig.GetGroupColor(_group));
    }
}

