using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketAbi;

namespace socialMediaServer
{
    public class Server
    {
        ServerSocket Serversocket;
        private SocialMediaPlatform spf;
        public Server(int port) 
        {
            this.Serversocket = new ServerSocket(port);
            spf = new SocialMediaPlatform();
            runServer();
        }

        public void runServer() 
        {
            int result = spf.Registrieren("bernd", "1234", "test@gmx.de");
            if (result == 0)
                Console.WriteLine("Anmeldung erfolgreich");
            else if (result == -1)
                Console.WriteLine("Test erfolgreich: Nutzernamen oder E-Mail bereits vergeben");
            else
                Console.WriteLine("Test fehlgeschlagen");



            Nutzer n = spf.Anmelden("bernd", "1234");
            Bild p = new Bild("hallo");
            Beitrag b = new Beitrag(n, "Hello World", p);
            spf.ErstelleBeitrag(b, p);
            Console.WriteLine("Beitrag erfolgreich");

                while (true)
                {
                    Socket client = Serversocket.Accept();
                    ServerThread thread = new ServerThread(client);
                    Thread tc = new Thread(new ThreadStart(thread.HandleConnection));
                    tc.Start();
                }
            
        } 
    }
}