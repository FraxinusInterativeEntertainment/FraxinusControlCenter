using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class McuServerVO
{
    public Dictionary<string, McuClient> connectedMcus { get; set; }

    public McuServerVO()
    {
        connectedMcus = new Dictionary<string, McuClient>();
    }
}

public class McuClient
{
    public string mcuId { get; set; }
    public TcpClient client { get; set; }
    public bool isAlive { get; set; }
    public bool disconnected { get; set; }

    public McuClient(string _mcuId, TcpClient _client)
    {
        mcuId = _mcuId;
        client = _client;
        isAlive = false;
        disconnected = true;
    }
}

public class McuMsgQueueItem
{
    public string TargetMcuName { get; set; }
    public string msg { get; set; }

    public McuMsgQueueItem(string _targetMcuName, string _msg)
    {
        TargetMcuName = _targetMcuName;
        msg = _msg;
    }
}
