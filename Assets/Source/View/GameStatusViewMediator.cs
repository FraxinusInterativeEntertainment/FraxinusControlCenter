using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class GameStatusViewMediator : Mediator, IMediator
{
    public const string NAME = "GameStatusViewMediator";

    private GameStatusView m_gameStatusView { get { return m_viewComponent as GameStatusView; } }
    private readonly GameStatusVO m_currentGameStatus = new GameStatusVO("", GameStatus.c);

    public GameStatusViewMediator(GameStatusView _view) : base(NAME, _view)
    {
        _view.ChangeGameStatus += ChangeGameStatusHandler;
        _view.OnUpdateGameStatusClick += UpdateGameStatusHandler;
        _view.OnGameStatusViewShowed += UpdateGameStatusHandler;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.GAME_STATUS_CHANGED,
            Const.Notification.GAME_STATUS_CHANGE_ERROR,
            Const.Notification.RECEIVED_GAME_STATUS,
            Const.Notification.UPDATE_DEVICE_ID_TO_USER_INFO
        };
    }

    public override void HandleNotification(INotification _notification)
    {
        string name = _notification.Name;
        object vo = _notification.Body;

        switch (name)
        {
            case Const.Notification.GAME_STATUS_CHANGED:
                UpdateCurrentGameStatus((vo as GameStatusVO).gameId, (vo as GameStatusVO).gameStatus, (vo as GameStatusVO).gameTime);
                break;
            case Const.Notification.GAME_STATUS_CHANGE_ERROR:
                SendNotification(Const.Notification.WARNING_POPUP, (vo as string));
                SendNotification(Const.Notification.DEBUG_LOG, "Game Status change error: " + vo as string);
                break;
            case Const.Notification.RECEIVED_GAME_STATUS:
                (vo as GameSessionsResponse).game_sessions_info.ForEach((section) => { Debug.Log(section.game_time + ": " + section.status); });
                UpdateGamesessioInfo(vo as GameSessionsResponse);
                break;
            case Const.Notification.UPDATE_DEVICE_ID_TO_USER_INFO:
                SendNotification(Const.Notification.DEBUG_LOG, "Player Updated: " + (vo as Dictionary<string, UwbUserInfo>).Count);

                //TODO: Remove foreach{} when finished testing
                foreach(KeyValuePair<string, UwbUserInfo> kvp in (vo as Dictionary<string, UwbUserInfo>))
                {
                    PlayerPosSImulator.instance.OnNewVirtualPlayer(kvp.Key);
                }

                AppFacade.instance.SendNotification(Const.Notification.DEBUG_LOG, "Players updated: " + (vo as Dictionary<string, UwbUserInfo>).Count.ToString());
                break;
        }
    }

    private void ChangeGameStatusHandler(GameStatus _status)
    {
        if (m_currentGameStatus == null || m_currentGameStatus.gameId.Length <= 0)
        {
            return;
        }

        GameStatusVO gamestatus = new GameStatusVO();
        gamestatus.gameId = m_currentGameStatus.gameId;
        gamestatus.gameStatus = _status;
        SendNotification(Const.Notification.CHANGE_GAME_STATUS, gamestatus);
    }

    private void UpdateGameStatusHandler()
    {
        SendNotification(Const.Notification.REQUEST_FOR_GAME_STATUS);
    }


    private void UpdateCurrentGameStatus(string _gameID, GameStatus _gameStatus, string _gameTime)
    {
        if (_gameID != null && _gameID.Length >= 0)
        {
            m_currentGameStatus.gameId = _gameID;
        }
        m_currentGameStatus.gameStatus = _gameStatus;
        m_currentGameStatus.gameTime = _gameTime;

        UpdateGameStatusIndicator(m_currentGameStatus.gameStatus);
        m_gameStatusView.SetGameIdText(m_currentGameStatus.gameId);
        m_gameStatusView.SetGameStartTimeText(m_currentGameStatus.gameTime);

        switch (_gameStatus)
        {
            case GameStatus.p:
                break;
            case GameStatus.s:
                SendNotification(Const.Notification.GAME_STARTED);
                break;
            case GameStatus.c:
                SendNotification(Const.Notification.GAME_CLOSED);
                break;
        }
    }
    private void UpdateGameStatusIndicator(GameStatus _status)
    { 
        switch(_status)
        {
            case GameStatus.p:
                m_gameStatusView.ActiveReadyStatus();
                break;
            case GameStatus.s:
                m_gameStatusView.ActiveStartStatus();
                break;
            case GameStatus.c:
                m_gameStatusView.ActiveClosedStatus();
                break;
        }
    }
    private void UpdateGamesessioInfo(GameSessionsResponse _vo)
    {
        for (int i = 0; i < _vo.game_sessions_info.Count; i++)
        {
            m_gameStatusView.UpdateGameSession(_vo.game_sessions_info[i]);
        }
    }
}
