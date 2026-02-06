using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class Client
    {
        private Socket clientSocket;

        public Client() 
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public bool Verbinden(string server, int port) 
        {
            try
            {
                clientSocket.Connect(server, port);
            }
            catch (Exception e) 
            {

                return false;
            }
            return true;
        }
    }
}
