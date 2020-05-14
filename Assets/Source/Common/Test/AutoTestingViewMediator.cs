using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Linq;

public class AutoTestingViewMediator : Mediator, IMediator
{
    public const string NAME = "AutoTestingViewMediator";
    public const string WARNING_TITLE = "自动化测试结果";
    public const string WARNING_BUTTON_NAME = "确认";
    public const string AUTO_TEST_TEXT = "***Auto Test*** : ";
    public const string PASS = " -----> PASS\n";
    public const string FAIL = " -----> FAILED\n";


    public const string TEST_ACCOUNT = "test1";
    public const string TEST_PASSWORD = "frax2020";
    public const float COORD_X = 50.0f;
    public const float COORD_Y = 50.0f;
    public const string ROOM_ID = "room2";
    public const string MODULE_NAME = "PlayerGroupTest";
    public const string MODULE_VALUE = "0";

    private TestState m_testState = TestState.Default;
    private string m_testPlayerUid;
    private string m_resultText;
    private string m_currentTestText;

    protected AutoTestingView m_autoTestingView { get { return m_viewComponent as AutoTestingView; } }

    public AutoTestingViewMediator(AutoTestingView _view) : base(NAME, _view)
    {
        _view.RunAutoTesting += StartAutoTesting;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {
            Const.Notification.LOGIN_SUCCESS,
            Const.Notification.LOGIN_FAIL,
            Const.Notification.LOGOUT_SUCCESS,
            Const.Notification.LOGOUT_FAIL,

            Const.Notification.GAME_STATUS_CHANGED,
            Const.Notification.GAME_STATUS_CHANGE_ERROR,
            Const.Notification.RECEIVED_GAME_STATUS,
            Const.Notification.V_PLAYER_LOGIN_SUCCESS,
            Const.Notification.SERVER_MSG_USER_INFO,
            Const.Notification.SERVER_MSG_GROUP_INFO
        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;

        switch (name)
        {
           
            case Const.Notification.LOGIN_SUCCESS:
                //Test Update Game Status
                //Test Send Location
                Debug.Log("TEST Login: " + m_testState);
                if (m_testState == TestState.Login || m_testState == TestState.ReLogin)
                {
                    ProcessTestState();
                }
                break;
            case Const.Notification.LOGIN_FAIL:
                ShowAutoTestResult(m_resultText + m_currentTestText + FAIL);
                ShowAutoTestResult("登陆失败：" + vo);
                break;

            case Const.Notification.RECEIVED_GAME_STATUS:
                //下一个state会尝试启动游戏
                if (m_testState == TestState.UpdateGameStatus)
                {
                    Debug.Log("TESTING RECV GAME STATUS");
                    ProcessTestState();
                }
                break;
            case Const.Notification.GAME_STATUS_CHANGED:
                if (m_testState == TestState.StartGame)
                {
                    //下一个state会加入vPlayer
                    ProcessTestState();
                }
                break;
            case Const.Notification.GAME_STATUS_CHANGE_ERROR:
                ShowAutoTestResult(m_resultText + m_currentTestText + FAIL);
                ShowAutoTestResult("改变游戏状态失败：" + vo);
                break;
            case Const.Notification.SERVER_MSG_USER_INFO:
                if (m_testState == TestState.AddVPlayer)
                {
                    m_testPlayerUid = (vo as Dictionary<string, UserInfo>).First().Value.uid;
                    ProcessTestState();
                }
                break;
            case Const.Notification.SERVER_MSG_GROUP_INFO:
                if (m_testState == TestState.AddPlayerToGroup)
                {
                    //TODO: 检查返回的vo是否包括m_uid在里面，如果在，说明测试用的Vplayer成功被添加进游戏中了。(等group——info接口接入后补全)
                    ProcessTestState();
                }
                break;

            case Const.Notification.LOGOUT_SUCCESS:
                break;
            case Const.Notification.LOGOUT_FAIL:
                break;
        }
    }

    private void ProcessTestState()
    {
        if (m_testState > 0)
        {
            m_resultText += m_currentTestText + PASS;
        }

        m_testState++;

        switch (m_testState)
        {
            case TestState.Login:
                SendNotification(Const.Notification.DEBUG_LOG, AUTO_TEST_TEXT + "Logging in.");
                m_currentTestText = "登陆";
                TestLogin();
                break;
            case TestState.UpdateGameStatus:
                SendNotification(Const.Notification.DEBUG_LOG, AUTO_TEST_TEXT + "Update Game Status.");
                m_currentTestText = "获取场次状态";
                TestUpdateGameStatus();
                break;
            case TestState.StartGame:
                SendNotification(Const.Notification.DEBUG_LOG, AUTO_TEST_TEXT + "Starting Game.");
                m_currentTestText = "开始游戏";
                TestStartGame();
                break;
            case TestState.AddVPlayer:
                SendNotification(Const.Notification.DEBUG_LOG, AUTO_TEST_TEXT + "Add V Players.");
                m_currentTestText = "加入VPlayer";
                TestAddVPlayer();
                break;
            case TestState.ReLogin:
                SendNotification(Const.Notification.DEBUG_LOG, AUTO_TEST_TEXT + "Logging in after Added V Players.");
                m_currentTestText = "重新登录";
                TestReLogin();
                break;
            case TestState.AddPlayerToGroup:
                SendNotification(Const.Notification.DEBUG_LOG, AUTO_TEST_TEXT + "Adding Player into Group.");
                m_currentTestText = "玩家进组";
                TestAddGroup();
                break;
            case TestState.End:
                SendNotification(Const.Notification.DEBUG_LOG, AUTO_TEST_TEXT + "Test Finished.");
                ShowAutoTestResult(m_resultText);
                ResetTestState();
                break;
        }
    }

    private void ShowAutoTestResult(string _errMsg)
    {
        if (m_testState != TestState.Default)
        {
            SendNotification(Const.Notification.CUSTOMIZED_POPUP, new PopupInfoVO(WARNING_TITLE, _errMsg, WARNING_BUTTON_NAME, true));
            ResetTestState();
        }
    }

    private void ResetTestState()
    {
        m_testState = 0;
        m_resultText = "";
        OnDefaultState();
    }

    private void OnDefaultState()
    {
        SendNotification(Const.Notification.SHOW_MAIN_PANEL_CONTENT, Const.UIFormNames.DEBUG_FORM);
        SendNotification(Const.Notification.SEND_LOGOUT);
    }

    private void StartAutoTesting()
    {
        ResetTestState();
        ProcessTestState();
    }

    private void TestLogin()
    {
        LoginVO loginVO = new LoginVO();
        loginVO.userName = TEST_ACCOUNT;
        loginVO.password = TEST_PASSWORD;
        AppFacade.instance.SendNotification(Const.Notification.SEND_LOGIN, loginVO);
    }

    private void TestUpdateGameStatus()
    {
        SendNotification(Const.Notification.SHOW_MAIN_PANEL_CONTENT, Const.UIFormNames.GAME_STATUS_FORM);
        SendNotification(Const.Notification.REQUEST_FOR_GAME_STATUS);
    }

    private void TestStartGame()
    {
        GameStatusProxy gameStatusProxy = AppFacade.instance.RetrieveProxy(GameStatusProxy.NAME) as GameStatusProxy;

        GameStatusVO gamestatus = new GameStatusVO();
        gamestatus.gameId = gameStatusProxy.GetCurrentGameId();
        gamestatus.gameStatus = GameStatus.s;
        SendNotification(Const.Notification.CHANGE_GAME_STATUS, gamestatus);
    }

    private void TestAddVPlayer()
    {
        SendNotification(Const.Notification.SHOW_MAIN_PANEL_CONTENT, Const.UIFormNames.DEBUG_FORM);
        GameStatusProxy gameStatusProxy;
        gameStatusProxy = Facade.RetrieveProxy(GameStatusProxy.NAME) as GameStatusProxy;

        if (gameStatusProxy.GetCurrentGameStatus() == GameStatus.s)
        {
            SendNotification(Const.Notification.GENERATE_VIRTUAL_PLAYER, gameStatusProxy.GetCurrentGameId());
        }
        else
        {
            SendNotification(Const.Notification.CUSTOMIZED_POPUP, new PopupInfoVO(WARNING_TITLE, "加入虚拟玩家失败：游戏尚未开始。", WARNING_BUTTON_NAME, true));
        }
    }

    private void TestReLogin()
    {
        SendNotification(Const.Notification.SEND_LOGOUT);
        TestLogin();
    }

    private void TestAddGroup()
    {
        PlayerInfoProxy playerInfoProxy;
        playerInfoProxy = Facade.RetrieveProxy(PlayerInfoProxy.NAME) as PlayerInfoProxy;

        if (playerInfoProxy.GetPlayerInfos().connectedPlayers == null || playerInfoProxy.GetPlayerInfos().connectedPlayers.Count <= 0)
        {
            ShowAutoTestResult("TestAddGroup Error: 游戏中没有该玩家！(" + m_testState + ")");
            return;
        }

        foreach (KeyValuePair<string, PlayerInfo> kvp in playerInfoProxy.GetPlayerInfos().connectedPlayers)
        {
            kvp.Value.posInfo.x = COORD_X;
            kvp.Value.posInfo.y = COORD_Y;
            kvp.Value.posInfo.rid = ROOM_ID;
        }

        SendNotification(Const.Notification.SEND_PLAYER_LOCATION_INFOS, playerInfoProxy.GetPlayerInfos());

        //TODO: 需要保证服务器已经收到了新的位置信息，再发送入组信号。由于位置信息没有回调，暂时没法判断服务器是否收到
        SendNotification(Const.Notification.WS_SEND, new SensorMessage(MODULE_NAME, MODULE_VALUE));
    }
}

public enum TestState
{ 
    Default,
    Login,
    UpdateGameStatus,
    StartGame,
    AddVPlayer,
    ReLogin,
    AddPlayerToGroup,
    End
}