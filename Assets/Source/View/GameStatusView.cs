using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameStatusView : UIViewBase
{
    public event Action<GameStatus> ChangeGameStatus = delegate { };
    public event Action OnUpdateGameStatusClick = delegate { };
    public event Action OnNextGameButtonClick = delegate { };
    public event Action OnGameStatusViewShowed = delegate { };

    [SerializeField]
    private Button m_startGame;
    [SerializeField]
    private Button m_nextGame;
    [SerializeField]
    private Button m_closeGame;
    [SerializeField]
    private Button m_updateGameStatus;

    [SerializeField]
    private Text m_gameIdIndicator;
    [SerializeField]
    private Text m_gameStartTimeIndicator;
    [SerializeField]
    private GameObject m_readyStatusIndicator;
    [SerializeField]
    private GameObject m_startStatusIndicator;
    [SerializeField]
    private GameObject m_closedStatusIndicator;
    [SerializeField]
    private GameObject m_gameSessions;
    [SerializeField]
    private GameObject m_gameSessionItem;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new GameStatusViewMediator(this));

        m_nextGame.onClick.AddListener(() => { OnNextGameButtonClick(); });
        m_startGame.onClick.AddListener(() => { ChangeGameStatus(GameStatus.s); });
        m_closeGame.onClick.AddListener(() => { ChangeGameStatus(GameStatus.c); });
        m_updateGameStatus.onClick.AddListener(() => { OnUpdateGameStatusClick(); });
        
        Show();
    }
    private void OnDisable()
    {
        for (int i = 0; i < m_gameSessions.transform.childCount; i++)
        {
            Destroy(m_gameSessions.transform.GetChild(i).gameObject);
        }
    }
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(GameStatusViewMediator.NAME);
       
    }

    public override void Show()
    {
        base.Show();
        OnGameStatusViewShowed();
    }

    public void OnGameStatusChangeSuccess(object _vo)
    {
        Debug.Log(_vo);
    }

    public void OnGameStatusChangeFailed(object _vo)
    {
        Debug.Log(_vo);
    }

    public void ActiveReadyStatus()
    {
        HideAllStatusIndicator();
        m_readyStatusIndicator.SetActive(true);
    }

    public void ActiveStartStatus()
    {
        HideAllStatusIndicator();
        m_startStatusIndicator.SetActive(true);
    }

    public void ActiveClosedStatus()
    {
        HideAllStatusIndicator();
        m_closedStatusIndicator.SetActive(true);
    }

    public void SetGameIdText(string _gameID)
    {
        m_gameIdIndicator.text = _gameID;
    }

    public void SetGameStartTimeText(string _time)
    {
        m_gameStartTimeIndicator.text = _time;
    }

    private void HideAllStatusIndicator()
    {
        m_readyStatusIndicator.SetActive(false);
        m_startStatusIndicator.SetActive(false);
        m_closedStatusIndicator.SetActive(false);
    }
    public void UpdateGameSession(GameSessionInfo _vo)
    {
        GameObject sessionItem = Instantiate(m_gameSessionItem);
        sessionItem.transform.SetParent(m_gameSessions.transform);
        GameSessionItem gameSessionItem = sessionItem.GetComponent<GameSessionItem>();
        gameSessionItem.SetSessionInfo(_vo);
        if (_vo.status==GameStatus.c)
        {
            gameSessionItem.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f,0.5f);
        }
    }
}


