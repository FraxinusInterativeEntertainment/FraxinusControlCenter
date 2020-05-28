using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class McuViewMediator : Mediator, IMediator
{
    public const string NAME = "McuViewMediator";

    private McuProxy m_mcuProxy;

    protected McuView m_mcuView { get { return m_viewComponent as McuView; } }

    public McuViewMediator(McuView _view) : base(NAME, _view)
    {
        m_mcuProxy = Facade.RetrieveProxy(McuProxy.NAME) as McuProxy;
        m_mcuView.OnMcuViewInit += UpdateAllMcu;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.ALL_MCU_UPDATED,
            Const.Notification.UPDATE_MCU_ITEM
        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;

        switch (name)
        {
            case Const.Notification.ALL_MCU_UPDATED:
                UpdateAllMcu();
                break;
            case Const.Notification.UPDATE_MCU_ITEM:
                m_mcuView.UpdateMcuItem(vo as McuVO);
                break;
        }
    }

    private void UpdateAllMcu()
    {
        foreach (KeyValuePair<string, McuVO> mcuItem in m_mcuProxy.mcu)
        {
            m_mcuView.UpdateMcuItem(mcuItem.Value);
        }
    }

    private void OnMcuConnected(McuVO _vo)
    {
        m_mcuView.UpdateMcuStatus(_vo);
    }

    private void OnMcudisConnected(McuVO _vo)
    {
        m_mcuView.UpdateMcuStatus(_vo);
    }
}
