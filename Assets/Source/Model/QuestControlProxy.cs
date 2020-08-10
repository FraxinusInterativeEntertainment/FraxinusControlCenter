using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System;

public class QuestControlProxy : Proxy, IProxy
{
    public const string NAME = "QuestControlProxy";

    private Dictionary<string, QuestVO> m_questVO = new Dictionary<string, QuestVO>();
    public Dictionary<string, QuestVO> questInfos { get { return m_questVO; } }

    private Dictionary<string, GroupInfoVO> m_groupInfos = new Dictionary<string, GroupInfoVO>();
    public Dictionary<string, GroupInfoVO> groupInfos { get { return m_groupInfos; } }
    public QuestControlProxy() : base(NAME)
    {

    }
    public void TryGetAllGroupInfo()
    {
        HttpService getAllGroupInfos = new HttpService(Const.Url.GET_ALL_GROUP_INFOS, HttpRequestType.Get);
        getAllGroupInfos.SendRequest<AllGroupInfosResponse>(AllGroupInfosCallBack);
    }
    private void AllGroupInfosCallBack(AllGroupInfosResponse _response)
    {
        if (_response.err_code == 0)
        {
            for (int i = 0; i < _response.group_infos.Length; i++)
            {
                if (!m_groupInfos.ContainsKey(_response.group_infos[i].name))
                {
                    m_groupInfos.Add(_response.group_infos[i].name, _response.group_infos[i]);
                }
            }
            SendNotification(Const.Notification.RECV_ALL_GROUP_NAME, groupInfos);
        }
        else
        {
            SendNotification(Const.Notification.DEBUG_LOG, "Have No GroupInfos");
        }
    }
 
    public void TryChangeQuestNode(QuestControlVO _vo)
    {
        WWWForm form = new WWWForm();
        form.AddField("group_type", _vo.groupType);
        form.AddField("action", Enum.GetName(typeof(QuestControlAction), _vo.action));
        form.AddField("target", _vo.targetNode);

        HttpService changeQuestNodeService = new HttpService(Const.Url.CHANGE_GROUP_QUEST_NODE, HttpRequestType.Post, form);
        changeQuestNodeService.SendRequest<HttpResponse>(ChangeQuestNodeCallback);
    }

    private void ChangeQuestNodeCallback(HttpResponse _response)
    {
        if (_response.err_code != 0)
        {
            SendNotification(Const.Notification.WARNING_POPUP, _response.err_msg);
        }
        else
        {
            SendNotification(Const.Notification.CHANGE_PLAYER_GROUP_SUCCESS);
        }
    }
    public void UpdateQuestInfos(QuestVO _vo)
    {
        if (m_questVO.ContainsKey(_vo.group_name))
        {
            m_questVO[_vo.group_name] = _vo;
        }
        else
        {
            m_questVO.Add(_vo.group_name, new QuestVO(_vo.group_name, _vo.quest_node_name, _vo.title, _vo.desc, _vo.loc, _vo.character, _vo.expected_time));
        }
        SendNotification(Const.Notification.UPDATE_QUEST_INFOS,m_questVO);
    }
}
public class QuestVO
{
    public string group_name { get; set; }
    public string quest_node_name { get; set; }
    public string title { get; set; }
    public string  desc { get; set; }
    public string loc { get; set; }

    public string character { get; set; }

    public float expected_time { get; set; }

    public QuestVO(string _groupName, string _nodeName,string _title,string _desc, string _loc, string _character,float _expectedTime)
    {
        group_name = _groupName;
        quest_node_name = _nodeName;
        title = _title;
        desc = _desc;
        loc = _loc;
        character = _character;
        expected_time = _expectedTime;
    }
}
