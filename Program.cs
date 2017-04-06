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
            Console.ReadKey();
        }

        static void CreateServer()
        {
            Server server = new Server(3333); // creates a new server and starts listening for connections on the specified port
        }
    }
}
