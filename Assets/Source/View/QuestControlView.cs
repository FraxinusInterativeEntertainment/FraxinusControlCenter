using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestControlView : UIViewBase
{
    public event Action<Toggle> OnTimelineToggle = delegate { };
    public event Action<Toggle> OnQuestToggle = delegate { };

    [SerializeField]
    private Toggle m_timelineToggle;
    [SerializeField]
    private Toggle m_questToggle;
    [SerializeField]
    private GameObject m_timelinePanel;
    [SerializeField]
    private QuestPanelView m_questPanel;


    void Start()
    {
        AppFacade.instance.RegisterMediator(new QuestControlViewMediator(this));

        m_timelineToggle.onValueChanged.AddListener(delegate { OnTimelineToggle(m_timelineToggle); });
        m_questToggle.onValueChanged.AddListener((isOn) => { OnQuestToggle(m_questToggle); });
        OnTimelineToggle(m_timelineToggle);
    }

    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(QuestControlViewMediator.NAME);
    }

    public override void Show()
    {
        base.Show();

        //TODO: Remove after testing
        m_questPanel.UpdateQuestPanel("a");
    }

    public void ShowTimelinePanel()
    {
        m_timelinePanel.SetActive(true);
        m_questPanel.gameObject.SetActive(false);
    }

    public void ShowQuestPanel()
    {
        m_timelinePanel.SetActive(false);
        m_questPanel.gameObject.SetActive(true);
    }


}


