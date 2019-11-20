using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteDeviceStatus
{
    public string clientID { get; private set; }
    public string description { get; private set; }

    private string m_remoteEndPoint;
}
