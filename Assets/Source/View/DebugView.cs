using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DebugView : UIViewBase
{
    public event Action SendWsMsg = delegate { };

    public string wsMsgVO { get; private set; }

    [SerializeField]
    private InputField m_WsMsgInputField;
    [SerializeField]
    private Button m_wsSendButton;
    [SerializeField]
    private Text m_debugText;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new DebugViewMediator(this));

        m_WsMsgInputField.onValueChanged.AddListener((string _wsMsg) => { wsMsgVO = _wsMsg; });
        m_wsSendButton.onClick.AddListener(() => { OnWsSendButton(); });
    }

    private void OnWsSendButton()
    {
        SendWsMsg();
    }

    public void ShowDebugText(string debugMsg)
    {
        m_debugText.text = debugMsg;
    }
}
