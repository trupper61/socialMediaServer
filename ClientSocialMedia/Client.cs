using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using socialMediaServer;
using SocketAbi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ClientSocialMedia
{
    public class Client
    {
        public SocketAbi.Socket clientSocket;

        public Client()
        {
            //IPAddress adress = IPAddress.Parse("10.1.2.186");
            this.clientSocket = new SocketAbi.Socket("localhost", 5555);
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
            clientSocket.Write("anmelden;" + eingabe +'\n');
            string msg = clientSocket.ReadLine();
            MessageBox.Show(msg);
            //List<string> bilder = BilderAuswaehlen();
            //msg = $"beitrag;Hallo Welt;{bilder.Count}";
            //clientSocket.Write($"{msg}{PictureMessage(bilder)};Wow das ist ja ein cooler Beitrag!\n");
            //MessageBox.Show(clientSocket.ReadLine());
        }

        public void registrieren(string benutzername, string passwort, string email) 
        {
            string eingabe = $"{benutzername};{passwort};{email}";
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
        public void BeitraegeAuspacken(string msg)
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
                    string msg = $";{System.IO.Path.GetFileName(path)}|{picture}";
                    bilder.Add(msg);
                }
            }
            return bilder;
        }
        public void beitragSenden(string titel, List<string> bilder) 
        {
            string eingabe = $"{titel};{bilder.Count}";
            foreach (string bild in bilder) 
            {
                eingabe += bild;
            } 
            
            clientSocket.Write("beitrag;" + eingabe + '\n');
        }

        public List<Beitrag> beitraegeAnfragen()
        {
            clientSocket.Write("neueBeitraege\n");
            string str;
            str = clientSocket.ReadLine();
            //string[] dataRecieved = str.Split(';');
            //foreach (string data in dataRecieved)
            //{
            //    string[] relevantData = data.Split('|');
            //    string titel = relevantData[1];
            //    string text = relevantData[2];
            //    string autor = relevantData[3];
            //    int likes = Convert.ToInt32(relevantData[4]);

            //    string[] images = data.Split(',');
            //    string[] imageData = null;
            //    string[] imageName = null;
            //    int counter = 0;
            //    foreach (string image in images) 
            //    {
            //        string[] innerData = image.Split(':');
            //        imageData[counter] = innerData[1];
            //        imageName[counter] = innerData[0];
            //    }
            //}
            //return null;
            List<Beitrag> beitraege = new List<Beitrag>();
            string[] dataReceived = str.Split(';');
            int anzahl = Convert.ToInt32(dataReceived[1]);
            for (int i= 0; i < anzahl; i++)
            {
                string beitragString = dataReceived[i + 2];
                string[] relevantData = beitragString.Split('|');
                int id = Convert.ToInt32(relevantData[0]);
                string titel = relevantData[1];
                string text = relevantData[2];
                string autor = relevantData[3];
                List<Bild> bilder = new List<Bild>();
                int likes = Convert.ToInt32(relevantData[4]);
                DateTime timestamp = Convert.ToDateTime(relevantData[5]);
                string[] images = relevantData[6].Split(',');

                foreach (string image in images)
                {
                    string[] innerData = image.Split(':');
                    if (innerData.Length == 2)
                    {
                        string imageName = innerData[0];
                        string imageData = innerData[1];
                        byte[] imageBytes = Convert.FromBase64String(imageData);
                        File.WriteAllBytes(imageName, imageBytes);
                        bilder.Add(new Bild(imageName));
                    }
                }
                Beitrag b = new Beitrag(new Nutzer(autor, "", "", 0), titel, bilder);
                b.Id = id;
                b.setAnzahlLikes(likes);
                b.setGeposted(timestamp);
                if (!string.IsNullOrEmpty(text))
                    b.ErstelleText(text);
                beitraege.Add(b);
            }
            return beitraege;
        }
    }
}
