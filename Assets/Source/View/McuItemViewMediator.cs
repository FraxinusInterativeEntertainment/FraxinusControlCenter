using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class McuItemViewMediator : Mediator, IMediator
{
    public const string NAME = "McuItemViewMediator";

    protected McuItemView m_mcuItemView { get { return m_viewComponent as McuItemView; } }

    private McuProxy m_mcuProxy;

    public McuItemViewMediator(McuItemView _view, string _name) : base(_name, _view)
    {
        m_mcuProxy = Facade.RetrieveProxy(McuProxy.NAME) as McuProxy;
        m_mcuItemView.TryLoadModules += OnTryLoadModules;
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

    private void OnTryLoadModules()
    {
        foreach (McuModule moduleVO in m_mcuProxy.mcu[m_mcuItemView.mcuVO.mcuName].modules)
        {
            m_mcuItemView.LoadModule(moduleVO);
        }

    }
}
