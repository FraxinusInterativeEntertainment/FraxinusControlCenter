using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class GameMapViewMediator : Mediator, IMediator
{
    public const string NAME = "GameMapViewMediator";
    public const float MAP_RATIO = 100f;

    private GameMapView m_gameMapView { get { return m_viewComponent as GameMapView; } }
    private PlayerInfoProxy m_playerInfoProxy;
    private GameMapProxy m_gameMapProxy;

    public GameMapViewMediator(GameMapView _view) : base(NAME, _view)
    {
        m_playerInfoProxy = AppFacade.instance.RetrieveProxy(PlayerInfoProxy.NAME) as PlayerInfoProxy;
        m_gameMapProxy = AppFacade.instance.RetrieveProxy(GameMapProxy.NAME) as GameMapProxy;

        m_gameMapView.PlayerUidToggleChanged += OnPlayerUidToggle;
        m_gameMapView.PlayerNicknameToggleChanged += OnPlayerNicknameToggle;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.PLAYER_POSITIONS_UPDATED,
            Const.Notification.PLAYER_LIST_UPDATED
        };
    }

    public override void HandleNotification(INotification _notification)
    {
        string name = _notification.Name;
        object vo = _notification.Body;

        switch (name)
        {
            case Const.Notification.PLAYER_POSITIONS_UPDATED:
                UpdateMapVisual();
                break;
            case Const.Notification.PLAYER_LIST_UPDATED:
                RefreshPlayerBeacons();
                break;
        }
    }

    private void UpdateMapVisual()
    { 
        foreach(KeyValuePair<string, PlayerInfo> kvp in m_playerInfoProxy.GetPlayerInfos().connectedPlayers)
        {
            m_gameMapView.mapBeaconManager.UpdatePlayerBeacon(kvp.Value);
        }
    }

    private void RefreshPlayerBeacons()
    {
        m_gameMapView.mapBeaconManager.ClearPlayerBeacons();
    }

    private void OnPlayerUidToggle(bool _value)
    {
        m_gameMapView.mapBeaconManager.ShowPlayerUid(_value);
    }

    private void OnPlayerNicknameToggle(bool _value)
    {
        m_gameMapView.mapBeaconManager.ShowPlayerNickname(_value);
    }
}
