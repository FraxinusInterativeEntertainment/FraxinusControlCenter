using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInfoVO
{
    public string title { get; set; }
    public string description { get; set; }
    public string buttonName { get; set; }
    public bool preventOtherInteractions { get; set; }
    public System.Action buttonAction { get; set; }

    public PopupInfoVO(string _title, string _desc, string _buttonName, bool _preventOtherInteractions, System.Action _buttonAction = null)
    {
        title = _title;
        description = _desc;
        buttonName = _buttonName;
        preventOtherInteractions = _preventOtherInteractions;
        buttonAction = _buttonAction;

        if (buttonAction == null)
        {
            buttonAction = () => { };
        }
    }
}
