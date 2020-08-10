using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine.UI;

public class QuestControlViewMediator : Mediator, IMediator
{
    public const string NAME = "QuestControlViewMediator";
    private QuestControlProxy m_questControlProxy;

    protected QuestControlView m_questControlView { get { return m_viewComponent as QuestControlView; } }

    public QuestControlViewMediator(QuestControlView _view) : base(NAME, _view)
    {
        m_questControlProxy = Facade.RetrieveProxy(QuestControlProxy.NAME) as QuestControlProxy;
        m_questControlView.RequestGroupName += TryRequestGroupName;
        m_questControlView.OnTimelineToggle += OnTimelineToggle;
        m_questControlView.OnQuestToggle += OnQuestToggle;
        m_questControlView.OnQuestInfo += OnQuestInfo;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.RECV_ALL_GROUP_NAME,
            Const.Notification.RECV_GAME_QUEST_INFO
        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;

        switch (name)
        {
            case Const.Notification.RECV_ALL_GROUP_NAME:
                InitQuestViewInfos(vo as Dictionary<string, GroupInfoVO>);
                break;
            case Const.Notification.UPDATE_QUEST_INFOS:
                UpdateGameTimes((vo as QuestVO).group_name);
                break;
        }
    }
    Dictionary<string, GroupInfoVO> m_groupNames = new Dictionary<string, GroupInfoVO>();
    private void InitQuestViewInfos(Dictionary<string, GroupInfoVO> _groupNames)
    {
        m_groupNames = _groupNames;
        Color[] colors = new Color[] { Color.green, Color.red, Color.blue, Color.yellow, Color.gray, Color.white, Color.black, Color.cyan };
        int colorNum = 0;
        foreach (string item in _groupNames.Keys)
        {
            m_questControlView.UpdateAllGroupInfos(_groupNames[item]);
            m_questControlView.UpdateGameTime(_groupNames[item], colors[colorNum]);
            if (m_questControlProxy.questInfos.ContainsKey(item))
            {
                m_questControlView.UpdateGameTimeInfos(item, m_questControlProxy.questInfos[item].expected_time);
            }
            colorNum++;
        }
    }

    private void UpdateGameTimes(string _infos)
    {
        if (m_groupNames.ContainsKey(_infos))
        {
            m_questControlView.UpdateGameTimeInfos(_infos, m_questControlProxy.questInfos[_infos].expected_time);
        }
    }
    private void TryRequestGroupName()
    {
        SendNotification(Const.Notification.REQUEST_GROUP_NAME);
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
    private void OnQuestInfo(string _name)
    {
        if (!m_questControlProxy.questInfos.ContainsKey(_name))
        {
            m_questControlView.ShowQuestInfoName(_name);
            SendNotification(Const.Notification.TRY_CHANGE_QUEST_NODE, new QuestControlVO("", QuestControlAction.move_target, _name));
        }
       /* QuestVO questVO = new QuestVO
            (_name, m_questControlProxy.questInfos[_name].title, 
            m_questControlProxy.questInfos[_name].loc,
            m_questControlProxy.questInfos[_name].desc,
            m_questControlProxy.questInfos[_name].character,
            m_questControlProxy.questInfos[_name].quest_node_name);
        */
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