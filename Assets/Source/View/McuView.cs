using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class McuView : UIViewBase
{

    [SerializeField]
    private GameObject m_mcuItemContainer;
    [SerializeField]
    private GameObject m_mcuItemPrefab;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new McuViewMediator(this));
    }
    
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(McuViewMediator.NAME);
    }

    public void ClearContainer()
    {
        while(m_mcuItemContainer.transform.childCount > 0)
        {
            Destroy(m_mcuItemContainer.transform.GetChild(0).gameObject);
        }
    }

    public void AddConditionItem(McuVO _vo)
    {
        GameObject mcuItem = Instantiate(m_mcuItemPrefab);
        mcuItem.transform.SetParent(m_mcuItemContainer.transform);
        mcuItem.GetComponent<McuItemView>().Init(_vo);
    }
}
