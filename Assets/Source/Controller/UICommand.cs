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
            case Const.Notification.LOAD_UI_FORM:
                UIManager.instance.ShowForm(obj as string);
                break;

        }
    }
}