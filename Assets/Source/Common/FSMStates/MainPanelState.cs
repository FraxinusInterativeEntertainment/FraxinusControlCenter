using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelState : FSMState
{
    public MainPanelState(int _id, FSMSystem _fsmSystem) : base(_id, _fsmSystem) { }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();

        //AppFacade.instance.SendNotification(Const.Notification.REQUEST_FOR_GAME_STATUS);

        //AppFacade.instance.SendNotification(Const.Notification.LOAD_SCENE, new SceneVO(Const.SceneIndex.MAIN_PANEL_SCENE));
    }
}
