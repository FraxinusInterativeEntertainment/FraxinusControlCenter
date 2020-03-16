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
        RequestForAllMcu();
    }

    public void OnResult(object _data)
    {
    }

    public void OnFault(object _data)
    {
    }

    public void RequestForAllMcu()
    {
        //TODO: Remove Fake data
        m_mcu.Add("MCU1", new McuVO("MCU1", McuStatus.Connected, "room1"));
        m_mcu.Add("MCU2", new McuVO("MCU2", McuStatus.Disconnnected, "room2"));
        m_mcu.Add("MCU3", new McuVO("MCU3", McuStatus.Connected, "room3"));
        m_mcu.Add("MCU4", new McuVO("MCU4", McuStatus.Connected, "room3"));
        m_mcu.Add("MCU5", new McuVO("MCU5", McuStatus.Unknown, "room4"));
        m_mcu.Add("MCU6", new McuVO("MCU6", McuStatus.Connected, "room1"));

        m_mcu["MCU1"].modules.Add(new McuModule("c1_toggle", "condition_c1", "MCU1", 0, 1, "111"));
        m_mcu["MCU1"].modules.Add(new McuModule("c2_toggle", "condition_c2", "MCU1", 0, 1, "111a"));
        m_mcu["MCU2"].modules.Add(new McuModule("test_group_a", "groupA加入玩家", "MCU2", 0, 0, "222"));

        SendNotification(Const.Notification.ALL_MCU_UPDATED);
    }

    public void UpdateAllMcu(Dictionary<string, McuVO> _allMcu)
    {
        m_mcu = _allMcu;
        //SendNotification(Const.Notification.ALL_CONDITION_UPDATED);
    }
}