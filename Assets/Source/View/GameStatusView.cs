using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameStatusView : UIViewBase
{
    public event Action ChangeGameStatus = delegate { };

    public GameStatusVO gameStatusVO { get; private set; }

    [SerializeField]
    private Button m_startGame;
    [SerializeField]
    private Button m_closeGame;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new GameStatusViewMediator(this));

        m_startGame.onClick.AddListener(() => { OnStartGameButton(); });
        m_closeGame.onClick.AddListener(() => { OnStarCloseButton(); });

        gameStatusVO = new GameStatusVO();

        //TODO: Remove Later
        gameStatusVO.gameId = "fakeone";
    }

    public void OnGameStatusChangeSuccess(object _vo)
    {
        Debug.Log(_vo);
    }

    public void OnGameStatusChangeFailed(object _vo)
    {
        Debug.Log(_vo);
    }

    private void OnStartGameButton()
    {
        gameStatusVO.gameStatus = GameStatus.s;
        ChangeGameStatus();
    }

    private void OnStarCloseButton()
    {
        gameStatusVO.gameStatus = GameStatus.c;
        ChangeGameStatus();
    }

    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(GameStatusViewMediator.NAME);
    }
}


