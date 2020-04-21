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

    public Action OnMcuViewInit;

    private readonly Dictionary<string, McuItemView> m_mcuItems = new Dictionary<string, McuItemView>();

    void Start()
    {
        AppFacade.instance.RegisterMediator(new McuViewMediator(this));
        OnMcuViewInit();
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

    public void UpdateMcuItem(McuVO _vo)
    {
        if (!m_mcuItems.ContainsKey(_vo.mcuName))
        {
            GameObject mcuItem = Instantiate(m_mcuItemPrefab);
            mcuItem.transform.SetParent(m_mcuItemContainer.transform);
            mcuItem.GetComponent<McuItemView>().Init(_vo);

            m_mcuItems.Add(_vo.mcuName, mcuItem.GetComponent<McuItemView>());
        }
        else
        {
            m_mcuItems[_vo.mcuName].Init(_vo);
        }
    }

    public void UpdateMcuStatus(McuVO _vo)
    {
        m_mcuItems[_vo.mcuName].UpdateStatus(_vo.mcuStatus);
    }
}
