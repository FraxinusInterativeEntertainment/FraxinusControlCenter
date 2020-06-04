using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McuModule
{
    public string module_name { get; set; }
    public string title { get; set; }
    public int module_type { get; set; }
    public int min { get; set; }
    public int max { get; set; }
    public string mcu_name { get; set; }
    public string desc { get; set; }

    public McuModule(string _moduleID, string _moduleName, string _mcuID, int _min, int _max, string _description)
    {

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