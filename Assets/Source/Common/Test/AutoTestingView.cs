using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AutoTestingView : UIViewBase
{
    public event Action RunAutoTesting;

    public Button startTestingButton;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new AutoTestingViewMediator(this));

        startTestingButton.onClick.AddListener(() => { RunAutoTesting(); });
    }
    
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(AutoTestingViewMediator.NAME);
    }

}
