using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class McuCommand : SimpleCommand
{
    public const float CONFIRM_DISCONNECT_TIME = 3f;

    McuProxy mcu_proxy;
    McuServerProxy mcuServerproxy;

    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;

        mcu_proxy = Facade.RetrieveProxy(McuProxy.NAME) as McuProxy;
        mcuServerproxy = Facade.RetrieveProxy(McuServerProxy.NAME) as McuServerProxy;
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.UPDATE_MCU_STATUS:
                mcu_proxy.UpdateMcu(obj as McuVO);
                break;
            case Const.Notification.TRY_SEND_MCU_MSG:
                McuMsgOutHandler((obj as McuMsg).McuId, (obj as McuMsg).msg);
                break;
            case Const.Notification.TRY_CONFIRM_MCU_DISCONNECTED:
                TryConfirmMcuDisconnected(obj as string);
                break;
            case Const.Notification.INIT_MCU:
                mcu_proxy.InitMcuInfos();
                mcu_proxy.InitMcuModuleInfos();
                break;
        }
    }

    private void McuMsgOutHandler(string _mcuID, string _msg)
    { 
        if (mcu_proxy.GetMcuStatus(_mcuID) == McuStatus.Connected)
        {
            mcuServerproxy.SendMsg(_mcuID, _msg);
        }
        else
        {
            mcuServerproxy.UnsendMsgHandler(_mcuID, _msg);
        }
    }

    private void TryConfirmMcuDisconnected(string _mcuID)
    {
        if (mcu_proxy.GetMcuStatus(_mcuID) == McuStatus.Connected)
        {
            SendNotification(Const.Notification.UPDATE_MCU_STATUS, new McuVO(_mcuID, McuStatus.Unknown));
            mcuServerproxy.SendAreYouOK(_mcuID);

            Timer.Instance.AddTimerTask(CONFIRM_DISCONNECT_TIME, () =>
            {
                if (mcu_proxy.GetMcuStatus(_mcuID) != McuStatus.Connected)
                {
                    mcuServerproxy.OnClientDisconnected(mcuServerproxy.GetClientByID(_mcuID));
                }
            });
        }
    }
}
