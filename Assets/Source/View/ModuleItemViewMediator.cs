using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class ModuleItemViewMediator : Mediator, IMediator
{
    public const string NAME = "ModuleItemViewMediator";

    protected ModuleItemView m_moduleItemView { get { return m_viewComponent as ModuleItemView; } }

    public ModuleItemViewMediator(ModuleItemView _view, string _name) : base(_name, _view)
    {
        m_moduleItemView.OnSendSignalButtonClicked += TrySendControlSignal;
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

        switch (name)
        {
        }
    }

    private void TrySendControlSignal()
    {
        SendNotification(Const.Notification.WS_SEND, new SensorMessage(m_moduleItemView.controlSignalVO.moduleID,
                                                                       m_moduleItemView.controlSignalVO.value.ToString()));
    }
}
