using SocketAbi;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class ServerThread
    {
        private SocialMediaPlatform spf;
        public SocketAbi.Socket client;
        private Nutzer nutzer;
        public ServerThread(SocketAbi.Socket cs) 
        {
            this.client = cs;
            spf = new SocialMediaPlatform();
        }

        public void HandleConnection()
        {
            Console.WriteLine("Client hat sich verbunden!");
            try
            {
                while (true)
                {
                    string befehl = client.ReadLine();
                    Console.WriteLine($"Empfangen: {befehl}");

                    string[] parameter = befehl.Split(';');
                    if (parameter.Length == 0)
                        continue;
                    string command = parameter[0];
                    switch (command)
                    {
                        case "anmelden":
                            string name = parameter[1];
                            string password = parameter[2];
                            Nutzer nutzer = spf.Anmelden(name, password);
                            if (nutzer != null)
                            {
                                this.nutzer = nutzer;
                                client.Write("+OK Willkommen \n");
                            }
                            else
                            {
                                client.Write("-Error \n");
                            }
                            break;
                        case "registrieren":
                            name = parameter[1];
                            password = parameter[2];
                            string email = parameter[3];
                            int code = spf.Registrieren(name, password, email);
                            if (code == -1)
                                client.Write("-Error;NameOrEmailVorhanden\n");
                            else
                                client.Write("+OK Registrierung Erfolg\n");
                            break;
                        case "beitrag":
                                    string titel = parameter[1];
                                    string bildDateiname = parameter[2];
                                    string text;
                                    Bild picture = new Bild(bildDateiname);
                                    Beitrag b;
                                    if (parameter.Length >= 4)
                                    {
                                        text = parameter[3];
                                        b = this.nutzer.ErstelleBeitrag(titel, picture, text);
                                    }
                                    else
                                    {
                                        text = null;
                                        b = this.nutzer.ErstelleBeitrag(titel, picture);
                                    }
                                    spf.ErstelleBeitrag(b, picture);
                                    client.Write("+OK Hochgeladen \n");
                                    break;
                                }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fehler: {e.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client getrennt.");
            }
        }
    }
}
