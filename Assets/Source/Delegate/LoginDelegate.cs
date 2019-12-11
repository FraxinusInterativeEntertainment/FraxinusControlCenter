using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginDelegate
{
    private IResponder m_responder;
    private HttpService m_httpService;

    public LoginDelegate(IResponder _responder, LoginVO _loginVO)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", _loginVO.userName);
        form.AddField("password", _loginVO.password);

        m_responder = _responder;
        m_httpService = new HttpService(Const.Url.CONTROL_CENTER_LOGIN, HttpRequestType.Post, form);
    }

    public void LoginService()
    {
        m_httpService.SendRequest<HttpResponse>(LoginCallback);
    }

    private void LoginCallback(HttpResponse _httpResponse)
    {
        if (_httpResponse.err_code == 0)
        {
            RequestForToken();
        }
        else
        {
            m_responder.OnFault(_httpResponse.err_msg);
        }
    }

    private void RequestForToken()
    {
        m_httpService = new HttpService(Const.Url.REQUEST_WS_TOKEN, HttpRequestType.Get);
        m_httpService.SendRequest<TokenRequestResponse>(TokenCallback);
    }

    private void TokenCallback(TokenRequestResponse _tokenResponse)
    {
        if (_tokenResponse.err_code == 0)
        {
            m_responder.OnResult(_tokenResponse.ws_token);
        }
        else
        {
            m_responder.OnFault(_tokenResponse.err_msg);
        }
    }

}
