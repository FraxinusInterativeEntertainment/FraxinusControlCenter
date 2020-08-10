using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerGroupDelegate 
{
    private IResponder m_responder;
    private HttpService m_httpService;
    private PlayerInfoVO m_playerInfoVO;
    public ChangePlayerGroupDelegate(IResponder _responder,PlayerInfoVO _playerInfo)
    {
        WWWForm form = new WWWForm();
        form.AddField("targetGroupName", _playerInfo.targetGroupName);
        form.AddField("playerUID", _playerInfo.playerUID);

        m_responder = _responder;
        m_playerInfoVO = _playerInfo;
        m_httpService = new HttpService(Const.Url.TRY_ADD_PLAYER_TO_GROUP, HttpRequestType.Post, form);
    }
    public void AddPlayerToGroup()
    {
        m_httpService.SendRequest<HttpResponse>(ChangePlayerGroupCallBack);
    }

    private void ChangePlayerGroupCallBack(HttpResponse _httpResponse)
    {
        if (_httpResponse.err_code == 0)
        {
            Debug.Log("玩家重新分组成功");
            m_responder.OnResult(_httpResponse.err_msg);
        }
        else
        {
            Debug.Log("玩家重新分组失败");
            m_responder.OnFault(_httpResponse.err_msg);
        }
    }
}
