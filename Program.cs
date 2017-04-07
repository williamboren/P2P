using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2P
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateServer();
            CreateClient("127.0.0.1", 3333);
            Console.ReadKey();
        }

        static void CreateServer()
        {
            Server server = new Server(3333); // creates a new server and starts listening for connections on the specified port
        }

        static void CreateClient(string ip, int port)
        {
            Client client = new Client(ip, port);
            Console.WriteLine("Enter a message to send:");
            string msg = Console.ReadLine();
            if (client.SendData(msg)) Console.WriteLine("Success");
            else Console.WriteLine("Failed to send data");
        }
    }
}
