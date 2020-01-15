using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroControllerVO
{
//public Socket microControllerSocket { get; private set; }
    public string endPoint { get; private set; }
    public string id { get; private set; }
    public MicroControllerStatus status { get; set; }
    public readonly List<McModule> mcModules = new List<McModule>();
}

public enum MicroControllerStatus
{
    Unknown,
    Normal,
    Disconnected
}