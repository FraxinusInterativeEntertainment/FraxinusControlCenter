using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameState : FSMState
{
    public PreGameState(int _id, FSMSystem _fsmSystem) : base(_id, _fsmSystem) { }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();

        //AppFacade.instance.SendNotification(Const.Notification.LOAD_SCENE, new SceneVO(Const.SceneIndex.MAIN_PANEL_SCENE));

        Debug.Log("Pre game");
    }
}
