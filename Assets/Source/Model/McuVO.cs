using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McuVO
{
    public string mcuName { get; set; }
    public McuStatus mcuStatus { get; set; }
    public string roomID { get; set; }

    public McuVO(string _name, McuStatus _status, string _roomID)
    {
        mcuName = _name;
        mcuStatus = _status;
        roomID = _roomID;
    }
}

public enum McuStatus
{
    Unknown,
    Connected,
    Disconnnected

}