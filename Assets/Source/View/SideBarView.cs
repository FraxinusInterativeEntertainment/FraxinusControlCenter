using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SideBarView : UIViewBase
{
    public event Action<Toggle> GameStatusToggleChanged = delegate { };
    public event Action<Toggle> QuestToggleChanged = delegate { };
    public event Action<Toggle> ConditionToggleChanged = delegate { };
    public event Action<Toggle> McuToggleChanged = delegate { };
    public event Action<Toggle> DebugToggleChanged = delegate { };
    public event Action<Toggle> PlayerToggleChanged = delegate { };

    [SerializeField]
    private Toggle m_gameStatusToggle;
    [SerializeField]
    private Toggle m_questToggle;
    [SerializeField]
    private Toggle m_playerToggle;
    [SerializeField]
    private Toggle m_conditionToggle;
    [SerializeField]
    private Toggle m_mcuToggle;
    [SerializeField]
    private Toggle m_debugToggle;
    [SerializeField]

    void Start()
    {
        AppFacade.instance.RegisterMediator(new SideBarViewMediator(this));

        m_gameStatusToggle.onValueChanged.AddListener(delegate { GameStatusToggleChanged(m_gameStatusToggle); });
        m_questToggle.onValueChanged.AddListener(delegate { QuestToggleChanged(m_questToggle); });
        m_conditionToggle.onValueChanged.AddListener(delegate { ConditionToggleChanged(m_conditionToggle); });
        m_mcuToggle.onValueChanged.AddListener(delegate { McuToggleChanged(m_mcuToggle); });
        m_debugToggle.onValueChanged.AddListener(delegate { DebugToggleChanged(m_debugToggle); });
        m_playerToggle.onValueChanged.AddListener(delegate { PlayerToggleChanged(m_playerToggle); });
    }

    public void SetSidebarInteractable(bool value)
    {
        m_gameStatusToggle.interactable = value;
        m_questToggle.interactable = value;
        m_playerToggle.interactable = value;
        m_conditionToggle.interactable = value;
        m_mcuToggle.interactable = value;
    }
}
