using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class ConditionProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "ConditionProxy";

    private Dictionary<string, int> m_conditions = new Dictionary<string, int>();
    public Dictionary<string, int> conditions { get { return m_conditions; } }

    public ConditionProxy() : base(NAME) { }

    public void OnResult(object _data)
    {
    }

    public void OnFault(object _data)
    {

    }

    public void UpdateConditions(Dictionary<string, int> _conditions)
    {
        m_conditions = _conditions;
        SendNotification(Const.Notification.ALL_CONDITION_UPDATED);
    }

    public void UpdateChangedCondition(ConditionVO _vo)
    {
        if (!m_conditions.ContainsKey(_vo.condition_name))
        {
            m_conditions.Add(_vo.condition_name, _vo.status);
        }
        else
        {
            m_conditions[_vo.condition_name] = _vo.status;
        }

        Debug.Log("Condition Proxy Updated: " +  _vo.condition_name + "/" + _vo.status);
    }
}

public class ConditionVO
{
    public string condition_name { get; set; }
    public int status { get; set; }

    public ConditionVO(string _name, int _status)
    {
        condition_name = _name;
        status = _status;
    }
}