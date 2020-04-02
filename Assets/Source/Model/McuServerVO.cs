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

public class McuMsg
{
    public string McuId { get; set; }
    public string msg { get; set; }

    public McuMsg(string _id, string _msg)
    {
        McuId = _id;
        msg = _msg;
    }
}
