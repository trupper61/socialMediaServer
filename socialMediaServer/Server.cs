using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketAbi;

namespace socialMediaServer
{
    public class Server
    {
        ServerSocket Serversocket;

        public Server(int port) 
        {
            this.Serversocket = new ServerSocket(port);
            runServer();
        }

        public void runServer() 
        {
            while(true) 
            {
                Socket client = Serversocket.Accept();
                ServerThread thread = new ServerThread(client);
                Thread tc = new Thread(new ThreadStart(thread.HandleConnection));
            }
            
        } 
    }
}