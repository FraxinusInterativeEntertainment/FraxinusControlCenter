using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutDelegate
{
    private IResponder m_responder;
    private HttpService m_httpService;

    public LogoutDelegate(IResponder _responder)
    {
        m_responder = _responder;
        m_httpService = new HttpService(Const.Url.CONTROL_CENTER_LOGOUT, HttpRequestType.Get);
    }

    public void LoginService()
    {
        m_httpService.SendRequest<HttpResponse>(LoginCallback);
    }

    private void LoginCallback(HttpResponse _httpResponse)
    {
        if (_httpResponse.err_code == 0)
        {
            m_responder.OnResult(_httpResponse.err_msg);
        }
        else
        {
            m_responder.OnFault(_httpResponse.err_msg);
        }
    }
}
