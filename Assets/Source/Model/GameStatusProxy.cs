using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Newtonsoft.Json;

public class GameStatusProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "GameStatusProxy";

    private readonly GameStatusVO m_currentGameStatus = new GameStatusVO();

    public GameStatusProxy() : base(NAME) { }

    public void RequestForGameStatus()
    {
        GameSessionsDelegate gameSessionDelegate = new GameSessionsDelegate(this);
        gameSessionDelegate.RequestForAllGameStatus();
    }

    public void OnResult(object _data)
    {
        SendNotification(Const.Notification.RECEIVED_GAME_STATUS, _data);
        OnReceivedGameSessions(_data as GameSessionsResponse);
    }

    public void OnFault(object _data)
    {
        Debug.Log((_data as string));
        SendNotification(Const.Notification.DEBUG_LOG, _data as string);
    }

    public string GetCurrentGameId()
    {
        return m_currentGameStatus.gameId;
    }

    public GameStatus GetCurrentGameStatus()
    {
        return m_currentGameStatus.gameStatus;
    }

    private void OnReceivedGameSessions(GameSessionsResponse _response)
    {
        if (_response.game_sessions_info != null && _response.game_sessions_info.Count > 0)
        {

            m_currentGameStatus.gameStatus = _response.game_sessions_info[_response.game_sessions_info.Count - 1].status;
            m_currentGameStatus.gameId = _response.game_sessions_info[_response.game_sessions_info.Count - 1].game_id;
            m_currentGameStatus.gameTime = _response.game_sessions_info[_response.game_sessions_info.Count - 1].game_time;

            foreach (GameSessionInfo sessionInfo in _response.game_sessions_info)
            {
                if (sessionInfo.status == GameStatus.p)
                {
                    m_currentGameStatus.gameId = sessionInfo.game_id;
                    m_currentGameStatus.gameStatus = sessionInfo.status;
                    m_currentGameStatus.gameTime = sessionInfo.game_time;
                    break;
                }
            }

            foreach (GameSessionInfo sessionInfo in _response.game_sessions_info)
            {
                if (sessionInfo.status == GameStatus.s)
                {
                    m_currentGameStatus.gameId = sessionInfo.game_id;
                    m_currentGameStatus.gameStatus = sessionInfo.status;
                    m_currentGameStatus.gameTime = sessionInfo.game_time;

                }
            }

            SendNotification(Const.Notification.GAME_STATUS_CHANGED, m_currentGameStatus);
            if (m_currentGameStatus.gameStatus == GameStatus.s)
            {
                SendNotification(Const.Notification.CHANGE_GAME_STATUS, m_currentGameStatus);
            }
        }
    }
}

public class GameSessionsResponse : HttpResponse
{
    public List<GameSessionInfo> game_sessions_info;

    public GameSessionsResponse(int _errCode, string _errMsg) : base(_errCode, _errMsg) { }
}

public class GameSessionInfo
{
    public string game_id { get; set; }
    public string game_time { get; set; }
    public GameStatus status { get; set; }
}
