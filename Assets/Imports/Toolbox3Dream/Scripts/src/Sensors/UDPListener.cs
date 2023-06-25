using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine;

class UDPListener
{
    private int m_portToListen = 2003;
    public int PortToListen
    {
        get { return m_portToListen; }
        set { m_portToListen = value; }
    }


    private UdpClient listener;
    private IPEndPoint groupEP;
    private volatile bool listening = false;


    Thread m_ListeningThread;
    //public event EventHandler<NewMessageArgs> NewMessageReceived;

    // Constructor
    public UDPListener()
    {
        listening = false;
    }

    public UDPListener(int portNumber)
    {
        m_portToListen = portNumber;
        listening = false;

        // NeuroRehabTranslator
        //UdpGenericTranslator.InitializeTranslator();
    }

    ~UDPListener()
    {
        if (listening)
            listening = false;

        if (m_ListeningThread.IsAlive)
            m_ListeningThread.Abort();
    }

    public void StartListener()
    {
        if (!listening)
        {
            m_ListeningThread = new Thread(ListenForUDPPackages);
            listening = true;
            m_ListeningThread.Start();
        }
    }

    public void StopListener()
    {
        listening = false;
    }

    private void ListenForUDPPackages()
    {
        listener = null;
        try
        {
            listener = new UdpClient(m_portToListen);
        }
        catch (SocketException)
        {
            //do nothing
        }

        if (listener != null)
        {
            groupEP = new IPEndPoint(IPAddress.Any, m_portToListen);

            try
            {
                while (listening)
                {
                    Console.WriteLine("Waiting for UDP broadcast to port " + m_portToListen);
                    byte[] bytes = listener.Receive(ref groupEP);

                    ////raise event                        
                    //NewMessageReceived(this, new NewMessageArgs(bytes));

                    ///// Multithreading
                    //listener.BeginReceive(AsyncRcvData, listener);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
                Console.WriteLine("Done listening for UDP broadcast");
            }
        }
    }

    private void AsyncRcvData(IAsyncResult res)
    {
        if (listening == false) return;
        byte[] data;

        try
        {
            data = listener.EndReceive(res, ref groupEP);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return;
        }
        listener.BeginReceive(AsyncRcvData, listener);

        var message = Encoding.UTF8.GetString(data);

        if (message != string.Empty)
        {
            try
            {
                message = message.ToLower();
                var split = message.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in split)
                {
                    //UdpGenericTranslator.TranslateData(line);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError(e.StackTrace);
            }
        }
    }

}
