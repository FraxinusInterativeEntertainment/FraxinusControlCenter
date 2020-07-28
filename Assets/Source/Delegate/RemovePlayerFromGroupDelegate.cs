using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerFromGroupDelegate
{
    private IResponder m_responder;
    private HttpService m_httpService;
    public RemovePlayerFromGroupDelegate(IResponder _responder, string _playerUid)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerUID", _playerUid);

        m_responder = _responder;
        m_httpService = new HttpService(Const.Url.TRY_REMOVE_PLAYER_FROM_GROUP, HttpRequestType.Post, form);
    }
    public void RemovePlayerFromGroup()
    {
        m_httpService.SendRequest<HttpResponse>(RemovePlayerFromGroupCallBack);
    }

    private void RemovePlayerFromGroupCallBack(HttpResponse _httpResponse)
    {
        if (_httpResponse.err_code == 0)
        {
            Debug.Log("玩家移除分组成功");
            Debug.Log(_httpResponse.err_msg);
            m_responder.OnResult(_httpResponse.err_msg);
        }
        else
        {
            Debug.Log("玩家移除分组失败");
            m_responder.OnFault(_httpResponse.err_msg);
        }
    }
}
