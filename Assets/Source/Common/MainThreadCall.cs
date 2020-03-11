using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MainThreadCall : MonoBehaviour
{
    static object locker = new object();
    static List<Action> aList = new List<Action>();
    static int count = 0;

    public static int MainThreadID { get; private set; }

    private void Awake()
    {
        MainThreadID = Thread.CurrentThread.ManagedThreadId;
    }
    
    void Update()
    {
        if (count != 0)
        {
            lock (locker)
            {
                foreach (var a in aList)
                {
                    if (a != null) a();
                }
                aList.Clear();
                count = 0;
            }
        }
    }

    private static void AddCall(Action a)
    {
        lock (locker)
        {
            aList.Add(a);
            count++;
        }
    }
    static public void SafeCallback(Action _a)
    {
        AddCall(_a);
    }
    static public void SafeCallback<T>(Action<T> _a, T p1)
    {
        Action tmp = () => { _a(p1); };
        AddCall(tmp);
    }
    static public void SafeCallback<T1, T2>(Action<T1, T2> _a, T1 p1, T2 p2)
    {
        Action tmp = () => { _a(p1, p2); };
        AddCall(tmp);
    }
    static public void SafeCallback<T1, T2, T3>(Action<T1, T2, T3> _a, T1 p1, T2 p2, T3 p3)
    {
        Action tmp = () => { _a(p1, p2, p3); };
        AddCall(tmp);
    }
    static public void SafeCallback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> _a, T1 p1, T2 p2, T3 p3, T4 p4)
    {
        Action tmp = () => { _a(p1, p2, p3, p4); };
        AddCall(tmp);
    }
}