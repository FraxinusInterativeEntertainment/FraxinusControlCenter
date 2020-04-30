using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DebugView : UIViewBase
{
    public event Action SendWsMsg = delegate { };
    public event Action TryAddPlayer = delegate { };

    public string wsMsgVO { get; private set; }

    [SerializeField]
    private InputField m_WsMsgInputField;
    [SerializeField]
    private Button m_wsSendButton;
    [SerializeField]
    private Text m_debugText;
    [SerializeField]
    private Button m_addVirtualPlayerButton;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new DebugViewMediator(this));

        m_WsMsgInputField.onValueChanged.AddListener((string _wsMsg) => { wsMsgVO = _wsMsg; });
        m_wsSendButton.onClick.AddListener(() => { OnWsSendButton(); });
        m_addVirtualPlayerButton.onClick.AddListener(() => { OnAddVPlayerButton(); });
    }

    private void OnWsSendButton()
    {
        SendWsMsg();
    }

    private void OnAddVPlayerButton()
    {
        TryAddPlayer();
    }

    public void ShowDebugText(string debugMsg)
    {
        if (m_debugText.text.Length >= 10000)
        {
            m_debugText.text = m_debugText.text.Substring(0, 8000);
        }

        m_debugText.text = debugMsg + "\n" + m_debugText.text;
    }
}
