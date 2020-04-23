﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class MapCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        object obj = notification.Body;
        //LoginProxy loginProxy;
        //loginProxy = Facade.RetrieveProxy(LoginProxy.NAME) as LoginProxy;
        string name = notification.Name;

        switch (name)
        {
            case Const.Notification.RECV_PLAYER_POS_INFOS:
                break;
        }
    }
}
