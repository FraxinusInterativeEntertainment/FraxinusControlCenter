using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class ErrorHandlerCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.DEBUG_LOG:
                //Debug.Log("Debug Log: " + obj as string);
                break;
            case Const.Notification.WARNING_POPUP:
                //Debug.Log("Warning Popup: " + obj as string);
                break;
        }
    }
}
