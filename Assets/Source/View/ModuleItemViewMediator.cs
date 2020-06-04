using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Newtonsoft.Json;

public class ModuleItemViewMediator : Mediator, IMediator
{
    public const string NAME = "ModuleItemViewMediator";

    protected ModuleItemView m_moduleItemView { get { return m_viewComponent as ModuleItemView; } }

    public ModuleItemViewMediator(ModuleItemView _view, string _name) : base(_name, _view)
    {
        m_moduleItemView.OnSendSignalButtonClicked += TrySendControlSignal;
        m_moduleItemView.mouseDetectionTool.AddMouseOverListener(m_moduleItemView.ShowModuleDescText);
        m_moduleItemView.mouseDetectionTool.AddMouseLeaveListener(m_moduleItemView.ConcealModuleDescText);
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;

        switch (name)
        {
        }
    }

    private void TrySendControlSignal()
    {
        // type = 0，发送向MCU
        if (m_moduleItemView.controlSignalVO.moduleType == 0)
        {
            string targetMcuName = m_moduleItemView.controlSignalVO.mcuName;
            string msg = SerilizeMsgToMcu(m_moduleItemView.controlSignalVO.moduleName,
                                          m_moduleItemView.controlSignalVO.value);

            SendNotification(Const.Notification.TRY_SEND_MCU_MSG, new McuMsgQueueItem(targetMcuName, msg));
        }
        // type = 1，发送向服务器
        else if (m_moduleItemView.controlSignalVO.moduleType == 1)
        {
            SendNotification(Const.Notification.WS_SEND, new SensorMessage(m_moduleItemView.controlSignalVO.moduleName,
                                                                           m_moduleItemView.controlSignalVO.value.ToString()));
        }
    }

    private string SerilizeMsgToMcu(string _moduleName, int _value)
    {
        SensorMessage sensorMessage = new SensorMessage(_moduleName, _value.ToString());

        return JsonConvert.SerializeObject(sensorMessage);
    }
}
