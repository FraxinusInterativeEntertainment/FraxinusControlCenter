using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSignalVO
{
    public string moduleID;
    public int value;

    public ControlSignalVO(string _moduleID, int _value)
    {
        moduleID = _moduleID;
        value = _value;
    }
}
