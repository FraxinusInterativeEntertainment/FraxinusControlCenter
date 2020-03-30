using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
public class TcpServerService
{
    private event Action<TcpClient> NewClientConnected;
    private event Action<TcpClient, string> MessageArrived;
    private event Action<TcpClient> ClientDisconnected;

    private List<TcpClient> listConnectedClients = new List<TcpClient>(new TcpClient[0]);
    private TcpListener tcpListener;
    /// <summary>
    /// Background thread for TcpServer workload.  
    /// </summary>  
    private Thread tcpListenerThread;
    /// <summary>  
    /// Create handle to connected tcp client.  
    /// </summary>  
    private TcpClient connectedTcpClient;
    private int m_port;
    private string m_ip;

    public TcpServerService(string _ip, int _port, int _maxConnections)
    {
        m_port = _port;
        m_ip = _ip;
    }

    public void StartListening()
    {
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    public TcpServerService AddMessageListener(Action<TcpClient, string> _messageListener)
    {
        MessageArrived += _messageListener;
        return this;
    }

    public TcpServerService AddNewClientListener(Action<TcpClient> _newClientListener)
    {
        NewClientConnected += _newClientListener;
        return this;
    }

    public TcpServerService AddClientDisconnectedListener(Action<TcpClient> _clientDisconnectListener)
    {
        ClientDisconnected += _clientDisconnectListener;
        return this;
    }

    public void SendMessage(object token, string msg)
    {
        if (connectedTcpClient == null)
        {
            Debug.Log("Problem connectedTCPClient null");
            return;
        }
        var client = token as TcpClient;
        {
            try
            {
                NetworkStream stream = client.GetStream();
                if (stream.CanWrite)
                {
                    // Get a stream object for writing.    
                    // Convert string message to byte array.              
                    byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(msg);
                    // Write byte array to socketConnection stream.            
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                    Debug.Log("Server sent his message - should be received by client");
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
                AbortClient(client);
                return;
            }
        }
    }

    public void AbortClient(TcpClient _client)
    {
        _client.Close();
        if (listConnectedClients.Contains(_client))
        {
            listConnectedClients.Remove(_client);
        }
        ClientDisconnected(_client);
    }


    private void ListenForIncommingRequests()
    {
        tcpListener = new TcpListener(IPAddress.Parse(m_ip), m_port);
        tcpListener.Start();
        ThreadPool.QueueUserWorkItem(this.ListenerWorker, null);
    }

    private void ListenerWorker(object token)
    {
        while (tcpListener != null)
        {
            Debug.Log("New Listener Deployed");
            connectedTcpClient = tcpListener.AcceptTcpClient();
            listConnectedClients.Add(connectedTcpClient);
            // Thread thread = new Thread(HandleClientWorker);
            // thread.Start(connectedTcpClient);
            ThreadPool.QueueUserWorkItem(this.HandleClientWorker, connectedTcpClient);
        }
    }

    private void HandleClientWorker(object token)
    {
        Byte[] bytes = new Byte[1024];
        using (var client = token as TcpClient)
        using (var stream = client.GetStream())
        {
            NewClientConnected(client);
            SendMessage(client, "Hi MCU");
            int length;
            // Read incomming stream into byte arrary.                      
            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                var incommingData = new byte[length];
                Array.Copy(bytes, 0, incommingData, 0, length);
                // Convert byte array to string message.                          
                string clientMessage = Encoding.ASCII.GetString(incommingData);
                MessageArrived(client, clientMessage);
            }
            if (connectedTcpClient == null)
            {
                return;
            }
        }
        //  ThreadPool.QueueUserWorkItem(this.SendMessage, connectedTcpClient);
    }
}