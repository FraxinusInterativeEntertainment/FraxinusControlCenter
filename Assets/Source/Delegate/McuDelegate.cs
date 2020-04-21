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
        m_httpService = new HttpService(Const.Url.GET_ALL_MCU_INFO, HttpRequestType.Get);
    }

    public void GetAllMcu()
    {
        m_httpService.SendRequest<McuResponse>(McuInfoCallback);
    }

    private void McuInfoCallback(McuResponse _mcuResponse)
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


