using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class SimulationCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        object obj = notification.Body;
        SimulationProxy simulationProxy;
        simulationProxy = Facade.RetrieveProxy(SimulationProxy.NAME) as SimulationProxy;
        string name = notification.Name;

        switch (name)
        {
            case Const.Notification.GENERATE_VIRTUAL_PLAYER:
                simulationProxy.GenerateVirtualPlayer(obj as string);
                break;
            case Const.Notification.ADD_VIRTUAL_PLAYER_TO_GAME:
                simulationProxy.VirtualPersonalDeviceLogin(obj as VirtualLoginVO);
                break;
        }
    }
}
