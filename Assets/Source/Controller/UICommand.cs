using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class UICommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;

        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.SHOW_MAIN_PANEL_CONTENT:
                UIManager.instance.ShowMainPanelContent(obj as string);
                break;
            case Const.Notification.SHOW_POPUP:
                UIManager.instance.ShowPopup(obj as PopupInfoVO);
                break;
        }
    }
}