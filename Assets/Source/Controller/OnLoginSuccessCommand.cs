using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class OnLoginSuccessCommand : SimpleCommand
{
    public override void Execute(PureMVC.Interfaces.INotification notification)
    {
        SendNotification(Const.Notification.INIT_MCU);
        SendNotification(Const.Notification.REQUEST_FOR_GAME_STATUS);
    }
}
