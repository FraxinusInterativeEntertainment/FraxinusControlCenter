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
        Debug.Log((_data as GameSessionsResponse).game_sessions_info.Count);
        UpdateCurrentGameStatus(_data as GameSessionsResponse);
    }

    public void OnFault(object _data)
    {
        Debug.Log((_data as GameSessionsResponse).err_msg);
    }

    private void UpdateCurrentGameStatus(GameSessionsResponse _response)
    {
        if (_response.game_sessions_info != null && _response.game_sessions_info.Count > 0)
        {
            m_currentGameStatus.gameStatus = GameStatus.c;
            m_currentGameStatus.gameId = _response.game_sessions_info[0].game_id;

            foreach (GameSessionInfo sessionInfo in _response.game_sessions_info)
            {
                if (sessionInfo.status == "s")
                {
                    Debug.Log(sessionInfo.game_id);
                    m_currentGameStatus.gameId = sessionInfo.game_id;
                    m_currentGameStatus.gameStatus = GameStatusToEnum(sessionInfo.status);
                    break;
                }
            }
        }

        SendNotification(Const.Notification.RECEIVED_GAME_STATUS, m_currentGameStatus);
    }

    private GameStatus GameStatusToEnum(string statusStr)
    {
        GameStatus result = GameStatus.c;

        switch (statusStr)
        {
            case "s":
                result = GameStatus.s;
                break;
            case "c":
                result = GameStatus.c;
                break;
            case "p":
                result = GameStatus.p;
                break;
        }

        return result;
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
    public string status { get; set; }
}
