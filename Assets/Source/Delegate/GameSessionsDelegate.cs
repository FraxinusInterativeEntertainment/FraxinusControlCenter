using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionsDelegate
{
    private IResponder m_responder;
    private HttpService m_httpService;

    public GameSessionsDelegate(IResponder _responder)
    {
        m_responder = _responder;

        m_httpService = new HttpService(Const.Url.GET_AVAILABLE_GAME_SESSIONS, HttpRequestType.Get);
    }

    public void RequestForAllGameStatus()
    {
        m_httpService.SendRequest<GameSessionsResponse>(GameSessionsCallback);
    }

    private void GameSessionsCallback(GameSessionsResponse _response)
    {
        if (_response.err_code == 0)
        {
            m_responder.OnResult(_response);
        }
        else
        {
            m_responder.OnFault(_response.err_msg);
        }
    }
}
