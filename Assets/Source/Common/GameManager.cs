using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainFSMStateID
{
    NullState = FSMState.NULL_STATE_ID,
    LoginState,
    PreGame,
    InGame
}

public enum MainFSMTransition
{
    NullState = FSMState.NULL_STATE_ID,
    Login,
    PreGame,
    InGame
}

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    private FSMSystem m_fsmSystem;

    public static GameManager instance
    {
        get
        {
            return m_instance;
        }
    }

    void Start()
    {
        m_instance = this;
        DontDestroyOnLoad(this.gameObject);
        AppFacade.instance.startup();
        InitMainFSMSystem();
    }

    private void Update()
    {
        Timer.Instance.UpdateTimer();
    }

    public void ChangeMainFSMState(MainFSMStateID _stateID)
    {
        m_fsmSystem.PerformTransition((int)_stateID);
    }

    private void InitMainFSMSystem()
    {
        m_fsmSystem = new FSMSystem();

        FSMState loginState = new LoginState((int)MainFSMStateID.LoginState, m_fsmSystem);
        loginState.AddTransition((int)MainFSMTransition.InGame, (int)MainFSMStateID.InGame);
        loginState.AddTransition((int)MainFSMTransition.PreGame, (int)MainFSMStateID.PreGame);
        loginState.AddTransition((int)MainFSMTransition.Login, (int)MainFSMStateID.LoginState);
        m_fsmSystem.AddState(loginState);

        FSMState InGame = new InGameState((int)MainFSMStateID.InGame, m_fsmSystem);
        InGame.AddTransition((int)MainFSMTransition.PreGame, (int)MainFSMStateID.PreGame);
        m_fsmSystem.AddState(InGame);

        FSMState PreGame = new PreGameState((int)MainFSMStateID.PreGame, m_fsmSystem);
        PreGame.AddTransition((int)MainFSMTransition.InGame, (int)MainFSMStateID.InGame);
        m_fsmSystem.AddState(PreGame);

        m_fsmSystem.PerformTransition((int)MainFSMStateID.LoginState);
    }

    
}
