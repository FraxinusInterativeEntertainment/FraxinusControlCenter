using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : FSMState
{
    public InGameState(int _id, FSMSystem _fsmSystem) : base(_id, _fsmSystem) { }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();

        AppFacade.instance.SendNotification(Const.Notification.LOAD_UI_FORM, Const.UIFormNames.PRE_GAME_FORM);

        Debug.Log("Enter Ingame State");
    }
}

