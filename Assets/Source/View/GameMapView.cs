using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameMapView : UIViewBase
{
    public event Action<bool> PlayerUidToggleChanged;
    public event Action<bool> PlayerNicknameToggleChanged;

    public Image visualMap { get; private set; }
    public MapBeaconManager mapBeaconManager { get; private set; }

    [SerializeField]
    private GameObject m_playerBeaconPrefab;
    [SerializeField]
    private GameObject m_playerBeaconContainer;
    private readonly Dictionary<string, PlayerMapBeacon> m_playerBeacons = new Dictionary<string, PlayerMapBeacon>();

    [SerializeField]
    private Toggle m_playerUidToggle;
    [SerializeField]
    private Toggle m_playerNicknameToggle;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new GameMapViewMediator(this));
        Show();

        GameMapConfig mapConfig = new GameMapConfig(GameMapProxy.REAL_WORLD_WIDTH,
                                                    GameMapProxy.REAL_WORLD_LENGTH,
                                                    GameMapProxy.MAP_WIDTH,
                                                    GameMapProxy.MAP_LENGTH);
        GroupColorConfig colorConfig = new GroupColorConfig();
        colorConfig.AddGroupColor(GameGroup.PlayerDefault, Const.GroupColor.DEFAULT_GROUP);

        mapBeaconManager = new MapBeaconManager(this, mapConfig, colorConfig);

        m_playerUidToggle.onValueChanged.AddListener((_value) => { PlayerUidToggleChanged(_value); });
        m_playerNicknameToggle.onValueChanged.AddListener((_value) => { PlayerNicknameToggleChanged(_value); });
    }

    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(GameMapViewMediator.NAME);
    }

    public PlayerMapBeacon GeneratePlayerMapBeacon()
    {
        //TODO: 用工厂处理处理这几百个indicator的复用
        return Instantiate(m_playerBeaconPrefab, m_playerBeaconContainer.transform).GetComponent<PlayerMapBeacon>();
    }
}


