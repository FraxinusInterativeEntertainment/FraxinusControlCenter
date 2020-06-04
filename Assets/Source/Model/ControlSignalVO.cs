using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSignalVO
{
    public string mcuName { get; set; }
    public string moduleName { get; set; }
    public int value { get; set; }
    public int moduleType { get; set; }

    public ControlSignalVO(string _mcuName, string _moduleName, int _value, int _moduleType )
    {
        mcuName = _mcuName;
        moduleName = _moduleName;
        value = _value;
        moduleType = _moduleType;
    }
}
