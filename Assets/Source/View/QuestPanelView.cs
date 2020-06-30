using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestPanelView : UIViewBase
{
    public event Action SendQuestControlButtonClicked;
    public event Action LastNodeControlButtonClicked;
    public event Action NextNodeControlButtonClicked;

    public QuestControlVO questControlVO = new QuestControlVO("", QuestControlAction.move_target, "");
    [SerializeField]
    private Button m_lastNodeButton;
    [SerializeField]
    private Button m_nextNodeButton;
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
        m_lastNodeButton.onClick.AddListener(() => { questControlVO.action = QuestControlAction.move_back;
            questControlVO.targetNode = ""; LastNodeControlButtonClicked(); });
        m_nextNodeButton.onClick.AddListener(() => { questControlVO.action = QuestControlAction.move_forward;
            questControlVO.targetNode = ""; NextNodeControlButtonClicked(); }) ;
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


