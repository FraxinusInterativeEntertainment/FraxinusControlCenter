using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class ConditionCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        ConditionProxy conditionProxy;
        conditionProxy = Facade.RetrieveProxy(ConditionProxy.NAME) as ConditionProxy;
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.RECV_ALL_GAME_CONDITIONS:
                conditionProxy.UpdateConditions(obj as Dictionary<string, int>);
                break;
            case Const.Notification.RECV_GAME_CONDITION_CHANGE:
                conditionProxy.UpdateChangedCondition(obj as ConditionVO);
                break;
        }
    }
}
