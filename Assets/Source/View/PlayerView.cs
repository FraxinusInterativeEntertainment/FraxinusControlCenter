using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerView : UIViewBase
{
    public event Action InitPlayerView = delegate { };

    [SerializeField]
    private GameObject m_userInfoContainer;
    [SerializeField]
    private GameObject m_userInfoItemPrefab;

    private readonly Dictionary<string, UserInfoItemView> m_userInfoItems = new Dictionary<string, UserInfoItemView>();
    void Start()
    {
        AppFacade.instance.RegisterMediator(new PlayerViewMediator(this));
        InitPlayerView();
    }
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(PlayerViewMediator.NAME);
    }
    public void UpdatePlayerInfo(PlayerInfo _vo)
    {
        if (!m_userInfoItems.ContainsKey(_vo.uid))
        {
            GameObject userInfoItemGO = Instantiate(m_userInfoItemPrefab);
            userInfoItemGO.transform.SetParent(m_userInfoContainer.transform);
            UserInfoItemView userInfoItem = userInfoItemGO.GetComponent<UserInfoItemView>();
            userInfoItem.Init(_vo);
            m_userInfoItems.Add(_vo.uid, userInfoItem);
        }
        else
        {
            m_userInfoItems[_vo.uid].Init(_vo);
        }
    }
}


