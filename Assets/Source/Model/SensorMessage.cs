using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorMessage : ServerMessage
{
    public const string MCU_MODULE_MSG_TYPE = "sensor";
    public McuModuleMsgContent MsgContent { get; private set; }

    public SensorMessage(string _moduleID, string _value) : base(MCU_MODULE_MSG_TYPE)
    {
        MsgContent = new McuModuleMsgContent(_moduleID, _value);
    }
}

public class McuModuleMsgContent
{ 
    public string module_id { get; private set; }
    public string value { get; private set; }

    public McuModuleMsgContent(string _moduleID, string _value)
    {
        module_id = _moduleID;
        value = _value;
    }
}
