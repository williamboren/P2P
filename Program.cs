using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace P2P
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateServer();
            //CreateClient("127.0.0.1", 3333);
            Console.ReadKey();
        }

        static void CreateServer()
        {
            Server server = new Server(3333); // creates a new server and starts listening for connections on the specified port
            server.ReceivedData += HandleData;
        }

        static void CreateClient(string ip, int port)
        {
            Client client = new Client(ip, port);

            List<string> items = new List<string>();
            items.Add("sword");
            items.Add("potion");
            Player player = new Player(100, "atk", items, 50, 50, 0.5f, 0.2f);
            player.TakeDamage(10);

            string data = JsonConvert.SerializeObject(player);
            
            if (client.Connected)
            {
                if (client.SendData(data)) Console.WriteLine("Success");
                else Console.WriteLine("Failed to send data");
            }
        }

        static void HandleData(object s, ReceivedDataEventArgs e)
        {
            Player data = JsonConvert.DeserializeObject<Player>(e.Data);
            Console.WriteLine(data.HealthPoints);
            data.TakeDamage(20);
            Console.WriteLine(data.HealthPoints);
        }
    }

    class Player
    {
        private int health_points;
        private string pClass;
        private List<string> inventory;
        private float posx, posy;
        private float speedx, speedy;
        private bool isAlive;

        public Player(int hp, string c, List<string> items, float x, float y, float sx, float sy)
        {
            this.health_points = hp;
            this.pClass = c;
            this.posx = x;
            this.posy = y;
            this.speedx = sx;
            this.speedy = sy;
            this.inventory = new List<string>(items);
            this.isAlive = true;
        }

        public void AddItem(string item)
        {
            inventory.Add(item);
        }

        public void RemoveItem(string item)
        {
            inventory.Remove(item);
        }

        public void TakeDamage(int damage)
        {
            int new_hp = this.health_points - damage;

            if (new_hp <= 0) this.isAlive = false;
            else this.health_points = new_hp;
        }

        public void UpdatePos()
        {
            posx += speedx;
            posy += speedy;
        }

        public int HealthPoints { get { return this.health_points; } set { this.health_points = value; } }
        public string Class { get { return this.pClass; } set { this.pClass = value; } }
        public List<string> Inventory { get { return this.inventory; } set { this.inventory = value; } }
        public bool IsAlive { get { return this.isAlive; } set { this.isAlive = value; } }
        public float SpeedX { get { return this.speedx; } set { this.speedx = value; } }
        public float SpeedY { get { return this.speedy; } set { this.speedy = value; } }
        public float PositionX { get { return this.posx; } set { this.posx = value; } }
        public float PositionY { get { return this.posy; } set { this.posy = value; } }
    }
}
