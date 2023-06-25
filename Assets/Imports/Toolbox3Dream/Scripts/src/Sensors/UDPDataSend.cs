using UnityEngine;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UDPDataSend : MonoBehaviour
{
    private bool _first = true;

    public static string Ip ;
    public static int Port ;
    
    public InputField PortField, IpField;
    private bool _canConnect;
    public static bool Flag;
	
    public static IPEndPoint RemoteEndPoint;
    public static UdpClient Client;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //if(GameObject.FindObjectsOfType(gameObject.GetType()).Length > 1)
        //{
        //    Destroy(gameObject);
        //}
    }


    private void Update () 
	{
        //_ipField = LocalIpAdress();
        //Debug.Log(Flag);
        if (_canConnect)
        {
            if (_first)
            {
                Ip = IpField.text;
                Port = int.Parse(PortField.text);
                Init();
                Flag = true;
                _first = false;
            }
        }
        else
        {
            if (Flag)
            {
                if (!_first)
                {
                    Flag = false;
                    _first = true;
                    Client.Close();
                }
            }
        }
	}
   
    public static void Init()
    { 
        RemoteEndPoint = new IPEndPoint(IPAddress.Parse(Ip), Port);
        //RemoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.2"), Port);
        Client = new UdpClient(); 

        // status
        // 
    }
	
    public static void SendString(string message)
    {
        try
        {
            if (message != "")
            {
                // UTF8 encoding to binary format.
                byte[] data = Encoding.UTF8.GetBytes(message);
                // Send the message to the remote Client.
                Client.Send(data, data.Length, RemoteEndPoint);
            }
        }

        catch (Exception err)
        {
          Debug.Log(err.ToString());
        }
    }

    private void OnApplicationQuit()
    {
        if(Client != null)
            Client.Close();
    }

    public void StartSendingUDP()
    {
        _canConnect = true;
    }
}
