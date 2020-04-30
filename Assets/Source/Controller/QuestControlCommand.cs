using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class QuestControlCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        object obj = notification.Body;
        QuestControlProxy questControlProxy;
        questControlProxy = Facade.RetrieveProxy(QuestControlProxy.NAME) as QuestControlProxy;
        string name = notification.Name;

        switch (name)
        {
            case Const.Notification.TRY_CHANGE_QUEST_NODE:
                questControlProxy.TryChangeQuestNode(obj as QuestControlVO);
                break;
        }
    }
}
