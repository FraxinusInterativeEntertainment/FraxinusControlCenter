using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine.SceneManagement;

public class SceneCommand : SimpleCommand
{
    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.LOAD_SCENE:
                SceneManager.LoadScene((obj as SceneVO).sceneIndex);
                break;
        }
    }
}
