using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class MicroControllerClient : ATCPSocketClient
{
    public string clientID { get; private set; }
   
    public MicroControllerClient(string _clientID, Socket _clientSocket, bool _isOnline) :  base(_clientSocket, _isOnline)
    {
        clientID = _clientID;
    }

    public void AddErrorModule(string _moduleID, string errorDescription)
    {
    }
    
}


public class ErrorModule
{
    public string moduleID { get; private set; }
    public string errorDescription { get; private set; }

    public ErrorModule(string _moduleID, string _errorDescription)
    {
        moduleID = _moduleID;
        errorDescription = _errorDescription;
    }
}

public enum ClientCatagory
{
    MicroController,
    Other
}