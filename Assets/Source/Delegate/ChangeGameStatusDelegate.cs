using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameStatusDelegate
{
    private IResponder m_responder;
    private HttpService m_httpService;
    private GameStatusVO m_gameStatusVO;

    public ChangeGameStatusDelegate(IResponder _responder, GameStatusVO _gameStatusVO)
    {
        WWWForm form = new WWWForm();
        form.AddField("status", _gameStatusVO.gameStatus.ToString());
        form.AddField("game_id", _gameStatusVO.gameId);

        m_responder = _responder;
        m_gameStatusVO = _gameStatusVO;
        m_httpService = new HttpService(Const.Url.CHANGE_GAME_STATUS, HttpRequestType.Post, form);
    }

    public void ChangeGameStatusService()
    {
        switch (m_gameStatusVO.gameStatus)
        {
            case GameStatus.s:
                m_httpService.SendRequest<ChangeGameStatusResponse>(GameStatusStartCallback);
                break;
            case GameStatus.c:
                m_httpService.SendRequest<ChangeGameStatusResponse>(GameStatusCloseCallback);
                break;

        }
    }

    private void GameStatusStartCallback(ChangeGameStatusResponse _response)
    {
        if (_response.err_code == 0)
        {
            _response.gameStatus = GameStatus.s;
            m_responder.OnResult(_response);
        }
        else
        {
            m_responder.OnFault(_response.err_msg);
        }
    }

    private void GameStatusCloseCallback(ChangeGameStatusResponse _response)
    {
        if (_response.err_code == 0)
        {
            _response.gameStatus = GameStatus.c;
            m_responder.OnResult(_response);
        }
        else
        {
            m_responder.OnFault(_response.err_msg);
        }
    }
}
