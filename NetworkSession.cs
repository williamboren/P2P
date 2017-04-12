using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace P2P
{
    abstract class NetworkSession
    {
        protected int id;
        // yada yada more variables

        public void CreateSession()
        {

        }
    }

    public class LobbySession : NetworkSession
    {
        public LobbySession()
        {

        }
    }

    public class GameSesison : NetworkSession
    {
        public GameSesison()
        {

        }
    }
}
