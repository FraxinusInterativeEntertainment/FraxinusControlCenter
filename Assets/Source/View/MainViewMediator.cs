using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class MainViewMediator : Mediator, IMediator
{
    public const string NAME = "MainViewMediator";

    protected MainView m_mainView { get { return m_viewComponent as MainView; } }

    public MainViewMediator(MainView _view) : base(NAME, _view)
    {
        m_mainView.WsSend += OnWsSend;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;
        
    }

    private void OnWsSend()
    {
        SendNotification(Constants.Notification.WS_SEND, m_mainView.wsMessageVO);
    }
}
