using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class McuServerVO
{
    public Dictionary<string, McuConnectionVO> connectedMcus { get; set; }

    public McuServerVO()
    {
        connectedMcus = new Dictionary<string, McuConnectionVO>();
    }
}

public class McuConnectionVO
{
    public TcpClient client;
    public bool isAlive;

    public McuConnectionVO(TcpClient _client, bool _heartbeat)
    {
        client = _client;
        isAlive = _heartbeat;
    }
}