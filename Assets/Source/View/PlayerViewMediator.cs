using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class PlayerViewMediator : Mediator, IMediator
{
    public const string NAME = "PlayerViewMediator";
    private PlayerInfoProxy m_playerInfoProxy;
    private PlayerView m_playerView { get { return m_viewComponent as PlayerView; } }

    private  Dictionary<string, PlayerInfo> newPlayers = new Dictionary<string, PlayerInfo>();

    public PlayerViewMediator(PlayerView _view) : base(NAME, _view)
    {
        m_playerInfoProxy = Facade.RetrieveProxy(PlayerInfoProxy.NAME) as PlayerInfoProxy;
        m_playerView.InitPlayerView += TryInitPlayerView;
    }
    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.RECV_PLAYER_INFO
        };
    }

    public override void HandleNotification(INotification _notification)
    {
        string name = _notification.Name;
        object vo = _notification.Body;

        switch (name)
        {
            case Const.Notification.RECV_PLAYER_INFO:
                UpdatePlayerInfo(vo as Dictionary<string, PlayerInfo>);
                break;
        }
    }
    private void TryInitPlayerView()
    {
        foreach (KeyValuePair<string, PlayerInfo> kvp in m_playerInfoProxy.GetPlayerInfos().connectedPlayers)
        {
            m_playerView.UpdatePlayerInfo(kvp.Value);
        }
    }
    private void UpdatePlayerInfo(Dictionary<string, PlayerInfo> _connectedPlayers)
    {
        foreach (KeyValuePair<string, PlayerInfo> kvp in _connectedPlayers)
        {
            m_playerView.UpdatePlayerInfo(kvp.Value);
        }
    }
}
