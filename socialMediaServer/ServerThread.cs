using SocketAbi;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class ServerThread
    {
        Socket client;
        public ServerThread(Socket cs) 
        {
            this.client = cs;
        }

        public void HandleConnection()
        {
            while(true) 
            {
                
            }
        }
    }
}
