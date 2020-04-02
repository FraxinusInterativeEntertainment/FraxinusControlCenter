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
    private event Action<TcpClient, string> SendMessageFailed;

    private List<TcpClient> listConnectedClients = new List<TcpClient>(new TcpClient[0]);
    private TcpListener tcpListener;
    /// <summary>
    /// Background thread for TcpServer workload.  
    /// </summary>  
    private Thread tcpListenerThread;
    /// <summary>  
    /// Create handle to connected tcp client.  
    /// </summary>  
    private int m_port;
    private IPAddress m_ip;

    public TcpServerService(IPAddress _ip, int _port, int _maxConnections)
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

    public TcpServerService AddSendMessageFailedListener(Action<TcpClient, string> _sendMessageFailedListener)
    {
        SendMessageFailed += _sendMessageFailedListener;
        return this;
    }

    public void SendMessage(object token, string msg)
    {
        var client = token as TcpClient;
        {
            try
            {
                if (client == null || !IsConnected(client.Client))
                {
                    SendMessageFailed(client, msg);
                    AbortClient(client);
                    return;
                }

                NetworkStream stream = client.GetStream();
                if (stream.CanWrite)
                {
                    // Get a stream object for writing.    
                    // Convert string message to byte array.              
                    byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(msg);
                    // Write byte array to socketConnection stream.            
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                    Debug.Log("Server sent: " + msg);
                }
                else
                {
                    Debug.Log("Exception: cant write");
                    SendMessageFailed(client, msg);
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
                SendMessageFailed(client, msg);
                AbortClient(client);
                return;
            }
            catch (ObjectDisposedException objectDisposedException)
            {
                Debug.Log("Object Disposed exception: " + objectDisposedException);
                SendMessageFailed(client, msg);
                AbortClient(client);
            }
        }
    }

    public static bool IsConnected(Socket _socket)
    {
        try
        {
            return !(_socket.Available == 0 && _socket.Poll(1, SelectMode.SelectRead));
        }
        catch (SocketException) { return false; }
    }

    public void AbortClient(TcpClient _client)
    {
        if (listConnectedClients.Contains(_client))
        {
            listConnectedClients.Remove(_client);
        }
        ClientDisconnected(_client);
        _client.Close();
    }


    private void ListenForIncommingRequests()
    {
        tcpListener = new TcpListener(m_ip, m_port);
        tcpListener.Start();
        ThreadPool.QueueUserWorkItem(this.ListenerWorker, null);
    }

    private void ListenerWorker(object token)
    {
        while (tcpListener != null)
        {
            Debug.Log("New Listener Deployed");
            TcpClient connectedTcpClient = tcpListener.AcceptTcpClient();
            listConnectedClients.Add(connectedTcpClient);
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
        }
    }
}