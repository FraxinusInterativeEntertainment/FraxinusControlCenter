using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;

public class ModelPreCommand : SimpleCommand
{
    public override void Execute(PureMVC.Interfaces.INotification notification)
    {
        Facade.RegisterProxy(new LoginProxy());
        Facade.RegisterProxy(new ServerCommunicationProxy());
        Facade.RegisterProxy(new ChangeGameStatusProxy());
        Facade.RegisterProxy(new GameMapProxy());
        Facade.RegisterProxy(new GameStatusProxy());
    }
}
