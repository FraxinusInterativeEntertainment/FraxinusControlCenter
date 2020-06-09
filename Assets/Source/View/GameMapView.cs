using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameMapView : UIViewBase
{
    public Image visualMap;

    [SerializeField]
    private GameObject m_playerIndicatorPrefab;
    [SerializeField]
    private Transform m_playerIndicatorContainer;
    private readonly Dictionary<string, PlayerMapIndicator> m_playerIndicators = new Dictionary<string, PlayerMapIndicator>();

    void OnEnable()
    {
        AppFacade.instance.RegisterMediator(new GameMapViewMediator(this));
        Show();
    }

    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(GameMapViewMediator.NAME);
    }

    public void UpdatePlayerMapPos(string _uid, Vector3 _pos)
    { 
        if (!m_playerIndicators.ContainsKey(_uid))
        {
            m_playerIndicators.Add(_uid, GeneratePlayerIndicator());
        }

        m_playerIndicators[_uid].UpdatePosition(_pos);
    }

    private PlayerMapIndicator GeneratePlayerIndicator()
    {
        PlayerMapIndicator indicator = Instantiate(m_playerIndicatorPrefab, m_playerIndicatorContainer).GetComponent<PlayerMapIndicator>();

        return indicator;
    }
}


