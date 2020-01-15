
using UnityEngine;
using System.Collections;
//引入库
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System;

public class TCPServerService
{
    private event Action<Socket> NewClientConnected;
    private event Action<Socket, string> MessageArrived;

    private Socket m_listener;
    private readonly Dictionary<string, MicroControllerClient>  m_connectedClients = new Dictionary<string, MicroControllerClient>();
    private IPAddress m_ip;
    private IPEndPoint m_ipEnd;
    private int m_port;
    private Thread m_connectThread;
    private int m_maxConnections;

    private Socket testHandler;

    public TCPServerService(string _ip, int _port, int _maxConnections)
    {
        m_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_ip = IPAddress.Parse(_ip);
        m_ipEnd = new IPEndPoint(m_ip, _port);
        m_maxConnections = _maxConnections;
    }

    public TCPServerService InitSocket()
    {
        m_listener.Bind(m_ipEnd);
        Debug.Log("TCP Socket Server Started!");

        m_listener.Listen(m_maxConnections);

        m_connectThread = new Thread(new ThreadStart(ListenForNewConnections));
        m_connectThread.IsBackground = true;
        m_connectThread.Start();

        return this;
    }

    public TCPServerService AddMessageListener(Action<Socket, string> _messageListener)
    {
        MessageArrived += _messageListener;
        return this;
    }

    public TCPServerService AddNewClientListener(Action<Socket> _newClientListener)
    {
        NewClientConnected += _newClientListener;
        return this;
    }

    public bool SocketConnected(Socket s)
    {
        bool part1 = s.Poll(1000, SelectMode.SelectRead);
        bool part2 = (s.Available == 0);
        if (part1 && part2)
            return false;
        else
            return true;
    }

    private void ListenForNewConnections()
    {
        Socket newConnectionHandler = null;

        while (true)
        {
            try
            {
                newConnectionHandler = m_listener.Accept();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                break;
            }

            string remoteEndPoint = newConnectionHandler.RemoteEndPoint.ToString();

            if (m_connectedClients.ContainsKey(remoteEndPoint))
            {
                m_connectedClients[remoteEndPoint] = new MicroControllerClient(Const.NetworkRelated.DEFAULT_CLIENT_ID, newConnectionHandler, false);
            }
            else
            {
                m_connectedClients.Add(remoteEndPoint, new MicroControllerClient(Const.NetworkRelated.DEFAULT_CLIENT_ID, newConnectionHandler, false));
            }
            
            //Send client info request
            //set clientID and set isOnline to true

            Debug.Log("\r\n[客户端\"" + remoteEndPoint + "\"建立连接成功！ 客户端数量：" + m_connectedClients.Count + "]");
            Send(newConnectionHandler, remoteEndPoint + "Connected!");

            NewClientConnected(newConnectionHandler);

            Thread recvThread = new Thread(Receive);
            recvThread.IsBackground = true;
            recvThread.Start(newConnectionHandler);
        }
    }

    private void Receive(object _socketClientPara)
    {
        Socket socketClient = _socketClientPara as Socket;

        while (true)
        {
            byte[] recvData = new byte[Const.NetworkRelated.BUFFER_SIZE];

            try
            {
                int recvLength = socketClient.Receive(recvData);

                if (recvLength <= 0)
                {
                    continue;
                }

                string recvString = Encoding.ASCII.GetString(recvData, 0, recvLength);

                MessageArrived(socketClient, recvString);
                //Send(socketClient, "From Frax Server: " + recvString);
            }
            catch (Exception)
            {
                m_connectedClients.Remove(socketClient.RemoteEndPoint.ToString());
                //Set MicroController Client status to offline
                
                Console.WriteLine("\r\n[客户端\"" + socketClient.RemoteEndPoint + "\"已经中断连接！ 客户端数量：" + m_connectedClients.Count + "]");

                socketClient.Close();
                break;
            }
        }
    }

    private void Send(Socket _client, string _message)
    {
        byte[] sendData = new byte[Const.NetworkRelated.BUFFER_SIZE];

        sendData = Encoding.ASCII.GetBytes(_message);

        _client.Send(sendData, sendData.Length, SocketFlags.None);
    }

    
}