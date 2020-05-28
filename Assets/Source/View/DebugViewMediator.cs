using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class DebugViewMediator : Mediator, IMediator
{
    public const string NAME = "DebugViewMediator";

    protected DebugView m_debugView { get { return m_viewComponent as DebugView; } }

    public DebugViewMediator(DebugView _view) : base(NAME, _view)
    {
        m_debugView.SendWsMsg += OnSendWsMsg;
        m_debugView.TryAddPlayer += OnTryAddVPlayer;
        Application.RegisterLogCallback(UnityLogHandler);
    }
    
    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.DEBUG_LOG
        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;

        switch (name)
        {
            case Const.Notification.DEBUG_LOG:
                m_debugView.ShowDebugText(vo as string);
                break;
        }
    }

    private void UnityLogHandler(string _condition, string _stackTrace, LogType _type)
    {
        //TODO: Remove type check after testing
        if (_type != LogType.Log)
        {
            SendNotification(Const.Notification.DEBUG_LOG, _condition + "\n" + _stackTrace);
        }
    }

    string c1 = "1";
    string c2 = "1";

    private void OnTryAddVPlayer()
    {
        GameStatusProxy gameStatusProxy;
        gameStatusProxy = Facade.RetrieveProxy(GameStatusProxy.NAME) as GameStatusProxy;

        SendNotification(Const.Notification.GENERATE_VIRTUAL_PLAYER, gameStatusProxy.GetCurrentGameId());
    }

    private void OnSendWsMsg()
    {
        //SendNotification(Const.Notification.TRY_SEND_MCU_MSG, new McuMsg(m_debugView.wsMsgVO, "************test send***********"));

        if (m_debugView.wsMsgVO == "location1")
        {
            Dictionary<string, LocationInfo> locationInfos = new Dictionary<string, LocationInfo>();
            locationInfos.Add("TestUWB123",new LocationInfo(1f + (float)Random.Range(1, 10) / 10.0f, 1f, "1"));
           // SendNotification(Const.Notification.WS_SEND, new LocationMessage(locationInfos));//m_debugView.wsMsgVO);
        }
        else if (m_debugView.wsMsgVO == "location2")
        {
            Dictionary<string, LocationInfo> locationInfos = new Dictionary<string, LocationInfo>();
            locationInfos.Add("TestUWB123", new LocationInfo(4f + (float)Random.Range(1, 10) / 10.0f, 4f, "1"));
           //SendNotification(Const.Notification.WS_SEND, new LocationMessage(locationInfos));//m_debugView.wsMsgVO);
        }
        else if (m_debugView.wsMsgVO == "conditionC1")
        {
            if (c1 == "1") c1 = "0";
            else if (c1 == "0") c1 = "1";
            SendNotification(Const.Notification.WS_SEND, new SensorMessage("c1_toggle", c1));//m_debugView.wsMsgVO);
        }
        else if (m_debugView.wsMsgVO == "conditionC2")
        {
            if (c2 == "1") c2 = "0";
            else if (c2 == "0") c2 = "1";
            SendNotification(Const.Notification.WS_SEND, new SensorMessage("c2_toggle", c2));//m_debugView.wsMsgVO);
        }
        else if (m_debugView.wsMsgVO == "next")
        {
            SendNotification(Const.Notification.WS_SEND, new SensorMessage("g_a_n_1", "1"));//m_debugView.wsMsgVO);
        }
        else if (m_debugView.wsMsgVO == "pevious")
        {
            SendNotification(Const.Notification.WS_SEND, new SensorMessage("g_a_n_3", "1"));//m_debugView.wsMsgVO);
        }
        else if (m_debugView.wsMsgVO == "group")
        {
            SendNotification(Const.Notification.WS_SEND, new SensorMessage("test_group_a", "1"));//m_debugView.wsMsgVO);
        }
        else if (m_debugView.wsMsgVO == "newplayer")
        {

        }
    }
    
}
