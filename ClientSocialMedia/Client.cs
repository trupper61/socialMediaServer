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
            //clientSocket.Write("registrieren;" + eingabe + ";test1233@gmx.de" +'\n');  // Registrieren
            //MessageBox.Show(clientSocket.ReadLine());
            //clientSocket.Write("anmelden;" + eingabe + '\n');
            //MessageBox.Show(clientSocket.ReadLine());
            //List<string> bilder = BilderAuswaehlen();  // Beitrag erstellen mit max 10 Bildern
            //string msg = $"beitrag;Hallo Welt;{bilder.Count};";
            //clientSocket.Write($"{PictureMessage(bilder)}Wow das ist ja was verrücktes!\n");
            //MessageBox.Show(clientSocket.ReadLine());
            //clientSocket.Write("neueBeitraege\n");
            //Test(clientSocket.ReadLine());
        }

        // Todo: Titel Test; Sämtliche Daten teilen
        public void Test(string msg)
        {
            string titel = "";
            string[] parts = msg.Split(';');
            int anzahl = int.Parse(parts[1]);
            for (int i = 0; i < anzahl; i++)
            {
                string beitragString = parts[2 + i];
                string[] felder = beitragString.Split('|');
                titel += felder[1];
            }
            MessageBox.Show(titel);
        }
        public string PictureMessage(List<string> bilder)
        {
            string msg = "";
            foreach (string bild in bilder)
                msg += bild;
            return msg;
        }
        /// <summary>
        /// Opens a dialog lets the user select pictures and encodes them to base64 (bytes just as strings) 
        /// Adds them to a list with their filename.
        /// </summary>
        public static List<string> BilderAuswaehlen()
        {
            List<string> bilder = new List<string>();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Bilder auswählen";
            dialog.Filter = "Bilder (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach(string path in dialog.FileNames)
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(path);  // Credits: https://stackoverflow.com/questions/1497997/reliable-way-to-convert-a-file-to-a-byte
                    string picture = Convert.ToBase64String(bytes);
                    string msg = $"{System.IO.Path.GetFileName(path)}|{picture};";
                    bilder.Add(msg);
                }
            }
            return bilder;
        }
    }
}
