using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class ConditionViewMediator : Mediator, IMediator
{
    public const string NAME = "ConditionViewMediator";

    private ConditionProxy m_conditionProxy;

    protected ConditionView m_conditionView { get { return m_viewComponent as ConditionView; } }

    public ConditionViewMediator(ConditionView _view) : base(NAME, _view)
    {
        m_conditionProxy = Facade.RetrieveProxy(ConditionProxy.NAME) as ConditionProxy;

        m_conditionView.TrySubmitNewValue += OnTrySubmitNewValue;

        UpdateAllConditions();
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.ALL_CONDITION_UPDATED,
            Const.Notification.RECV_GAME_CONDITION_CHANGE
        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;

        switch (name)
        {
            case Const.Notification.ALL_CONDITION_UPDATED:
                UpdateAllConditions();
                break;
            case Const.Notification.RECV_GAME_CONDITION_CHANGE:
                UpdateCondition(vo as ConditionVO);
                break;
        }
    }

    private void UpdateAllConditions()
    {
        foreach(KeyValuePair<string, ConditionVO> condition in m_conditionProxy.conditions)
        {
            m_conditionView.UpdateConditionItem(new ConditionVO(condition.Key, condition.Value.status, condition.Value.desc, condition.Value.title));
        }
    }

    private void UpdateCondition(ConditionVO _vo)
    {
        m_conditionView.UpdateConditionItem(_vo);
    }

    private void OnTrySubmitNewValue(ConditionVO _vo)
    {
        Debug.Log("Try submit condition: " + _vo.condition_name + "/" + _vo.status);
        //TODO: 接入Http Condition改变的接口（set_game_condition/）
    }
}
