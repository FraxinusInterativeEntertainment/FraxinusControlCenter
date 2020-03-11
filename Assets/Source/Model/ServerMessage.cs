using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMessage
{
    public string MsgType { get; set; }

    public ServerMessage(string _msgType)
    {
        MsgType = _msgType;
    }
}

public class ServerConditionMessage : ServerMessage
{
    public ConditionVO MsgContent { get; set; }

    public ServerConditionMessage(ConditionVO _msgContent, string _msgType):base(_msgType)
    {
        MsgContent = _msgContent;
    }
}