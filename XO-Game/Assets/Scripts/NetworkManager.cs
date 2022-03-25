using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    private string LocalIPAddress = "127.0.0.1";
    private int ListeningPort = 40000;
    private int SendingPort = 40001;
    Thread listener;
    static Queue pQueue = Queue.Synchronized(new Queue()); //this is the message queue, it is thread safe
    static UdpClient udp;
    private IPEndPoint endPoint;

    public GameManager gameManager; //drag this on the inspector
    public TextMeshProUGUI PortText;

    public string[] arr = new string[9];

    private void Update()
    {
        //in the main thread, read the message and update the game manager
        lock (pQueue.SyncRoot)
        {
            if (pQueue.Count > 0)
            {
                object o = pQueue.Dequeue(); //Take the olders message out of the queue
                gameManager.GotNetworkMessage((string)o); //Send it to the game manager
            }
        }
    }

    private void OnDestroy()
    {
        EndUDP();
    }

    public void StartUDP()
    {
        if (PortInUse(SendingPort))
        {
            endPoint = new IPEndPoint(IPAddress.Any, ListeningPort); //this line will listen to all IP addresses in the network
            Debug.Log("Player 1 connected");
            PortText.text = ListeningPort.ToString();
            gameManager.IsPlayer1 = true;
        }
        else
        {
            endPoint = new IPEndPoint(IPAddress.Any, SendingPort);
            Debug.Log("Player 2 connected");
            PortText.text = SendingPort.ToString();
            gameManager.IsPlayer1 = false;
        }
        //endPoint = new IPEndPoint(IPAddress.Parse(LocalIPAddress), ListeningPort); //this line will listen to a specific IP address
        udp = new UdpClient(endPoint);
        Debug.Log("Listening for Data...");
        listener = new Thread(new ThreadStart(MessageHandler));
        listener.IsBackground = true;
        listener.Start();
    }

    public static bool PortInUse(int port)
    {
        bool inUse = false;

        IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
        IPEndPoint[] ipEndPoints = ipProperties.GetActiveUdpListeners();

        foreach (IPEndPoint endPoint in ipEndPoints)
        {
            if (endPoint.Port == port)
            {
                inUse = true;
                break;
            }
        }

        return inUse;
    }

    void MessageHandler()
    {
        Byte[] data = new byte[0];
        while (true)
        {
            try
            {
                //Did we get a new message?
                data = udp.Receive(ref endPoint);
            }
            catch (Exception err)
            {
                //If there's a problem
                Debug.Log("Communication error, recieve data error " + err);
                udp.Close();
                return;
            }
            //Treat the new message
            string msg = Encoding.ASCII.GetString(data);
            Debug.Log("UDP incoming " + msg);
            pQueue.Enqueue(msg);
        }
    }

    private void EndUDP()
    {
        if (udp != null)
        {
            udp.Close();
        }
        if (listener != null)
        {
            listener.Abort();
        }
    }

    public void SendMessage(GameObject thisPoint)
    {
        UdpClient send_client = new UdpClient();
        IPEndPoint send_endPoint = new IPEndPoint(IPAddress.Parse(LocalIPAddress), SendingPort);
        if (!gameManager.IsPlayer1)
        {
            send_endPoint = new IPEndPoint(IPAddress.Parse(LocalIPAddress), ListeningPort);
        }
        else
        {
            send_endPoint = new IPEndPoint(IPAddress.Parse(LocalIPAddress), SendingPort);
        }
        byte[] bytes = Encoding.ASCII.GetBytes(thisPoint.name);
        send_client.Send(bytes, bytes.Length, send_endPoint);
        send_client.Close();
        //Debug.Log("Sent message: " + message);
    }
}
