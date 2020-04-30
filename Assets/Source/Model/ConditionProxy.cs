using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class ConditionProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "ConditionProxy";

    private Dictionary<string, ConditionVO> m_conditions = new Dictionary<string, ConditionVO>();
    public Dictionary<string, ConditionVO> conditions { get { return m_conditions; } }

    public ConditionProxy() : base(NAME) { }

    public void OnResult(object _data)
    {
        SendNotification(Const.Notification.RECV_ALL_GAME_CONDITIONS, _data);
    }

    public void OnFault(object _data)
    {

    }
    public void RequestAllCondition()
    {
        ConditionInfoDelegate conditionInfoDelegate = new ConditionInfoDelegate(this);
        conditionInfoDelegate.RequestAllConditionInfo();
    }
    public void UpdateConditions(Dictionary<string, ConditionVO> _conditions)
    {
        m_conditions = _conditions;
        SendNotification(Const.Notification.ALL_CONDITION_UPDATED);
    }
    public void UpdateChangedCondition(ConditionVO _vo)
    {
        if (m_conditions.ContainsKey(_vo.condition_name))
        {
            m_conditions[_vo.condition_name] = _vo;
        }
        Debug.Log("Condition Proxy Updated: " +  _vo.condition_name + "/" + _vo.status);
        SendNotification(Const.Notification.DEBUG_LOG, "Condition Proxy Updated: " + _vo.condition_name + "/" + _vo.status);
    }
}

public class ConditionVO
{
    public string condition_name { get; set; }
    public int status { get; set; }

    public string desc { get; set; }
    public  string title { get; set; }

    public ConditionVO(string _name, int _status,string _desc,string _title)
    {
        condition_name = _name;
        status = _status;
        desc = _desc;
        title = _title;
    }
}
public class AllConditioResponsen : HttpResponse
{
    public Dictionary<string, ConditionVO> condition_infos;
    public AllConditioResponsen(int _errCode, string _errMsg) : base(_errCode, _errMsg) { }
}