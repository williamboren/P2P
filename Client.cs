using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace P2P
{
    public class Client
    {
        private int port;
        private string ipaddr;
        private TcpClient client;

        public Client(string a, int p)
        {
            this.port = p;
            this.ipaddr = a;

            client = new TcpClient(a, p);
        }

        public bool SendData(string data)
        {
            // try/catch for error "handling"
            try
            {
                NetworkStream stream = client.GetStream();

                if (client.Connected)
                {
                    byte[] d = System.Text.Encoding.ASCII.GetBytes(data);

                    stream.Write(d, 0, d.Length);
                }

                stream.Close();

                return true;
            }
            catch (ArgumentNullException e)
            {
                return false;
            }
            catch (SocketException e)
            {
                return false;
            }
        }
    }

    static void CreateClient()
    {
        Client client = new Client("192.168.0.18", 3333);
        Console.WriteLine("Enter a message to send:");
        string msg = Console.ReadLine();
        if (client.SendData(msg)) Console.WriteLine("Success");
        else Console.WriteLine("Failed to send data");
    }
}