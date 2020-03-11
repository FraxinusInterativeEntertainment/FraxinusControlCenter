using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConditionView : UIViewBase
{
    public event Action<ConditionVO> TrySubmitNewValue;

    private readonly Dictionary<string, ConditionItem> m_conditionItems = new Dictionary<string, ConditionItem>();

    [SerializeField]
    private GameObject m_conditionItemContainer;
    [SerializeField]
    private GameObject m_conditionItemPrefab;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new ConditionViewMediator(this));
    }
    
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(ConditionViewMediator.NAME);
    }

    public void ClearContainer()
    {
        while(m_conditionItemContainer.transform.childCount > 0)
        {
            Destroy(m_conditionItemContainer.transform.GetChild(0).gameObject);
        }

        m_conditionItems.Clear();
    }

    public void UpdateConditionItem(ConditionVO _vo)
    {
        if (!m_conditionItems.ContainsKey(_vo.condition_name))
        {
            GameObject conditionItemGO = Instantiate(m_conditionItemPrefab);
            conditionItemGO.transform.SetParent(m_conditionItemContainer.transform);
            ConditionItem conditionItem = conditionItemGO.GetComponent<ConditionItem>();
            conditionItem.Init(_vo).AddOnEditedListener((_newValue) => { TrySubmitNewValue(_newValue); });
            m_conditionItems.Add(_vo.condition_name, conditionItem);
        }
        else
        {
            m_conditionItems[_vo.condition_name].SetName(_vo.condition_name);
            m_conditionItems[_vo.condition_name].SetValue(_vo.status);
        }
    }

    public void OnConditionValueEdit(ConditionVO _vo)
    {
        TrySubmitNewValue(_vo);
    }
}
