using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McuModule
{
    public string mcuID { get; set; }
    public string moduleName { get; set; }
    public string ModuleID { get; set; }
    public int min { get; set; }
    public int max { get; set; }
    public string description { get; set; }

    public McuModule(string _moduleID, string _moduleName, string _mcuID, int _min, int _max, string _description)
    {
        ModuleID = _moduleID;
        moduleName = _moduleName;
        mcuID = _mcuID;
        min = _min;
        max = _max;
        description = _description;
    }
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