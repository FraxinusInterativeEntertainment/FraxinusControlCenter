using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameState : FSMState
{
    public PreGameState(int _id, FSMSystem _fsmSystem) : base(_id, _fsmSystem) { }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();

        //AppFacade.instance.SendNotification(Const.Notification.LOAD_SCENE, new SceneVO(Const.SceneNames.PRE_GAME_SCENE));
        AppFacade.instance.SendNotification(Const.Notification.LOAD_UI_FORM, Const.UIFormNames.PRE_GAME_FORM);

        Debug.Log("Enter Pre game State");
    }
}
