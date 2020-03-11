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

    void Start()
    {
        AppFacade.instance.RegisterMediator(new SideBarViewMediator(this));

        m_gameStatusToggle.onValueChanged.AddListener(delegate { GameStatusToggleChanged(m_gameStatusToggle); });
        m_questToggle.onValueChanged.AddListener(delegate { QuestToggleChanged(m_questToggle); });
        m_conditionToggle.onValueChanged.AddListener(delegate { ConditionToggleChanged(m_conditionToggle); });
        m_mcuToggle.onValueChanged.AddListener(delegate { McuToggleChanged(m_mcuToggle); });
    }
}
