using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState
{
    public const int NULL_STATE_ID = 0;
    public const int NULL_TRANSITION = 0;

    protected int stateID;
    public int ID { get { return stateID; } }
    protected Dictionary<int, int> transitionMap = new Dictionary<int, int>();
    protected FSMSystem fsmSystem;

    public FSMState(int _id, FSMSystem _fsmSystem)
    {
        stateID = _id;
        fsmSystem = _fsmSystem;
    }

    public void AddTransition(int _transition, int _stateID)
    {
        if (_transition == 0)
        {
            return;
        }
        if (_stateID == 0)
        {
            return;
        }

        transitionMap[_transition] = _stateID;
    }

    public void DeleteTransition(int _transition)
    {
        if (_transition == 0)
        {
            return;
        }
        if (!transitionMap.ContainsKey(_transition))
        {
            return;
        }

        transitionMap.Remove(_transition);
    }

    public int GetOutputState(int _transition)
    {
        if (!transitionMap.ContainsKey(_transition))
        {
            return NULL_STATE_ID;
        }

        return transitionMap[_transition];
    }

    public virtual void DoBeforeEntering() { }
    public virtual void DoAfterLeaving() { }
    public virtual void Act() { }
    public virtual void Reason() { }
}
