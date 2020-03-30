using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class McuCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        McuProxy mcu_proxy;
        mcu_proxy = Facade.RetrieveProxy(McuProxy.NAME) as McuProxy;
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.UPDATE_MCU_REQUEST:
                mcu_proxy.RequestForAllMcu();
                break;
            case Const.Notification.UPDATE_MCU_STATUS:
                mcu_proxy.UpdateMcu(obj as McuVO);
                break;
        }
    }
}
