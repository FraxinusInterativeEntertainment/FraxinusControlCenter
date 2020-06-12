using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameMapView : UIViewBase
{
    public Image visualMap { get; private set; }
    public MapBeaconManager mapBeaconManager { get; private set; }

    [SerializeField]
    private GameObject m_playerBeaconPrefab;
    [SerializeField]
    private GameObject m_playerBeaconContainer;
    private readonly Dictionary<string, PlayerMapBeacon> m_playerBeacons = new Dictionary<string, PlayerMapBeacon>();

    void OnEnable()
    {
        AppFacade.instance.RegisterMediator(new GameMapViewMediator(this));
        Show();

        GameMapConfig mapConfig = new GameMapConfig(GameMapProxy.REAL_WORLD_WIDTH,
                                                    GameMapProxy.REAL_WORLD_LENGTH,
                                                    GameMapProxy.MAP_WIDTH,
                                                    GameMapProxy.MAP_LENGTH);
        mapBeaconManager = new MapBeaconManager(m_playerBeaconContainer, m_playerBeaconPrefab, mapConfig);
    }

    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(GameMapViewMediator.NAME);
    }
}


