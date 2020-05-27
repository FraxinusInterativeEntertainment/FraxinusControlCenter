using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine.UI;

public class QuestControlViewMediator : Mediator, IMediator
{
    public const string NAME = "SideBarViewMediator";

    protected QuestControlView m_questControlView { get { return m_viewComponent as QuestControlView; } }

    public QuestControlViewMediator(QuestControlView _view) : base(NAME, _view)
    {
        m_questControlView.OnTimelineToggle += OnTimelineToggle;
        m_questControlView.OnQuestToggle += OnQuestToggle;
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

    private void OnTimelineToggle(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            _toggle.interactable = false;
            m_questControlView.ShowTimelinePanel();
        }
        else
        {
            _toggle.interactable = true;
        }
    }

    private void OnQuestToggle(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            _toggle.interactable = false;
            m_questControlView.ShowQuestPanel();
        }
        else
        {
            _toggle.interactable = true;
        }
    }

}


public class QuestControlVO
{
    public string groupType { get; set; }
    public QuestControlAction action { get; set; }
    public string targetNode { get; set; }

    public QuestControlVO(string _groupType, QuestControlAction _action, string _targetNode)
    {
        groupType = _groupType;
        action = _action;
        targetNode = _targetNode;
    }
}

public enum QuestControlAction
{
    move_back,
    move_forward, 
    move_target
}