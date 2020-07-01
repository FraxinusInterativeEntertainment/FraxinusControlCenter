using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine.UI;

public class QuestPanelViewMediator : Mediator, IMediator
{
    public const string NAME = "SideBarViewMediator";

    protected QuestPanelView m_questPanelView { get { return m_viewComponent as QuestPanelView; } }

    public QuestPanelViewMediator(QuestPanelView _view) : base(NAME, _view)
    {
        m_questPanelView.SendQuestControlButtonClicked += TryChangeQuestNode;
        m_questPanelView.LastNodeControlButtonClicked += TryChangeQuestNode;
        m_questPanelView.NextNodeControlButtonClicked += TryChangeQuestNode;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;

        switch (name)
        {
        }
    }

    private void TryChangeQuestNode()
    {
        //TODO: 加入接口后移除
        Debug.Log(m_questPanelView.questControlVO.groupType + " " + 
                  System.Enum.GetName(typeof(QuestControlAction), m_questPanelView.questControlVO.action) + " " +
                  m_questPanelView.questControlVO.targetNode);

        SendNotification(Const.Notification.TRY_CHANGE_QUEST_NODE, m_questPanelView.questControlVO);
    }
}


