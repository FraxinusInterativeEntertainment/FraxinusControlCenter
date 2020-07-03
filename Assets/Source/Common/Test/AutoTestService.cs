using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutoTestService
{
    public string PASS = "PASS";
    public string FAILED = "FAILED";
    public string RESULT_DEVIDER = " ----> ";

    public List<string> subscribedNotifications { get { return m_subscribedNotifications; } }

    private event Action OnTestStart = delegate { };
    private event Action<string> OnTestDone = delegate { };

    private readonly List<string> m_subscribedNotifications = new List<string>();
    private readonly LinkedList<TestItem> m_autoTestItems = new LinkedList<TestItem>();

    private LinkedListNode<TestItem> m_currentTestNode;

    public AutoTestService()
    {
        //为linkedList 加一个用于识别的头部
        m_autoTestItems.AddFirst(new TestItem());
    }

    public void AddTest(TestItem _testItem)
    {
        m_autoTestItems.AddLast(_testItem);
        subscribedNotifications.Add(_testItem.successNotification);
    }

    public void StartAutoTest()
    {
        OnTestStart();
        m_currentTestNode = m_autoTestItems.First;
        ProcedeedNextTest();
    }

    public void NotificationHandler(string _notification)
    { 
        if (m_currentTestNode != null && _notification == m_currentTestNode.Value.failedNotification) 
        {
            OnTestItemFailed();
            ProcedeedNextTest();
            return;
        }

        if (m_currentTestNode != null && m_currentTestNode.Value.successNotification == _notification)
        {
            OnTestItemPassed();
            ProcedeedNextTest();
        }
    }

    public void AddOnTestDoneListener(Action<string> _listener)
    {
        OnTestDone += _listener;
    }

    public void AddOnTestStartListener(Action _listener)
    {
        OnTestStart += _listener;
    }

    public void Skip(bool _isError)
    {
        if (_isError)
        {
            OnTestItemFailed();
        }
        else
        {
            OnTestItemPassed();
        }
        ProcedeedNextTest();
    }

    private void RunTest()
    { 
        if (m_currentTestNode == null)
        {
            //所有测试完成
            TestsDoneHandler();
            return;
        }

        m_currentTestNode.Value.test();

        //当一定时间没有收到服务器回复，判断为测试失败
        Timer.Instance.AddTimerTask(2f, () => { TimerCallback(m_currentTestNode.Value); });
    }

    //计时器的回调方法，判断当前测试是否超时
    private void TimerCallback(TestItem _testItem)
    { 
        if (_testItem == m_currentTestNode.Value && _testItem != m_autoTestItems.First.Value)
        {
            OnTestItemFailed();
            ProcedeedNextTest();
        }
    }

    private void ProcedeedNextTest()
    {
        m_currentTestNode = m_currentTestNode.Next;
        RunTest();
    }

    private void OnTestItemPassed()
    {
        m_currentTestNode.Value.pass = true;
    }

    private void OnTestItemFailed()
    {
        m_currentTestNode.Value.pass = false;
    }

    private void TestsDoneHandler()
    {
        string result = "";
        LinkedListNode<TestItem> tempNode = m_autoTestItems.First;
        tempNode = tempNode.Next;

        while (tempNode != null)
        {
            result += tempNode.Value.testName;
            result += RESULT_DEVIDER;
            result += tempNode.Value.pass ? PASS : FAILED;
            result += "\n";

            tempNode = tempNode.Next;
        }

        Debug.Log("完成所有测试: \n" + result);

        m_currentTestNode = m_autoTestItems.First;
        OnTestDone(result);
    }
}

public class TestItem
{
    public string testName;
    public Action test { get; private set; }
    public string successNotification { get; private set; }
    public string failedNotification { get; private set; }
    public bool pass;

    public TestItem(string _testName, Action _test, string _succNoti, string _failedNoti = null)
    {
        testName = _testName;
        test = _test;
        successNotification = _succNoti;
        failedNotification = _failedNoti;
        pass = false;
    }

    public TestItem()
    { 
    
    }
}