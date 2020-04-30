using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System;

public class ErrorHandlerCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.DEBUG_LOG:
                Debug.Log("Debug Log: " + obj as string);
                break;
            case Const.Notification.WARNING_POPUP:
                Debug.Log("Warning Popup: " + obj as string);
                PopupInfoVO vo = new PopupInfoVO("错误", obj as string, "确认", true);
                SendNotification(Const.Notification.SHOW_POPUP, vo);
                break;
            case Const.Notification.CUSTOMIZED_POPUP:
                Debug.Log("Warning Popup: " + obj as string);
                SendNotification(Const.Notification.SHOW_POPUP, obj as PopupInfoVO);
                break;
        }
    }
}
