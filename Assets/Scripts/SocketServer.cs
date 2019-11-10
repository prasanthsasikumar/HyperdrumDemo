using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SocketServer : MonoBehaviour {

    public string serverIP;
    public int serverPort;

    private string TAG = "SocketServer: ";

    private bool isRunningThread = false;
    private Socket server;
    private List<Socket> socketList = new List<Socket>();
    private byte[] msg = new byte[10000000];

    [Range(0, 1)]
    public float PLVValue;


    public enum messageID
    {
        msgString = 0,
        msgPLV = 1,
        EndThread = 99
    }

    // Use this for initialization
    void Start () {

        InitSocketServer();

    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnApplicationQuit()
    {
        server.Close();
        Debug.Log(TAG + "Server closed");
    }

    //init socket server
    void InitSocketServer()
    {
        server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPAddress ip = IPAddress.Parse(serverIP);
        IPEndPoint ip_end_point = new IPEndPoint(ip, serverPort);

        server.Bind(ip_end_point);
        server.Listen(10);
        Debug.Log(TAG + "Start server socket: " + server.LocalEndPoint.ToString());
        server.BeginAccept(new AsyncCallback(AcceptClient), server);
    }

    void AcceptClient(IAsyncResult ar)
    {
        Socket myserver = ar.AsyncState as Socket;
        Socket client = myserver.EndAccept(ar);
        Debug.Log(TAG + "New Client added, Client ip: " + client.RemoteEndPoint);
        socketList.Add(client);

        isRunningThread = true;
        Thread t = new Thread(ReceiveMsg);
        t.Start(client);

        myserver.BeginAccept(new AsyncCallback(AcceptClient), myserver);
    }

    void ReceiveMsg(object socket)
    {
        Socket mSocket = socket as Socket;
        while(isRunningThread)
        {
            try
            {
                int packageLength = mSocket.Receive(msg);
                Debug.Log(TAG + "Client id: " + mSocket.RemoteEndPoint.ToString() + " Package Length: " + packageLength);
            }
            catch(Exception e)
            {
                Debug.LogError(TAG + e.Message);
                socketList.Remove(mSocket);
                //mSocket.Shutdown(SocketShutdown.Both);
                //mSocket.Close();
                break;
            }

            ByteBuffer buff = new ByteBuffer(msg);

            int id = buff.ReadInt();
            if (id == (int)messageID.msgString)
            {
                string mssage = buff.ReadString();
                Debug.Log(TAG + "Client id: " + mSocket.RemoteEndPoint.ToString() + " Test Message: " + mssage);
            }
            else if (id == (int)messageID.msgPLV)
            {
                float PLV = buff.ReadFloat();
                PLVValue = PLV;
                Debug.Log(TAG + "Client id: " + mSocket.RemoteEndPoint.ToString() + " PLV Value: " + PLV);
            }
            else if (id == (int)messageID.EndThread)
            {
                isRunningThread = false;
                string mssage = buff.ReadString();
                Debug.Log(TAG + "Client id: " + mSocket.RemoteEndPoint.ToString() + " Test Message: " + mssage);
            }            
            else
                continue;    
        }
    }

}
