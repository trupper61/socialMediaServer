using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketAbi;

namespace ClientSocialMedia
{
    public class Client
    {
        public SocketAbi.Socket clientSocket;

        public Client()
        {
            //IPAddress adress = IPAddress.Parse("10.1.2.186");
            this.clientSocket = new SocketAbi.Socket("127.0.0.1", 5555);
            Verbinden();
        }

        public bool Verbinden() 
        {
            bool status = clientSocket.Connect();
            return status;
        }
        public void anmelden(string benutzername, string passwort) 
        {
            string eingabe = $"{benutzername};{passwort}";
            //clientSocket.Write("anmelden;" + eingabe + ";test1233@gmx.de" +'\n');
            //string msg = clientSocket.ReadLine();
            //MessageBox.Show(msg);
        }

        public void registrieren(string benutzername, string passwort) 
        {
            string eingabe = $"{benutzername};{passwort}";
            clientSocket.Write("registrieren;"+eingabe+'\n');
            string msg = clientSocket.ReadLine();
            MessageBox.Show(msg);
        }
    }
}
