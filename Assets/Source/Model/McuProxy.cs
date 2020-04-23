using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class McuProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "McuProxy";

    private Dictionary<string, McuVO> m_mcu = new Dictionary<string, McuVO>();
    public Dictionary<string, McuVO> mcu { get { return m_mcu; } }

    public McuProxy() : base(NAME)
    {

    }

    public void OnResult(object _data)
    {
    }

    public void OnFault(object _data)
    {
    }

    public void InitMcuInfos()
    {
        HttpService m_McuInfoService = new HttpService(Const.Url.GET_ALL_MCU_INFO, HttpRequestType.Get);
        m_McuInfoService.SendRequest<McuResponse>(OnReceivedMcuInfos);
    }

    public void InitMcuModuleInfos()
    {
        HttpService m_McuModuleInfoService = new HttpService(Const.Url.GET_ALL_MCU_MODULE_INFO, HttpRequestType.Get);
        m_McuModuleInfoService.SendRequest<McuModuleResponse>(OnReceivedMcuModuleInfos);
    }

    public void UpdateAllMcu(Dictionary<string, McuVO> _allMcu)
    {
        m_mcu = _allMcu;
        SendNotification(Const.Notification.ALL_MCU_UPDATED);
    }

    public void UpdateMcu(McuVO _mcuVO)
    {
        if(m_mcu.ContainsKey(_mcuVO.mcuName))
        {
            m_mcu[_mcuVO.mcuName].mcuStatus = _mcuVO.mcuStatus;
            SendNotification(Const.Notification.UPDATE_MCU_ITEM, m_mcu[_mcuVO.mcuName]);
        }
    }

    public McuStatus GetMcuStatus(string _mcuID)
    {
        if (m_mcu.ContainsKey(_mcuID))
        {
            return m_mcu[_mcuID].mcuStatus;
        }

        return McuStatus.Unknown;
    }

    private void OnReceivedMcuInfos(McuResponse _response)
    {
        Dictionary<string, string> mcuInfos = _response.mcu_infos;

        m_mcu.Clear();

        foreach(KeyValuePair<string, string> kvp in mcuInfos)
        {
            if (!m_mcu.ContainsKey(kvp.Key))
            {
                m_mcu.Add(kvp.Key, null);
            }

            m_mcu[kvp.Key] = new McuVO(kvp.Key, McuStatus.Unknown, kvp.Value);
        }
    }

    private void OnReceivedMcuModuleInfos(McuModuleResponse _response)
    {
        Dictionary<string, McuModule> moduleInfos = _response.mcu_module_infos;

        foreach (KeyValuePair<string, McuModule> kvp in moduleInfos)
        { 
            if (!m_mcu.ContainsKey(kvp.Value.mcu_name))
            {
                //TODO: Warning: could not find parent mcu of this module
                SendNotification(Const.Notification.DEBUG_LOG, "Parent MCU of this module Not Found!");
                Debug.Log("NotFound");
                continue;
            }
            else
            {
                m_mcu[kvp.Value.mcu_name].modules.Add(kvp.Value);
            }
        }

        SendNotification(Const.Notification.ALL_MCU_UPDATED);
    }
}

public class McuResponse : HttpResponse
{
    public Dictionary<string, string> mcu_infos { get; set; }

    public McuResponse(int _errCode, string _errMsg) : base(_errCode, _errMsg)
    {

    }
}

public class McuModuleResponse : HttpResponse
{ 
    public Dictionary<string, McuModule> mcu_module_infos { get; set; }

    public McuModuleResponse(int _errCode, string _errMsg) : base(_errCode, _errMsg)
    {

    }
}