using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class PlayerViewMediator : Mediator, IMediator
{
    public const string NAME = "PlayerViewMediator";

    private PlayerView m_playerView { get { return m_viewComponent as PlayerView; } }

    public PlayerViewMediator(PlayerView _view) : base(NAME, _view)
    {
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
        };
    }

    public override void HandleNotification(INotification _notification)
    {
        string name = _notification.Name;
        object vo = _notification.Body;

        switch (name)
        {
            case Const.Notification.ALL_CONDITION_UPDATED:
                break;
            
        }
    }

}
