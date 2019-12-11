using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;


public class MainFSMCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.LOGIN_SUCCESS:
                GameManager.instance.ChangeMainFSMState(MainFSMStateID.InGame);
                break;
            case Const.Notification.RECEIVED_GAME_STATUS:
                if ((obj as GameStatusVO).gameStatus == GameStatus.s)
                {
                    GameManager.instance.ChangeMainFSMState(MainFSMStateID.InGame);
                }
                else if ((obj as GameStatusVO).gameStatus == GameStatus.p)
                {
                    GameManager.instance.ChangeMainFSMState(MainFSMStateID.PreGame);
                }
                break;
        }
    }
}
