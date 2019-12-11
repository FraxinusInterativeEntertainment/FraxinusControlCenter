using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    private readonly Dictionary<int, FSMState> m_states = new Dictionary<int, FSMState>();
    private int m_currentStateID;
    private FSMState m_currentState;

    public void AddState(FSMState _state)
    {
        if (_state == null)
        {
            return;
        }

        if (m_currentState == null)
        {
            m_currentState = _state; ;
            m_currentStateID = _state.ID;
        }

        if (m_states.ContainsKey(_state.ID))
        {
            return;
        }

        m_states.Add(_state.ID, _state);
    }

    public void DeleteState(int _stateID)
    {
        if (_stateID == FSMState.NULL_STATE_ID)
        {
            return;
        }

        if (m_states.ContainsKey(_stateID))
        {
            return;
        }

        m_states.Remove(_stateID);
    }

    public void PerformTransition(int _transition)
    {
        if (_transition == FSMState.NULL_TRANSITION)
        {
            return;
        }

        int stateID = m_currentState.GetOutputState(_transition);

        if (stateID == FSMState.NULL_STATE_ID)
        {
            return;
        }

        if (!m_states.ContainsKey(stateID))
        {
            return;
        }

        FSMState state = m_states[stateID];
        m_currentState.DoAfterLeaving();
        m_currentState = state;
        m_currentStateID = stateID;
        m_currentState.DoBeforeEntering();
    }
}
