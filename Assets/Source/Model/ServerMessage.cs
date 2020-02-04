using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMessage
{
    public string MsgType { get; private set; }

    public ServerMessage(string _msgType)
    {
        MsgType = _msgType;
    }
}
