using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerView : UIViewBase
{

    void Start()
    {
        AppFacade.instance.RegisterMediator(new PlayerViewMediator(this));


        Show();
    }

    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(PlayerViewMediator.NAME);
    }

}


