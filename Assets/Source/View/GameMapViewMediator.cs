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
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.PLAYER_POSITIONS_UPDATED
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
            
        }
    }

    private void UpdateMapVisual()
    { 
        foreach(KeyValuePair<string, PlayerInfo> kvp in m_playerInfoProxy.GetPlayerInfos().connectedPlayers)
        {
            m_gameMapView.UpdatePlayerMapPos(kvp.Key, RearWorldToMapCoor(kvp.Value.posInfo));
        }
    }

    private Vector3 RearWorldToMapCoor(PlayerPosInfo _posInfo)
    {
        Vector3 mapPos = new Vector3(-99, -99, 0);

        mapPos.x = (int)(_posInfo.x / GameMapProxy.READ_WORLD_WIDTH * (GameMapProxy.MAP_WIDTH - 1));
        mapPos.y = (int)(_posInfo.y / GameMapProxy.REAL_WORLD_LENGTH * (GameMapProxy.MAP_LENGTH - 1));

        return mapPos;
    }
}
