using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class GameStatusViewMediator : Mediator, IMediator
{
    public const string NAME = "GameStatusViewMediator";

    protected GameStatusView m_gameStatusView { get { return m_viewComponent as GameStatusView; } }

    public GameStatusViewMediator(GameStatusView _view) : base(NAME, _view)
    {
        _view.ChangeGameStatus += ChangeGameStatusHandler;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Constants.Notification.GAME_STATUS_CHANGED,
            Constants.Notification.GAME_STATUS_CHANGE_ERROR
        };
    }

    public override void HandleNotification(INotification _notification)
    {
        string name = _notification.Name;
        object vo = _notification.Body;

        switch (name)
        {
            case Constants.Notification.GAME_STATUS_CHANGED:
                Debug.Log("Changed: " + (vo as ChangeGameStatusResponse).device_id_2_user_info);
                break;
            case Constants.Notification.GAME_STATUS_CHANGE_ERROR:
                Debug.Log(vo as string);
                break;
        }
    }

    private void ChangeGameStatusHandler()
    {
        SendNotification(Constants.Notification.CHANGE_GAME_STATUS, m_gameStatusView.gameStatusVO);
    }
}
