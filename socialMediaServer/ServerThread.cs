using SocketAbi;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime;
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
        private static string imgOrdner = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img"); // Ordner, wo sich die Bilder befinden

        public ServerThread(SocketAbi.Socket cs) 
        {
            this.client = cs;
            spf = new SocialMediaPlatform();
        }

        public void HandleConnection()
        {
            try
            {
                while (true)
                {

                    string befehl = client.ReadLine();

                    string[] parameter = befehl.Split(';'); 
                    if (parameter.Length == 0)
                        continue;
                    string command = parameter[0];
                    switch (command)
                    {
                        case "anmelden":
                            string name = GetMessage(parameter[1]);
                            string password = GetMessage(parameter[2]);
                            Nutzer nutzer = spf.Anmelden(name, password);
                            if (nutzer != null)
                            {
                                this.nutzer = nutzer;
                                client.Write("+;Willkommen\n");
                            }
                            else
                            {
                                client.Write("-;falscheAnmeldedaten\n");
                            }
                            break;
                        case "registrieren":
                            name = GetMessage(parameter[1]);
                            password = GetMessage(parameter[2]);
                            string email = GetMessage(parameter[3]);
                            int code = spf.Registrieren(name, password, email);
                            if (code == -1)
                                client.Write("-;NameOrEmailVorhanden\n");
                            else
                                client.Write("+;registrierungerfolg\n");
                            break;
                        case "abmelden":
                            client.Write($"Bis zum nächsten Mal {ConvertMessage(this.nutzer.BenutzerName)}!\n");
                            this.nutzer = null;
                            break;
                        case "beitrag":
                            string titel = GetMessage(parameter[1]);
                            int anzahl = Convert.ToInt32(parameter[2]);
                            string tag = GetMessage(parameter[3 + anzahl]);
                            string text = GetMessage(parameter[4 + anzahl]);
                            if (anzahl > 10)
                            {
                                client.Write("-;fehler;max10\n");
                                break;
                            }
                            List<string> dateinamen = new List<string>();
                            for (int i = 0; i < anzahl; i++)
                            {
                                string[] pieces = parameter[3 + i].Split('|');

                                if (pieces.Length != 2)
                                {
                                    client.Write("-;error while transmitting\n");
                                    continue;
                                }
                                string dateiname = pieces[0];

                                byte[] bytes = Convert.FromBase64String(pieces[1]);
                                Image image;
                                using (MemoryStream ms = new MemoryStream(bytes))
                                {
                                    image = Image.FromStream(ms);
                                }
                                image = SocialMediaPlatform.CropToSquare(image);
                                image = SocialMediaPlatform.ResizeImage(image);

                                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(dateiname);
                                string pfad = Path.Combine(imgOrdner, "original", uniqueName);
                                string pfad2 = Path.Combine(imgOrdner, "preview", uniqueName);

                                File.WriteAllBytes(pfad, bytes);
                                image.Save(pfad2);

                                dateinamen.Add(uniqueName);
                            }
                            if (parameter.Length > 4 + anzahl)
                                

                            spf.ErstelleBeitrag(this.nutzer, titel, text, dateinamen, tag);
                            client.Write("+;Hochgeladen\n");
                            break;
                        case "neueBeitraege":
                            string msg = "";
                            int offset = 0;
                            if (parameter[1] != null)
                            {
                                offset = Convert.ToInt32(parameter[1]);
                            }
                            List<Beitrag> beitraege = spf.ErmittleNeueBeitraege(this.nutzer, offset);
                            foreach (Beitrag b in beitraege)
                            {
                                foreach (Bild bild in spf.HoleBilder(b.Id))
                                {
                                    b.Hinzufuegen(bild);
                                }
                                List<string> bilderStrings = new List<string>();
                                foreach (Bild imgs in b.Bilder)
                                {
                                    string s = Convert.ToBase64String(File.ReadAllBytes(Path.Combine("img", "preview", imgs.Dateiname)));

                                    bilderStrings.Add($"{imgs.Dateiname}:{s}");
                                }
                                string pictues = string.Join(",", bilderStrings);
                                string textBeitrag = b.Text.text;
                                if(textBeitrag != null) 
                                {
                                    textBeitrag = ConvertMessage(b.Text.text);
                                }
                                msg = $"+;{b.Id};{ConvertMessage(b.Titel)};{textBeitrag};{b.Autor.BenutzerId};{b.gebeAnzahlLikes()};{b.Geposted};{pictues};{b.Tag}\n";
                                client.Write(msg);
                            }
                            client.Write("+;fertig\n");
                            break;
                        case "nurAbos":
                            offset = 0;
                            if (parameter[1] != null)
                            {
                                offset = Convert.ToInt32(parameter[1]);
                            }
                            List<Beitrag> nurAboBeitraege = spf.BeitraegeVonAbosHolen(this.nutzer, offset);
                            foreach (Beitrag b in nurAboBeitraege)
                            {
                                foreach (Bild bild in spf.HoleBilder(b.Id))
                                {
                                    b.Hinzufuegen(bild);
                                }
                            }
                            // Protokoll: +;anzahlBeitraege;id;titel;text;autor;anzahlLikes;timestamp;dateinamen1:bild1,dateinamen2:bild2,..,dateinamenN:bildn;...
                            msg = "";
                            if (nurAboBeitraege.Count == 0)
                            {
                                msg = $"aboBeitraege?{nurAboBeitraege.Count}?";
                            }
                            foreach (Beitrag b in nurAboBeitraege)
                            {
                                foreach (Bild bild in spf.HoleBilder(b.Id))
                                {
                                    b.Hinzufuegen(bild);
                                }
                                List<string> bilderStrings = new List<string>();
                                foreach (Bild imgs in b.Bilder)
                                {
                                    string s = Convert.ToBase64String(File.ReadAllBytes(Path.Combine("img", "preview", imgs.Dateiname)));

                                    bilderStrings.Add($"{imgs.Dateiname}:{s}");
                                }
                                string pictues = string.Join(",", bilderStrings);
                                string textBeitrag = b.Text.text;
                                if (textBeitrag != null)
                                {
                                    textBeitrag = ConvertMessage(b.Text.text);
                                }

                                msg = $"+;{b.Id};{ConvertMessage(b.Titel)};{textBeitrag};{b.Autor.BenutzerId};{b.gebeAnzahlLikes()};{b.Geposted};{pictues};{b.Tag}\n";
                                client.Write(msg);
                            }
                            client.Write("+;fertig\n");
                            break;
                        case "original":
                            int beitragId = Convert.ToInt32(parameter[1]);
                            List<string> bilder = spf.HoleOriginalBilder(beitragId);
                            msg = $"+;{bilder.Count}";
                            foreach (string s in bilder)
                            {
                                byte[] pictureBytes = File.ReadAllBytes(Path.Combine(imgOrdner, "original", s));
                                string base64 = Convert.ToBase64String(pictureBytes);
                                msg += $";{base64}";
                            }
                            client.Write(msg + "\n");
                            break;
                        case "like":   // like;2 (BeitragId)
                            beitragId = Convert.ToInt32(parameter[1]);
                            int response = spf.Like(beitragId, this.nutzer.BenutzerId);
                            if (response == -1)
                            {
                                client.Write("-;Autor kann nicht selbst liken\n");
                            }
                            else if (response == -2)
                            {
                                client.Write("-;Autor hat bereits diesen Beitrag geliked\n");
                            }
                            else
                            {
                                client.Write("+;Liken Erfolgreich\n");
                            }
                            break;
                        case "abonnieren":
                            int abonnentId = Convert.ToInt32(parameter[1]);
                            response = spf.Abonnieren(this.nutzer.BenutzerId, abonnentId);
                            if (response == 0)
                            {
                                client.Write("+;Abonnent erfolgreich abonniert\n");
                            }
                            else if (response == 1)
                                client.Write("-;Nutzer kann sich nicht selbst abonnieren\n");
                            else
                                client.Write("-;Abonnent wurde bereits vom Nutzer abonniert\n");
                            break;
                        case "abonnentenAnzahl":
                            int nutzerId = Convert.ToInt32(parameter[1]);
                            anzahl = spf.ErmittelAbonnentenAnzahl(nutzerId);
                            client.Write($"+;{anzahl}\n");
                            break;
                        case "kommentar":
                            beitragId = Convert.ToInt32(parameter[1]);
                            text = GetMessage(parameter[2]);
                            int? oberKommentarId = null;
                            if (parameter.Length > 3)
                                oberKommentarId = Convert.ToInt32(parameter[3]);
                            spf.ErstelleKommentar(beitragId, this.nutzer.BenutzerId, text, oberKommentarId);
                            client.Write("+;Kommentar erfolgreich erstellt\n");
                            break;
                        case "ladeKommentare":
                            beitragId = Convert.ToInt32(parameter[1]);
                            List<Kommentar> kommentare = spf.LadeKommentare(beitragId);
                            msg = "";
                            msg = $"kommentare;{kommentare.Count}";
                            foreach (Kommentar k in kommentare)
                            {
                                string pictureString = "";
                                if (k.profil != null)
                                {
                                    byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", k.profil));
                                    pictureString = Convert.ToBase64String(picture);
                                }
                                else
                                {
                                    byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", "profile.jpg"));
                                    pictureString = Convert.ToBase64String(picture);
                                }
                                msg += $";{k.Id}|{ConvertMessage(k.Nachricht)}|{k.AutorId}|{k.Timestamp}|{ConvertMessage(k.autor)}|{pictureString}|";
                                if (k.OberKommentarId != null)
                                    msg += k.OberKommentarId;
                                else
                                    msg += "null";
                            }
                            client.Write(msg + "\n");
                            break;
                        case "loadProfile":
                            int abonnenten = spf.ErmittelAbonnentenAnzahl(this.nutzer.BenutzerId);
                            if (this.nutzer.ProfilBild == null)
                            {
                                byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", "profile.jpg"));
                                string pictureString = Convert.ToBase64String(picture);
                                msg = $"+;{ConvertMessage(this.nutzer.BenutzerName)};{ConvertMessage(this.nutzer.Email)};{this.nutzer.BenutzerId};{this.nutzer.ZuletztAktiv};{abonnenten};{pictureString}";
                            }
                            else
                            {
                                byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", this.nutzer.ProfilBild));
                                string pictureString = Convert.ToBase64String(picture);
                                msg = $"+;{ConvertMessage(this.nutzer.BenutzerName)};{ConvertMessage(this.nutzer.Email)};{this.nutzer.BenutzerId};{this.nutzer.ZuletztAktiv};{abonnenten};{pictureString}";
                            }
                            client.Write(msg + "\n");
                            break;
                        case "loadNutzer":
                            nutzerId = Convert.ToInt32(parameter[1]);
                            abonnenten = spf.ErmittelAbonnentenAnzahl(nutzerId);
                            Nutzer n = spf.SucheNutzer(nutzerId);
                            if (n.ProfilBild == null)
                            {
                                byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", "profile.jpg"));
                                string pictureString = Convert.ToBase64String(picture);
                                msg = $"+;{ConvertMessage(n.BenutzerName)};{n.BenutzerId};{n.ZuletztAktiv};{abonnenten};{pictureString}";
                            }
                            else
                            {
                                byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", n.ProfilBild));
                                string pictureString = Convert.ToBase64String(picture);
                                msg = $"+;{ConvertMessage(n.BenutzerName)};{n.BenutzerId};{n.ZuletztAktiv};{abonnenten};{pictureString}";
                            }
                            client.Write(msg + "\n");
                            break;
                        case "updateProfile":
                            name = GetMessage(parameter[1]);
                            email = GetMessage(parameter[2]);
                            if (parameter.Length > 3)
                            {
                                string file = parameter[3];
                                string base64 = parameter[4];
                                byte[] pictureBytes = Convert.FromBase64String(base64);
                                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file);
                                File.WriteAllBytes(Path.Combine(imgOrdner, "profile", fileName), pictureBytes);
                                this.nutzer.ProfilBild = fileName;
                            }
                            else
                                spf.AktualisiereProfil(this.nutzer.BenutzerId, name, email);
                            this.nutzer.Email = email;
                            this.nutzer.BenutzerName = name;
                            client.Write("+;Profil aktualisiert\n");
                            break;
                        case "addProfilePicture":
                            string filename = parameter[1];
                            string base64s = parameter[2];
                            byte[] pictureByte = Convert.FromBase64String(base64s);
                             Image img;
                            using (MemoryStream ms = new MemoryStream(pictureByte))
                            {
                                img = Image.FromStream(ms);
                            }
                            img = SocialMediaPlatform.CropToSquare(img);
                            img = SocialMediaPlatform.ResizeImage(img, 256);
                            string unique = Guid.NewGuid().ToString() + Path.GetExtension(filename);
                            if (this.nutzer.ProfilBild != null)
                                File.Delete(Path.Combine(imgOrdner, "profile", this.nutzer.ProfilBild));
                            img.Save(Path.Combine(imgOrdner, "profile", unique));
                            this.nutzer.ProfilBild = unique;
                            spf.AktualisiereProfilBild(this.nutzer.BenutzerId, unique);
                            client.Write("+;Profilbild hinzugefügt\n");
                            break;
                        case "updatePasswort":
                            string old = GetMessage(parameter[1]);
                            string newP = GetMessage(parameter[2]);
                            response = spf.ChangePassword(old, newP, this.nutzer.BenutzerId);
                            if (response == 0)
                            {
                                client.Write("+;Passwort erfolgreich aktualisiert\n");
                            }
                            else
                            {
                                client.Write("-;Falsches aktuelles Passwort eingegeben\n");
                            }
                            break;
                        case "sucheNutzer":
                            name = GetMessage(parameter[1]);
                            List<Nutzer> nutzerList = spf.SucheNutzer(name);
                            msg = $"+;{nutzerList.Count}";
                            foreach (Nutzer nu in nutzerList)
                            {
                                abonnenten = spf.ErmittelAbonnentenAnzahl(nu.BenutzerId);
                                if (nu.ProfilBild == null)
                                {
                                    byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", "profile.jpg"));
                                    string pictureString = Convert.ToBase64String(picture);
                                    msg += $";{ConvertMessage(nu.BenutzerName)}|{nu.BenutzerId}|{abonnenten}|{pictureString}";
                                }
                                else
                                {
                                    byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", nu.ProfilBild));
                                    string pictureString = Convert.ToBase64String(picture);
                                    msg += $";{ConvertMessage(nu.BenutzerName)}|{nu.BenutzerId}|{abonnenten}|{pictureString}";
                                }
                            }
                            client.Write(msg + "\n");
                            break;
                        case "chatErstellen":
                            int user2 = Convert.ToInt32(parameter[1]);
                            if (user2 == this.nutzer.BenutzerId)
                            {
                                client.Write("-;Nutzer kann nicht mit sich selbst ein Chat erstellen\n");
                                break;
                            }
                            int chatId = spf.ChatErstellen(this.nutzer.BenutzerId, user2);
                            client.Write($"+;{chatId}\n");
                            break;
                        case "chatListe":
                            List<Chat> chats = spf.LadeChats(this.nutzer.BenutzerId);
                            msg = $"+;{chats.Count}";
                            foreach (Chat c in chats)
                            {
                                if (c.LetzteNachricht != null)
                                    msg += $";{c.ChatId}|{ConvertMessage(c.BenutzerName)}|{ConvertMessage(c.LetzteNachricht)}|{c.LetzteZeit}";
                                else
                                    msg += $";{c.ChatId}|{ConvertMessage(c.BenutzerName)}||";
                                if (c.ProfilBild != null)
                                {
                                    byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", c.ProfilBild));
                                    string pictureString = Convert.ToBase64String(picture);
                                    msg += $"|{pictureString}";
                                }
                                else
                                {
                                    byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", "profile.jpg"));
                                    string pictureString = Convert.ToBase64String(picture);
                                    msg += $"|{pictureString}";
                                }
                            }
                            client.Write(msg + "\n");
                            break;
                        case "nachrichtSenden":
                            int chat = Convert.ToInt32(parameter[1]);
                            text = GetMessage(parameter[2]);
                            spf.SendeNachricht(chat, this.nutzer.BenutzerId, text);
                            client.Write("+;Nachricht gesendet\n");
                            break;
                        case "loadNachrichten":
                            chatId = Convert.ToInt32(parameter[1]);
                            offset = 0;
                            if (parameter[2] != null)
                                offset = Convert.ToInt32(parameter[2]);
                            List<Nachricht> nachrichten = spf.LadeNachricht(chatId, offset);
                            msg = $"+;{nachrichten.Count}";
                            foreach (Nachricht na in nachrichten)
                            {
                                msg += $";{na.Sender.BenutzerId}|{ConvertMessage(na.Sender.BenutzerName)}|{ConvertMessage(na.Text)}|{na.GesendetAm}|{na.NachrichtId}";
                                if (na.Sender.ProfilBild != null)
                                {
                                    byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", na.Sender.ProfilBild));
                                    string pictureString = Convert.ToBase64String(picture);
                                    msg += $"|{pictureString}";
                                }
                                else
                                {
                                    byte[] picture = File.ReadAllBytes(Path.Combine(imgOrdner, "profile", "profile.jpg"));
                                    string pictureString = Convert.ToBase64String(picture);
                                    msg += $"|{pictureString}";
                                }
                            }
                            client.Write(msg + "\n");
                            break;
                        case "empfehlung":
                            offset = 0;
                            if (parameter[1] != null)
                                offset = Convert.ToInt32(parameter[1]);
                            List<Beitrag> beitreageLiked = spf.HoleLikedBeitraege(this.nutzer);

                            string[] beliebt = TagRankingErmittlen(beitreageLiked);

                            List<Beitrag> relevanteBeitraege = spf.HoleRelevanteBeitraege(beliebt, offset);

                            GewichtungZuweisen(relevanteBeitraege, beliebt, spf.ErmittleAbonnierteNutzer(this.nutzer));

                            List<Beitrag> beitraegeSortiertNachGewichtungUnflipped = sortiereBeitraegeNachGewichtung(relevanteBeitraege, 0, relevanteBeitraege.Count - 1);
                            List<Beitrag> beitraegeSortiertNachGewichtung = new List<Beitrag>();
                            // Protokoll: +;anzahlBeitraege;id;titel;text;autor;anzahlLikes;timestamp;dateinamen1:bild1,dateinamen2:bild2,..,dateinamenN:bildn;...
                            msg = "";
                            for(int i = beitraegeSortiertNachGewichtungUnflipped.Count - 1; i > 0; i--) 
                            {
                                beitraegeSortiertNachGewichtung.Add(beitraegeSortiertNachGewichtungUnflipped[i]);
                            }
                            foreach (Beitrag b in beitraegeSortiertNachGewichtung)
                            {
                                foreach (Bild bild in spf.HoleBilder(b.Id))
                                {
                                    b.Hinzufuegen(bild);
                                }
                                List<string> bilderStrings = new List<string>();
                                foreach (Bild imgs in b.Bilder)
                                {
                                    string s = Convert.ToBase64String(File.ReadAllBytes(Path.Combine("img", "preview", imgs.Dateiname)));

                                    bilderStrings.Add($"{imgs.Dateiname}:{s}");
                                }
                                string pictues = string.Join(",", bilderStrings);
                                string textBeitrag = b.Text.text;
                                if (textBeitrag != null)
                                {
                                    textBeitrag = ConvertMessage(b.Text.text);
                                }
                                msg = $"+;{b.Id};{ConvertMessage(b.Titel)};{textBeitrag};{b.Autor.BenutzerId};{b.gebeAnzahlLikes()};{b.Geposted};{pictues};{b.Tag}\n";
                                client.Write(msg);
                            }
                            client.Write("+;fertig\n");
                            break;
                        case "beliebteste":
                            msg = "";
                            offset = 0;
                            if (parameter[1] != null)
                                offset = Convert.ToInt32(parameter[1]);
                            List<Beitrag> beitreageBeliebt = spf.HoleBeliebtesteBeitraege(offset);

                            // Protokoll: +;anzahlBeitraege;id;titel;text;autor;anzahlLikes;timestamp;ateinamen1:bild1,dateinamen2:bild2,..,dateinamenN:bildn;...
                            foreach (Beitrag b in beitreageBeliebt)
                            {
                                foreach (Bild bild in spf.HoleBilder(b.Id))
                                {
                                    b.Hinzufuegen(bild);
                                }
                                List<string> bilderStringList = new List<string>();
                                foreach (Bild imgs in b.Bilder)
                                {
                                    string s = Convert.ToBase64String(File.ReadAllBytes(Path.Combine("img", "preview", imgs.Dateiname)));
                                    bilderStringList.Add($"{imgs.Dateiname}:{s}");
                                }
                                string bilderString = string.Join(",", bilderStringList);
                                string textBeitrag = b.Text.text;
                                if (textBeitrag != null)
                                {
                                    textBeitrag = ConvertMessage(b.Text.text);
                                }
                                msg = $"+;{b.Id};{ConvertMessage(b.Titel)};{textBeitrag};{b.Autor.BenutzerId};{b.gebeAnzahlLikes()};{b.Geposted};{bilderString};{b.Tag}\n";
                                client.Write(msg);
                            }
                            client.Write("+;fertig\n");
                            break;
                        case "newPasswort":
                            msg = "";
                            string emailNutzer = GetMessage(parameter[1]);
                            string passwortNeu = new string(spf.GenerierePasswort());
                            bool ok = spf.passwortWechseln(passwortNeu, emailNutzer);
                            if(ok) 
                            {
                                msg = "Neues Passwort, bitte nach Anmeldung wieder wechseln!: " + passwortNeu+"\n";
                            }
                            else 
                            {
                                msg = "Email nicht gefunden!" + "\n";
                            }
                            client.Write(msg);
                            break;
                        case "nutzerBeitraege":
                            offset = 0;
                            if (parameter[1] != null)
                                offset = Convert.ToInt32(parameter[1]);
                            beitraege = spf.HoleNutzerBeitraege(this.nutzer.BenutzerId, offset);
                            foreach (Beitrag b in beitraege)
                            {
                                foreach (Bild bild in spf.HoleBilder(b.Id))
                                {
                                    b.Hinzufuegen(bild);
                                }
                                List<string> bilderStringList = new List<string>();
                                foreach (Bild imgs in b.Bilder)
                                {
                                    string s = Convert.ToBase64String(File.ReadAllBytes(Path.Combine("img", "preview", imgs.Dateiname)));
                                    bilderStringList.Add($"{imgs.Dateiname}:{s}");
                                }
                                string bilderString = string.Join(",", bilderStringList);
                                string textBeitrag = b.Text.text;
                                if (textBeitrag != null)
                                    textBeitrag = ConvertMessage(textBeitrag);
                                msg = $"+;{b.Id};{ConvertMessage(b.Titel)};{textBeitrag};{b.Autor.BenutzerId};{b.gebeAnzahlLikes()};{b.Geposted};{bilderString};{b.Tag}\n";
                                client.Write(msg);
                            }
                            client.Write("+;fertig\n");
                            break;
                        case "test":
                            client.Write("+\n");
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
        
        /// <summary>
        /// Konvertiert nachrichten, die sonderzeichen haben können in Bytes um und dann in Base64, damit sie die Protokoll Struktur nicht brechen
        /// </summary>
        private string ConvertMessage(string message)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
        }

        private string GetMessage(string message)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(message));
        }

        private string[] TagRankingErmittlen(List<Beitrag> beitraege)
        {
            string[] beliebteste = new string[3];

            int countTiere = 0;
            int countNews = 0;
            int countSonstiges = 0;
            int countMemes = 0;

            //Die Anzahl der Relevanten Tags aller geliketen Beiträgen werden gezählt.
            
            foreach(Beitrag b in beitraege) 
            {
                switch (b.Tag) 
                {
                    case "Tiere":
                        countTiere++;
                        break;
                    case "News":
                        countNews++;
                        break;
                    case "Sonstiges":
                        countSonstiges++;
                        break;
                    case "Memes":
                        countMemes++;
                        break;
                }
            }
            int[] counts = { countTiere, countNews, countSonstiges, countMemes };
            //Sortierung aller Count Werte

            for (int i = 0; i < counts.Length - 1; i++) 
            {
                for(int j = 0; j < counts.Length - 1; j++) 
                {
                    if (counts[j] < counts[j + 1]) 
                    {
                        int temp = counts[j];
                        counts[j] = counts[j + 1];
                        counts[j + 1] = temp;
                    }
                }
            }

            //Zuweisung der Tags nach ermittelter Reihenfolge (Ranking nach Array-Index)
            for(int i = 0; i < beliebteste.Length; i++) 
            {
                if (counts[i] == countTiere && counts[i] != 0)
                    beliebteste[i] = "Tiere";
                else if(counts[i] == countNews && counts[i] != 0)
                    beliebteste[i] = "News";
                else if (counts[i] == countSonstiges && counts[i] != 0)
                    beliebteste[i] = "Sonstiges";
                else if (counts[i] == countMemes && counts[i] != 0)
                    beliebteste[i] = "Memes";
            }

            return beliebteste;
        }
        public void GewichtungZuweisen(List<Beitrag> beitraege, string[] beliebt, List<Nutzer> nutzer)
        {
            foreach(Beitrag b in beitraege) 
            {
                int gewichtung = 0;
                gewichtung += b.gebeAnzahlLikes();

                if(b.Tag == beliebt[0])
                    { gewichtung += 50; }
                else if(b.Tag == beliebt[1])
                    { gewichtung += 25; }
                else { gewichtung += 10; }

                if(nutzer.Contains(b.Autor)) 
                {
                    gewichtung += 100;
                }

                b.setGewichtung(gewichtung);
            }
        }

        public List<Beitrag> sortiereBeitraegeNachGewichtung(List<Beitrag> beitraege, int left, int right)
        {
            //Quicksort Algorithmus für die Sortierung der Zugewiesenen Gewichtung der Beiträge
            int i = left;
            int x = right - 1;
            var pivot = beitraege[right];

            while (i < x)
            {
                if (beitraege[i].Gewichtung <= pivot.Gewichtung)
                {
                    i++;
                }
                if (beitraege[x].Gewichtung >= pivot.Gewichtung)
                {
                    x--;
                }
                if (beitraege[i].Gewichtung >= pivot.Gewichtung && beitraege[x].Gewichtung <= pivot.Gewichtung)
                {
                    var temp = beitraege[i];
                    beitraege[i] = beitraege[x];
                    beitraege[x] = temp;

                    i++;
                    x--;
                }
            }

            if (beitraege[i].Gewichtung > pivot.Gewichtung)
            {
                beitraege[right] = beitraege[i];
                beitraege[i] = pivot;
            }

            left += i + 1;

            if (left < right)
            {
                beitraege = sortiereBeitraegeNachGewichtung(beitraege, left, right);
            }
            else
            {
                left = 0;
                if (right == 0)
                {
                    return beitraege;
                }
                beitraege = sortiereBeitraegeNachGewichtung(beitraege, left, right - 1);
            }
            return beitraege;
        }
    }
}
