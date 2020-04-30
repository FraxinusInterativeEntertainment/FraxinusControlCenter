using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McuMessage
{
    public string MsgType { get; set; }
}

public class McuHeartbeatMsg : McuMessage
{ 
    public McuHeartbeatContent MsgContent { get; set; }
}

public class McuHeartbeatContent
{
    public string Mcuid { get; set; }
}

public class McuConditionMsg : McuMessage
{
    public ConditionMsgContent MsgContent { get; set; }
}

public class ConditionMsgContent
{
    public string moduleName { get; set; }
    public string value { get; set; }
}