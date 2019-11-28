using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainView : MonoBehaviour
{
    public event Action WsSend = delegate { };

    public string wsMessageVO;

    [SerializeField]
    private InputField m_wsSend;
    [SerializeField]
    private Button m_wsSendButton;
    
    void Start()
    {
        AppFacade.instance.RegisterMediator(new MainViewMediator(this));

        m_wsSend.onValueChanged.AddListener((string message) => { wsMessageVO = message; });
        m_wsSendButton.onClick.AddListener(() => { WsSend(); });
    }
}
