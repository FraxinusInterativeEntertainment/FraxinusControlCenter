using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McModule
{
    public string id { get; private set; }
    public string roomID { get; private set; }
    public ModuleStatus status { get; set; }
    public ModuleType type { get; private set; }
}

public enum ModuleType
{
    Unknown,
    Door,
    Light 
}

public enum ModuleStatus
{
    Unknown,
    Normal,
    Malfunction
}