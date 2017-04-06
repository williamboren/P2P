using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace P2P
{
    public class Server
    {
        private TcpListener listener; // a server to accept connections and data
        private Thread listenerThread; // a thread to handle incoming data, would interrupt all other operations otherwise
        private delegate void GetData(string data); // delegate to handle events
        private List<string> connectedClients = new List<string>(); // list to keep track of connected clients
        private int port;

        // konstruktor, tar en string och en int
        public Server(int p)
        {
            this.port = p;

            // creates a server/listener that listens on any IP address
            listener = new TcpListener(IPAddress.Any, p);
            listener.Start();

            // creates the server thread and sets it up
            ThreadStart start = new ThreadStart(Listener);
            listenerThread = new Thread(start);
            listenerThread.IsBackground = true;
            listenerThread.Start();
        }

        private void Listener()
        {
            //GetData dataDelegate = HandleData;

            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient lClient = listener.AcceptTcpClient();
                Console.WriteLine("Incoming connection from {0}", ((IPEndPoint)lClient.Client.RemoteEndPoint).Address.ToString());
                connectedClients.Add(((IPEndPoint)lClient.Client.RemoteEndPoint).Address.ToString());
                NetworkStream stream = lClient.GetStream();
                byte[] bytes = new byte[256];
                string data = null;
                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    //dataDelegate(data);
                    Console.WriteLine("Received: {0}", data);
                }
                stream.Close();
                lClient.Close();
            }
        }

        /*private void HandleData(string data)
        {
            foreach (string c in connectedClients)
            {
                TcpClient cli = new TcpClient(c, Port);
                NetworkStream stream = cli.GetStream();

                if (cli.Connected)
                {
                    byte[] d = System.Text.Encoding.ASCII.GetBytes(data, 0, data.Length);

                    stream.Write(d, 0, d.Length);
                }

                stream.Close();
            }
        }*/

        public int Port { get { return this.port; } }

        public void StopServer()
        {
            listenerThread.Abort();
            listener.Stop();
        }
    }
}
