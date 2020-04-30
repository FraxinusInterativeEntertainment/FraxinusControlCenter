using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestPanelView : UIViewBase
{
    public event Action SendQuestControlButtonClicked;

    public QuestControlVO questControlVO = new QuestControlVO("", QuestControlAction.move_target, "");

    [SerializeField]
    private InputField m_targetNodeInput;
    [SerializeField]
    private Button m_sendQuestControlButton;


    void Start()
    {
        AppFacade.instance.RegisterMediator(new QuestPanelViewMediator(this));

        m_sendQuestControlButton.onClick.AddListener(() => {
            questControlVO.action = QuestControlAction.move_target;
            SendQuestControlButtonClicked(); });
        m_targetNodeInput.onValueChanged.AddListener((value) => { questControlVO.targetNode = value; });
    }

    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(QuestPanelViewMediator.NAME);
    }

    public override void Show()
    {
        base.Show();
    }

    public void UpdateQuestPanel(string _groupType)
    {
        questControlVO.groupType = _groupType;
    }

}


