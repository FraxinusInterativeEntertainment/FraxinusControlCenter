using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDetectionTool : Button
{
    public event Action OnMouseOver = delegate { };
    public event Action OnMouseLeave = delegate { };
   
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        switch (state)
        {
            case SelectionState.Disabled:
                break;
            case SelectionState.Highlighted:
                OnMouseOver();
                break;
            case SelectionState.Normal:
                OnMouseLeave();
                break;
            case SelectionState.Pressed:
                break;
            default:
                break;
        }
    }
    public void AddMouseOverListener(Action _listener)
    {
        if (_listener!=null)
        {
            OnMouseOver += _listener;
        }
    }
    public void AddMouseLeaveListener(Action _listener)
    {
        if (_listener != null)
        {
            OnMouseLeave += _listener;
        }
    }
}