using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionInfoDelegate
{
    private IResponder m_responder;
    private HttpService m_httpService;
    public ConditionInfoDelegate(IResponder _responder)
    {
        m_responder = _responder;
        m_httpService = new HttpService(Const.Url.GET_ALL_CONDITION_DETAIL, HttpRequestType.Get);
    }
    public void RequestAllConditionInfo()
    {
        m_httpService.SendRequest<AllConditioResponsen>(ConditionInfoCallback);
    }
    private void ConditionInfoCallback(AllConditioResponsen _response)
    {
        if (_response.err_code == 0)
        {
            m_responder.OnResult(_response.condition_infos);
        }
        else
        {
            m_responder.OnFault(_response.err_msg);
        }
    }
}
