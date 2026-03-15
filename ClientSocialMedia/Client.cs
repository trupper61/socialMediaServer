using socialMediaServer;
using SocketAbi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ClientSocialMedia
{
    public class Client //Diese Klasse ist verantwortlich für alle Funktionen rund um die Kommunikation zwischen Client und Server.
    {
        public SocketAbi.Socket clientSocket;
        private string benutzername;
        public Action<Beitrag> OnBeitragErhalten;
        public Action OnConnectionLost;
        private string hostname = "91.63.107.81";
        private int port = 5555;
        public Client()
        {
            //IPAddress adress = IPAddress.Parse("10.1.2.186");
            this.clientSocket = new SocketAbi.Socket("localhost", 5555);
            Verbinden();
        }
        /// <summary>
        /// Versucht, eine Verbindung zum Server herzustellen und testet die Kommunikation mit einem Test
        /// Gibt true zurück wenn das funktioniert.
        /// Andernfalls wird false zurüclgegeben
        /// </summary>
        public bool Verbinden() 
        {
            try
            {
                clientSocket = new SocketAbi.Socket(hostname, port);
                clientSocket.Connect();
                clientSocket.Write("test\n");
                clientSocket.ReadLine();
                return true;
            }
            catch
            {
                ConnectionLost();
                return false;
            }
        }
        public string anmelden(string benutzername, string passwort) 
        {
            string eingabe = $"{ConvertMessage(benutzername)};{ConvertMessage(passwort)}";
            if (!Write("anmelden;" + eingabe + '\n'))
                return null;
            string msg = ReadLine();
            if (msg == null) return null;
            this.benutzername = benutzername;
            return msg;
        }

        public void registrieren(string benutzername, string passwort, string email) 
        {
            string eingabe = $"{ConvertMessage(benutzername)};{ConvertMessage(passwort)};{ConvertMessage(email)}";
            if (!Write("registrieren;" + eingabe + '\n'))
                return;

            string msg = ReadLine();
            if (msg == null) return;
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
        public void beitragSenden(string titel, List<string> bilder, string tag, string text) 
        {
            string eingabe = $"{ConvertMessage(titel)};{bilder.Count}";
            foreach (string bild in bilder) 
            {
                eingabe += bild;
            } 
            
            if (!Write("beitrag;" + eingabe + ";" + ConvertMessage(tag) + ";" + ConvertMessage(text) + '\n'))
                return;
            string reply = ReadLine();
            if (reply == null) 
                return;
            if (reply.Split(';')[0] == "-")
                MessageBox.Show("Zu viele Bilder, maximal 10!");
        }

        public List<Image> HoleOriginalBilder(int beitragId)
        {
            string msg = $"original;{beitragId}\n";
            if (!Write(msg)) 
                return null;
            string reply = ReadLine();
            if (reply == null)
                return null;
            string[] parts = reply.Split(';');
            List<Image> bilder = new List<Image>();
            int anzahl = Convert.ToInt32(parts[1]);
            for (int i = 0; i < anzahl; i++)
            {
                byte[] bytes = Convert.FromBase64String(parts[2 + i]);
                using (MemoryStream ms =  new MemoryStream(bytes))
                {
                    Image img = Image.FromStream(ms);
                    bilder.Add(img);
                }
            }
            return bilder;
        }
        public string Like(int beitragId)
        {
            string msg = $"like;{beitragId}\n";
            if (!Write(msg))
                return null;
            return ReadLine();
        }

        public string Abonnieren(int abonnentId)
        {
            string msg = $"abonnieren;{abonnentId}\n";
            if (!Write(msg))
                return null;
            return ReadLine();
        }

        public int GetAbonnentenAnzahl(int nutzerId)
        {
            string msg = $"abonnentenAnzahl;{nutzerId}\n";
            if (!Write(msg))
                return 0;
            string reply = ReadLine();
            if (reply == null)
                return 0;
            if (reply.Trim(';')[0] == '+')
            {
                return Convert.ToInt32(reply.Split(';')[1]);
            }
            else
            {
                return 0;
            }
        }

        public List<Beitrag> HoleNutzerBeitraege(int offset = 0)
        {
            string msg = $"nutzerBeitraege;{offset}\n";
            if (!Write(msg))
                return null;
            List<Beitrag> beitraege = new List<Beitrag>();
            while (true)
            {
                string str = ReadLine();
                if (str == null)
                    break;
                if (str.Split(';')[1] == "fertig")
                    break;
                string[] dataDetail = str.Split(';');
                int id = Convert.ToInt32(dataDetail[1]);
                string titel = GetMessage(dataDetail[2]);
                string text = GetMessage(dataDetail[3]);
                int nutzerId = Convert.ToInt32(dataDetail[4]);
                int anzahlLikes = Convert.ToInt32(dataDetail[5]);
                DateTime geposted = Convert.ToDateTime(dataDetail[6]);
                string tag = dataDetail[8];

                List<Bild> bilder = new List<Bild>();
                string[] images = dataDetail[7].Split(',');
                foreach (string image in images)
                {
                    string[] innerData = image.Split(':');
                    string imageName = innerData[0];
                    string imageData = innerData[1];

                    Bild bild = new Bild(imageName);
                    bild.bilddata = imageData;
                    bilder.Add(bild);
                }
                Beitrag b = new Beitrag(new Nutzer("", "", "", nutzerId), titel, bilder, tag, text);
                b.Id = id;
                b.setAnzahlLikes(anzahlLikes);
                b.setGeposted(geposted);
                if (text != "")
                {
                    b.ErstelleText(text);
                }
                OnBeitragErhalten?.Invoke(b);
                beitraege.Add(b);
            }
            return beitraege;
        }
        public void ErstelleKommentar(int beitragId, string nachricht, int? oberKommentarId)
        {
            string msg = $"kommentar;{beitragId};{ConvertMessage(nachricht)}\n";
            if (!Write(msg))
                return;
            string reply = ReadLine();
            if (reply == null)
                return;
        }

        public List<Kommentar> LadeKommentare(int beitragId)
        {
            string msg = $"ladeKommentare;{beitragId}\n";
            if (!Write(msg))
                return null;
            string data = ReadLine();
            if (data == null)
                return null;
            string[] parts = data.Split(';');
            List<Kommentar> comments = new List<Kommentar>();
            if (parts[0] != "kommentare")
                return comments;
            int anzahl = Convert.ToInt32(parts[1]);
            
            
            for (int i = 0; i < anzahl; i++)
            {
                string[] commentData = parts[2 + i].Split('|');
                int id = Convert.ToInt32(commentData[0]);
                string nachricht = GetMessage(commentData[1]);
                int autor = Convert.ToInt32(commentData[2]);
                DateTime timestamp = Convert.ToDateTime(commentData[3]);
                string autorKommentar = GetMessage(commentData[4]);
                string profilAutor = commentData[5];
                string oberKommentar = commentData[6];
                Kommentar k;
                if (oberKommentar == "null") 
                {
                    k = new Kommentar(nachricht, timestamp, autor);
                }
                else
                {
                    k = new Kommentar(nachricht, timestamp, autor);
                }
                k.autor = autorKommentar;
                k.profil = profilAutor;
                comments.Add(k);
            }
            return comments;
        }
        public List<Beitrag> beitraegeAnfragen(bool nurAbos, bool empfehlungen, bool beliebteste, int offset = 0)
        {
            //Der typ der zu erhaltenen Beiträgen wird mittels 3 bools bestimmt.
            if (nurAbos)
            {
                if (!Write($"nurAbos;{offset}\n"))
                    return null;
            }
            else if(empfehlungen) 
            {
                if (!Write($"empfehlung;{offset}\n"))
                    return null;
            }
            else if(beliebteste) 
            {
                if (!Write($"beliebteste;{offset}\n"))
                    return null;
            }
            else
            {
                if (!Write($"neueBeitraege;{offset}\n"))
                    return null;
            }
            List<Beitrag> beitraege = new List<Beitrag>();

            //Nach dem festgelegten Protokoll: +;anzahlBeitraege;id;titel;text;autor;anzahlLikes;timestamp;dateinamen1:bild1,dateinamen2:bild2,..,dateinamenN:bildn;... 
            //werden alle relevanten Daten bei Empfang gespalten und ihre jeweiligen Variablen zugewiesen.
            while (true)
            {
                string str = ReadLine();
                if (str == null)
                    break;
                if (str.Split(';')[1] == "fertig")
                    break;
                string[] dataDetail = str.Split(';');
                int id = Convert.ToInt32(dataDetail[1]);
                string titel = GetMessage(dataDetail[2]);
                string text = GetMessage(dataDetail[3]);
                int nutzerId = Convert.ToInt32(dataDetail[4]);
                int anzahlLikes = Convert.ToInt32(dataDetail[5]);
                DateTime geposted = Convert.ToDateTime(dataDetail[6]);
                string tag = dataDetail[8];

                List<Bild> bilder = new List<Bild>();
                string[] images = dataDetail[7].Split(',');
                foreach (string image in images)
                {
                    string[] innerData = image.Split(':');
                    string imageName = innerData[0];
                    string imageData = innerData[1];

                    Bild bild = new Bild(imageName);
                    bild.bilddata = imageData;
                    bilder.Add(bild);
                }
                Beitrag b = new Beitrag(new Nutzer("", "", "", nutzerId), titel, bilder, tag, text);
                b.Id = id;
                b.setAnzahlLikes(anzahlLikes);
                b.setGeposted(geposted);
                if (text != "")
                {
                    b.ErstelleText(text);
                }
                OnBeitragErhalten?.Invoke(b);
                beitraege.Add(b);
            }
            return beitraege;
        }
        
        public Nutzer LadeProfil()
        {
            if (!Write($"loadProfile\n"))
                return null;
            string msg = ReadLine();
            if (msg == null)
                return null;
            string[] parts = msg.Split(';');
            string name = GetMessage(parts[1]);
            string email = GetMessage(parts[2]);
            int id = Convert.ToInt32(parts[3]);
            DateTime zuletztAktiv = Convert.ToDateTime(parts[4]);
            int abonnenten = Convert.ToInt32(parts[5]);
            Nutzer n = new Nutzer(name, "", email, id);
            n.ZuletztAktiv = zuletztAktiv;
            n.AbonnentenAnzahl = abonnenten;
            string base64 = parts[6];
            n.ProfilBild = base64;

            return n;
        }
        public Nutzer LadeNutzer(int nutzerId)
        {
            if (!Write($"loadNutzer;{nutzerId}\n"))
                return null;
            string msg = ReadLine();
            if (msg == null)
                return null;
            string[] parts = msg.Split(';');
            string name = GetMessage(parts[1]);
            int id = Convert.ToInt32(parts[2]);
            DateTime zuletztAktiv = Convert.ToDateTime(parts[3]);
            int abonnenten = Convert.ToInt32(parts[4]);
            Nutzer n = new Nutzer(name, "", "", id);
            n.ZuletztAktiv = zuletztAktiv;
            n.AbonnentenAnzahl = abonnenten;
            string base64 = parts[5];
            n.ProfilBild = base64;
            return n;
        }

        public List<Nutzer> SucheNutzer(string name)
        {
            if (!Write($"sucheNutzer;{ConvertMessage(name)}\n"))
                return null;
            string reply = ReadLine();
            if (reply == null)
                return null;
            string[] parts = reply.Split(';');
            int anzahl = Convert.ToInt32(parts[1]);
            List<Nutzer> nutzerListe = new List<Nutzer>(); 
            for(int i = 0; i < anzahl; i++)
            {
                string[] data = parts[i + 2].Split('|');
                string nutzerName = GetMessage(data[0]);
                int id = Convert.ToInt32(data[1]);
                int abonnenten = Convert.ToInt32(data[2]);
                string profil = data[3];
                Nutzer n = new Nutzer(nutzerName, "", "", id);
                n.AbonnentenAnzahl = abonnenten;
                n.ProfilBild = profil;
                nutzerListe.Add(n);
            }
            return nutzerListe;
        }

        private bool Write(string msg)
        {
            try
            {
                clientSocket.Write(msg);
                return true;
            }
            catch
            {
                ConnectionLost();
                return false;
            }
        }

        private string ReadLine()
        {
            try
            {
                return clientSocket.ReadLine();
            }
            catch
            {
                ConnectionLost();
                return null;
            }
        }
        /// <summary>
        /// Methode wird aufgerufen, wenn die Verbindung zum Server verloren geht.
        /// Es wird ein Event ausgelösen, welches bei der Form für einen Halt in der Kommunikation führen sollte,
        /// wie auch ein Zurückwerfen zum Login-Fenster
        /// </summary>
        private void ConnectionLost()
        {
            try
            {
                clientSocket.Close(); 
            }
            catch 
            { 

            }
            OnConnectionLost?.Invoke();
        }
        public byte[] LadeProfilePicture()
        {
            if (!Write("loadProfile\n"))
                return null;
            string msg = ReadLine();
            if (msg == null)
                return null;
            string[] parts = msg.Split(';');
            string base64 = parts[6];
            return Convert.FromBase64String(base64);
        }

        public string ProfilAktualisieren(string name, string email)
        {
            string msg = $"updateProfile;{ConvertMessage(name)};{ConvertMessage(email)}\n";
            if (!Write(msg))
                return null;
            return ReadLine();
        }

        public string ProfilBild(string fileName, string picture)
        {
            string msg = $"addProfilePicture;{fileName};{picture}\n";
            if (!Write(msg))
                return null;
            return ReadLine();
        }

        public string PasswortAktualisieren(string old, string newP)
        {
            string msg = $"updatePasswort;{ConvertMessage(old)};{ConvertMessage(newP)}\n";
            if (!Write(msg))
                return null;
            return ReadLine();
        }

        public string PasswortVergessenAktualisierung(string email) 
        {
            string msg = $"newPasswort;{ConvertMessage(email)}\n";
            clientSocket.Write(msg);
            return clientSocket.ReadLine();
        }

        public int ChatErstellen(int nutzerId)
        {
            string msg = $"chatErstellen;{nutzerId}\n";
            if (!Write(msg))
                return 0;
            string reply = ReadLine();
            if (reply == null)
                return 0;
            string[] parts = reply.Split(';');
            if (parts[0] == "+")
            {
                return Convert.ToInt32(reply.Split(';')[1]);
            }
            else
            {
                return -1;
            }
        }

        public List<Chat> LadeChats()
        {
            if (!Write("chatListe\n"))
                return null;
            string reply = ReadLine();
            if (reply == null)
                return null;
            List<Chat> chats = new List<Chat>();
            string[] parts = reply.Split(';');
            int anzahl = Convert.ToInt32(parts[1]);
            for (int i = 0; i < anzahl; i++)
            {
                string[] data = parts[i + 2].Split('|');
                int id = Convert.ToInt32(data[0]);
                string name = GetMessage(data[1]);
                string nachricht = null;
                DateTime? time = null;
                if (data[2] != "")
                {
                    nachricht = GetMessage(data[2]);
                    time = Convert.ToDateTime(data[3]);
                }
                string profil = data[4];
                Chat c = new Chat(id);
                c.SetData(name, profil, nachricht, time);
                chats.Add(c);
            }
            return chats;
        }

        public string SendeNachricht(int chat, string text)
        {
            string msg = $"nachrichtSenden;{chat};{ConvertMessage(text)}\n";
            if (!Write(msg))
                return null;
            string reply = ReadLine();
            return reply;
        }

        public List<Nachricht> LadeNachrichten(int chat, int offset)
        {
            if (!Write($"loadNachrichten;{chat};{offset}\n"))
                return null;
            string reply = ReadLine();
            if (reply == null ) 
                return null;
            List<Nachricht> nachrichten = new List<Nachricht>();
            string[] parts = reply.Split(';');
            if (parts[0] != "+")
                return nachrichten;
            int anzahl = Convert.ToInt32(parts[1]);
            for (int i = 0; i < anzahl; i++)
            {
                string[] data = parts[2 + i].Split('|');
                int benutzerId = Convert.ToInt32(data[0]);
                string name = GetMessage(data[1]);
                string text = GetMessage(data[2]);
                DateTime gesendetAm = Convert.ToDateTime(data[3]);
                int nachrichtId = Convert.ToInt32(data[4]);
                string profil = data[5];
                Nutzer n = new Nutzer(name, "", "", benutzerId);
                n.ProfilBild = profil;
                nachrichten.Add(new Nachricht(chat, n, text, gesendetAm, nachrichtId));
            }
            return nachrichten;
        }
        
        public string Abmelden()
        {
            string msg = "abmelden\n";
            if (!Write(msg))
                return null;
            return ReadLine();
        }
        private string ConvertMessage(string message)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
        }
        private string GetMessage(string message)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(message));
        }
    }
}
