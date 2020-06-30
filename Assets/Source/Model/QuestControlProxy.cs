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
    public QuestControlProxy() : base(NAME)
    {

    }
    // GroupNameTest：
    List<string> groupName = new List<string>();
    private void GroupNameTest()
    {
        groupName.Add("大故事");
        groupName.Add("女皇线");
        groupName.Add("木山今线");
        groupName.Add("情缘线");
        groupName.Add("瘟疫线");
        groupName.Add("宗教线");
        groupName.Add("抵抗组织");
        groupName.Add("侦探线");
    }
    public void TryGetAllGroupName()
    {
        //TODO:Wait For Port
        HttpService getAllGroupName = new HttpService(Const.Url.GET_ALL_GROUP_NAME, HttpRequestType.Get);
        //getAllGroupName.SendRequest<HttpResponse>(AllGroupNameCallBack);
        GroupNameTest();
        SendNotification(Const.Notification.RECV_ALL_GROUP_NAME, groupName);
    }
    private void AllGroupNameCallBack(HttpResponse _response)
    {
        if (_response.err_code != 0)
        {
            SendNotification(Const.Notification.RECV_ALL_GROUP_NAME, _response.err_msg);
        }
        else
        {
            Debug.Log("Have Not GroupName");
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
            //TODO:Show TargetNode Info
            Debug.Log("Change Success");
            SendNotification(Const.Notification.CHANGE_PLAYER_GROUP_SUCCESS);
        }
    }
    public void UpdateQuestInfos(QuestVO _vo)
    {
        if (m_questVO.ContainsKey(_vo.group_name))
        {
            m_questVO[_vo.group_name] = _vo;
        }
    }
}
public class QuestVO
{
    public string group_name { get; set; }
    public string title { get; set; }
    public string loc { get; set; }

    public string  desc { get; set; }
    public string character { get; set; }

    public string quest_node_name { get; set; }
    


    public QuestVO(string _groupName,string _title,string _loc,string _desc,string _character, string _nodeName)
    {
        group_name = _groupName;
        title = _title;
        loc = _loc;
        desc = _desc;
        character = _character;
        quest_node_name = _nodeName;
    }
}
