using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System;

public class QuestControlProxy : Proxy, IProxy
{
    public const string NAME = "QuestControlProxy";

    public QuestControlProxy() : base(NAME)
    {

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
            Debug.Log("Change Success");
        }
    }
}
