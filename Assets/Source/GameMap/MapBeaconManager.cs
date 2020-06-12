using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBeaconManager : MonoBehaviour
{
    private readonly Dictionary<string, PlayerMapBeacon> m_playerBeacons = new Dictionary<string, PlayerMapBeacon>();
    private GameObject m_beaconContainer;
    private GameObject m_beaconPrefab;
    private GameMapConfig m_mapConfig;

    public MapBeaconManager (GameObject _beaconContainer, GameObject _beaconPrefab, GameMapConfig _gameMapConfig)
    {
        m_beaconContainer = _beaconContainer;
        m_beaconPrefab = _beaconPrefab;
        m_mapConfig = _gameMapConfig;
    }

    public void UpdatePlayerBeacon(string _uid, PlayerInfo _playerInfo)
    {
        if (!m_playerBeacons.ContainsKey(_uid))
        {
            m_playerBeacons.Add(_uid, GeneratePlayerBeacon(_playerInfo));
        }

        m_playerBeacons[_uid].UpdatePosition(RearWorldToMapCoor(_playerInfo.posInfo));
    }

    public void ClearPlayerBeacons()
    {
        foreach (KeyValuePair<string, PlayerMapBeacon> kvp in m_playerBeacons)
        {
            kvp.Value.Destroy();
        }

        m_playerBeacons.Clear();
    }

    private PlayerMapBeacon GeneratePlayerBeacon(PlayerInfo _playerInfo)
    {
        //TODO: 用工厂处理处理这几百个indicator的复用
        PlayerMapBeacon beacon = Instantiate(m_beaconPrefab, m_beaconContainer.transform).GetComponent<PlayerMapBeacon>();

        beacon.Init(_playerInfo);

        return beacon;
    }

    private Vector3 RearWorldToMapCoor(PlayerPosInfo _posInfo)
    {
        Vector3 mapPos = new Vector3(-99, -99, 0);

        mapPos.x = (int)(_posInfo.x / m_mapConfig.realWorldWidth * (m_mapConfig.mapImageWidth - 1));
        mapPos.y = (int)(_posInfo.y / m_mapConfig.realWorldLength * (m_mapConfig.mapImageLength - 1));

        return mapPos;
    }
}

