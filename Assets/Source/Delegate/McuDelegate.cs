using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McuDelegate
{
    private IResponder m_responder;
    private HttpService m_httpService;

    public McuDelegate(IResponder _responder)
    {
        m_responder = _responder;
    }

    public void GetAllMcu()
    {
        
    }

    private void LoginCallback(McuResponse _mcuResponse)
    {
        if (_mcuResponse.err_code == 0)
        {
            m_responder.OnResult(_mcuResponse.err_msg);
        }
        else
        {
            m_responder.OnFault(_mcuResponse.err_msg);
        }
    }
}

public class McuResponse : HttpResponse
{ 
    public McuResponse(int _errCode, string _errMsg) : base(_errCode, _errMsg)
    { 
    
    }
}