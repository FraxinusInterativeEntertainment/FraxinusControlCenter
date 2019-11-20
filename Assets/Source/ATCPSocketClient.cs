using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public abstract class ATCPSocketClient
{
    public Socket clientSocket { get; private set; }
    public bool isOnline { get; private set; }

    public ATCPSocketClient(Socket _clientSocket, bool _isOnline)
    {
        clientSocket = _clientSocket;
        isOnline = _isOnline;
    }
}


