using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine.SceneManagement;


public class MainFSMCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.LOGIN_SUCCESS:
                SendNotification(Const.Notification.CONNECT_TO_WS_SERVER, obj);
                SendNotification(Const.Notification.REQUEST_FOR_GAME_STATUS);
                SendNotification(Const.Notification.INIT_MCU);
                SendNotification(Const.Notification.SHOW_MAP_PANEL_CONTENT, Const.UIFormNames.GAME_MAP_FORM);
                SendNotification(Const.Notification.SHOW_MAIN_PANEL_CONTENT, Const.UIFormNames.GAME_STATUS_FORM);
                SendNotification(Const.Notification.UNLOCK_UI, "");
                break;
            case Const.Notification.LOGOUT_SUCCESS:
                SendNotification(Const.Notification.LOCK_UI, "未登录");
                break;
        }
    }
}
