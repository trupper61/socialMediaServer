using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using MySql.Data;
using MySql.Data.MySqlClient;
using Mysqlx;
using System.Drawing;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using socialMedia;
namespace socialMediaServer
{
    public class SocialMediaPlatform //Größtenteils für die Handhabung zwischen Server und Datenbankanbindung zuständig.
    {
        private List<Nutzer> nutzer;
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=smpdb;User=root;Password=;";

        public SocialMediaPlatform()
        {
            nutzer = new List<Nutzer>();
        }
        /// <summary>
        /// Erstellt einen neuen Beitrag in der Datenbank, sowie alle beigefügten Bilderdateinamen
        /// </summary>
        public void ErstelleBeitrag(Nutzer nutzer, string titel, string text, List<string> bilder, string tag)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand beitrag = new MySqlCommand("INSERT INTO beitrag (text, titel, erstelltAm, autor, tag) VALUES (@text, @titel, @erstelltAm, @autor, @tag); SELECT LAST_INSERT_ID()", conn);
                beitrag.Parameters.AddWithValue("@text", text);
                beitrag.Parameters.AddWithValue("@titel", titel);
                beitrag.Parameters.AddWithValue("@erstelltAm", DateTime.Now);
                beitrag.Parameters.AddWithValue("@autor", nutzer.BenutzerId);
                beitrag.Parameters.AddWithValue("@tag", tag);

                int beitragId = Convert.ToInt32(beitrag.ExecuteScalar());

                foreach (string dateiNamen in bilder)
                {
                    MySqlCommand bild = new MySqlCommand("INSERT INTO bild (dateiname, beitragid) VALUES (@dateiname, @beitragid)", conn);
                    bild.Parameters.AddWithValue("@dateiname", dateiNamen);
                    bild.Parameters.AddWithValue("@beitragid", beitragId);
                    bild.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Registriert einen neuen Nutzer mit Benutzername, E-Mail und Passwort
        /// Dabei wird das Passwort mit einem Salt gehashed.
        /// </summary>
        public int Registrieren(string name, string passwort, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                MySqlCommand checkSelect = new MySqlCommand("SELECT COUNT(*) FROM nutzer WHERE benutzerName=@benutzerName OR email=@email", conn);
                checkSelect.Parameters.AddWithValue("@benutzerName", name);
                checkSelect.Parameters.AddWithValue("@email", email);
                if (Convert.ToInt32(checkSelect.ExecuteScalar()) > 0)
                {
                    conn.Close();
                    return -1;
                }

                string hashedPasswort = HashPasswort(passwort);

                MySqlCommand insert = new MySqlCommand("INSERT INTO nutzer (benutzerName, passwort, email, zuletztAktiv) VALUES (@benutzerName, @pass, @email, @aktiv)", conn);
                insert.Parameters.AddWithValue("@benutzerName", name);
                insert.Parameters.AddWithValue("@pass", hashedPasswort);
                insert.Parameters.AddWithValue("@email", email);
                insert.Parameters.AddWithValue("@aktiv", DateTime.Now);
                insert.ExecuteNonQuery();

                MySqlCommand getId = new MySqlCommand("SELECT LAST_INSERT_ID()", conn);
                int id = Convert.ToInt32(getId.ExecuteScalar());
                Nutzer neuerNutzer = new Nutzer(name, passwort, email, id);
                conn.Close();
                return 0;
            }
        }
        /// <summary>
        /// Überprüft ob die eingegeben Daten zu einem Nutzer passen, gibt den Nutzer zurück
        /// </summary>
        public Nutzer Anmelden(string name, string passwort)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand search = new MySqlCommand("SELECT nutzerId, passwort, email, profilBild FROM nutzer WHERE benutzerName=@benutzerName", conn);
                search.Parameters.AddWithValue("@benutzerName", name);
                Nutzer n = null;
                using (MySqlDataReader reader = search.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;
                    string savedPass = reader.GetString("passwort");
                    if (!VeriryPasswort(passwort, savedPass))
                    {
                        return null;
                    }
                    n = new Nutzer(name, "", reader.GetString("email"), reader.GetInt32("nutzerId"));
                    int ordinale = reader.GetOrdinal("profilBild");
                    if (!reader.IsDBNull(ordinale))
                        n.ProfilBild = reader.GetString(ordinale);
                    else
                        n.ProfilBild = null;
                    lock (nutzer)
                    {
                        nutzer.Add(n);
                    }
                    reader.Close();
                }
                conn.Close();
                return n;
            }
        }
        /// <summary>
        /// Generiert ein 12 Zeichen langes Passwort mit einem Großbuchstaben, einer Zahl und einem Sonderzeichen.
        /// </summary>
        public char[] GenerierePasswort()
        {
            Random rand = new Random();
            char[] passwort = new char[12];
            for (int i = 0; i < passwort.Length; i++)
            {
                passwort[i] = (char) (rand.Next(26) + 'a');
            }
            int z1, z2, z3 = 0;
            z1 = rand.Next(12);
            do
            {
                z2 = rand.Next(12);
            } while (z2 == z1);
            do
            {
                z3 = rand.Next(12);
            } while (z3 == z1 || z3 == z2);
            passwort[z1] = (char)(rand.Next(26) + 'A');
            passwort[z2] = (char)(rand.Next(10) + '0');
            passwort[z3] = (char)(rand.Next(4) + '#');
            return passwort;
        }

        /// <summary>
        /// Updated das Passwort für den Benutzer über die entsprechende E-Mail-Adresse
        /// </summary>
        public bool passwortWechseln(string pass, string email) 
        {
            string hashedPass = HashPasswort(pass);

            using (MySqlConnection conn = new MySqlConnection(connectionString)) 
            {
                try
                {
                    conn.Open();
                    MySqlCommand passwortWechseln = new MySqlCommand(@"
                    UPDATE nutzer 
                    SET passwort = @pass
                    WHERE email = @email", conn);
                    passwortWechseln.Parameters.AddWithValue("@pass", hashedPass);
                    passwortWechseln.Parameters.AddWithValue("@email", email);
                    int erfolg = passwortWechseln.ExecuteNonQuery();
                    if(erfolg == 0) 
                    {
                        conn.Close();
                        return false;
                    }
                    else 
                    {
                        conn.Close();
                        return true;
                    }
                }
                catch (Exception e) 
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Entnimmt 10 neuere Beiträge als der User, fügt ältere Beiträge hinzu, wenn es noch Lücken gibt
        /// </summary>
        public List<Beitrag> ErmittleNeueBeitraege(Nutzer n, int offset = 0)
        {
            List<Beitrag> beitraege = new List<Beitrag>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand neusteBeitraege = new MySqlCommand(@"
                SELECT b.beitragid, b.text, b.titel, b.erstelltAm, b.autor, u.benutzerName, COUNT(l.beitragId) AS likes, b.tag
                FROM beitrag b
                JOIN nutzer u ON b.autor = u.nutzerId
                LEFT JOIN likes l ON b.beitragid = l.beitragId
                WHERE b.erstelltAm > @zuletztAktiv
                GROUP BY b.beitragid
                ORDER BY b.erstelltAm DESC
                LIMIT 10 OFFSET @offset", conn);
                //neusteBeitraege.Parameters.AddWithValue("@nutzerId", n);
                neusteBeitraege.Parameters.AddWithValue("@zuletztAktiv", n.ZuletztAktiv);
                neusteBeitraege.Parameters.AddWithValue("@offset", offset);
                using (MySqlDataReader reader = neusteBeitraege.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        beitraege.Add(LeseBeitrag(reader));
                    }
                    reader.Close();
                }
                if (beitraege.Count < 10)
                {
                    int remaining = 10 - beitraege.Count;
                    MySqlCommand alteBeitraege = new MySqlCommand(@"
                    SELECT b.beitragid, b.titel, b.text, b.erstelltAm, b.autor, u.benutzerName, COUNT(l.beitragId) AS likes, b.tag
                    FROM beitrag b
                    JOIN nutzer u ON b.autor = u.nutzerId
                    LEFT JOIN likes l ON b.beitragid = l.beitragId
                    WHERE b.erstelltAm <= @zuletztAktiv
                    GROUP BY b.beitragid
                    ORDER BY b.erstelltAm DESC
                    LIMIT @max OFFSET @offset", conn);
                    alteBeitraege.Parameters.AddWithValue("@zuletztAktiv", n.ZuletztAktiv);
                    alteBeitraege.Parameters.AddWithValue("@offset", offset);
                    alteBeitraege.Parameters.AddWithValue("@max", remaining);
                    using (MySqlDataReader reader = alteBeitraege.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            beitraege.Add(LeseBeitrag(reader));
                        }
                        reader.Close();
                    }
                }
                conn.Close();
                return beitraege;
            }
        }

        /// <summary>
        /// Holt 10 neue Beiträgen von Nutzer die der Nutzer abonniert hat.
        /// </summary>
        public List<Beitrag> BeitraegeVonAbosHolen(Nutzer n, int offset = 0) 
        {
            List<Beitrag> beitraege = new List<Beitrag>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand neusteBeitraege = new MySqlCommand(@"
                SELECT b.beitragid, b.text, b.titel, b.erstelltAm, b.autor, u.benutzerName, COUNT(l.beitragId) AS likes, b.tag
                FROM beitrag b
                JOIN nutzer u ON b.autor = u.nutzerId
                LEFT JOIN likes l ON b.beitragid = l.beitragId
                WHERE b.erstelltAm > @zuletztAktiv AND b.autor IN (SELECT abonnentId FROM abonnement WHERE abonnierteNutzerId = (SELECT nutzerId FROM nutzer WHERE benutzername = @benutzername))
                GROUP BY b.beitragid
                ORDER BY b.erstelltAm DESC
                LIMIT 10 OFFSET @offset", conn);
                neusteBeitraege.Parameters.AddWithValue("@benutzername", n.BenutzerName);
                neusteBeitraege.Parameters.AddWithValue("@zuletztAktiv", n.ZuletztAktiv);
                neusteBeitraege.Parameters.AddWithValue("@offset", offset);
                using (MySqlDataReader reader = neusteBeitraege.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        beitraege.Add(LeseBeitrag(reader));
                    }
                    reader.Close();
                }
                if (beitraege.Count < 10)
                {
                    int remaining = 10 - beitraege.Count;
                    MySqlCommand alteBeitraege = new MySqlCommand(@"
                    SELECT b.beitragid, b.text, b.titel, b.erstelltAm, b.autor, u.benutzerName, COUNT(l.beitragId) AS likes, b.tag
                    FROM beitrag b
                    JOIN nutzer u ON b.autor = u.nutzerId
                    LEFT JOIN likes l ON b.beitragid = l.beitragId
                    WHERE b.erstelltAm <= @zuletztAktiv AND b.autor IN (SELECT abonnentId FROM abonnement WHERE abonnierteNutzerId = (SELECT nutzerId FROM nutzer WHERE benutzername = @benutzername))
                    GROUP BY b.beitragid
                    ORDER BY b.erstelltAm DESC
                    LIMIT 10 OFFSET @offset", conn);
                    alteBeitraege.Parameters.AddWithValue("@benutzername", n.BenutzerName);
                    alteBeitraege.Parameters.AddWithValue("@zuletztAktiv", n.ZuletztAktiv);
                    alteBeitraege.Parameters.AddWithValue("@offset", offset);
                    alteBeitraege.Parameters.AddWithValue("@max", remaining);
                    using (MySqlDataReader reader = alteBeitraege.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            beitraege.Add(LeseBeitrag(reader));
                        }
                        reader.Close();
                    }
                }

                conn.Close();
                return beitraege;
            }
        }
        /// <summary>
        /// Holt 10 Beiträge mit den meisten Likes
        /// </summary>
        public List<Beitrag> HoleBeliebtesteBeitraege(int offset) 
        {
            List<Beitrag> beitraege = new List<Beitrag>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand likedBeitraege = new MySqlCommand(@"
                        SELECT b.beitragid, b.text, b.titel, b.erstelltAm, b.autor, u.benutzerName, COUNT(l.beitragId) AS likes, b.tag
                        FROM beitrag b
                        JOIN nutzer u ON b.autor = u.nutzerId
                        LEFT JOIN likes l ON b.beitragid = l.beitragId
                        GROUP BY b.beitragid
                        ORDER BY likes DESC
                        LIMIT 10 OFFSET @offset", conn);
                likedBeitraege.Parameters.AddWithValue("@offset", offset);
                using (MySqlDataReader reader = likedBeitraege.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        beitraege.Add(LeseBeitrag(reader));
                    }
                    reader.Close();
                }
            }
            return beitraege;
        }
        /// <summary>
        /// Lädt Beiträge die der Nutzer geliked hat.
        /// Dies wird genutzt für die Empfehlung
        /// </summary>
        public List<Beitrag> HoleLikedBeitraege(Nutzer n)
        {
            List<Beitrag> beitraege = new List<Beitrag>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand likedBeitraege = new MySqlCommand(@"
                SELECT b.beitragid, b.text, b.titel, b.erstelltAm, b.autor, u.benutzerName, COUNT(l.beitragId) AS likes, b.tag
                FROM beitrag b
                JOIN nutzer u ON b.autor = u.nutzerId
                LEFT JOIN likes l ON b.beitragid = l.beitragId
                WHERE l.nutzerId = @nutz
                GROUP BY b.beitragid
                ORDER BY b.erstelltAm DESC", conn);
                likedBeitraege.Parameters.AddWithValue("@nutz", n.BenutzerId);
                using (MySqlDataReader reader = likedBeitraege.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        beitraege.Add(LeseBeitrag(reader));
                    }
                    reader.Close();
                }
                return beitraege;
            }
        }

        public List<Beitrag> HoleRelevanteBeitraege(string[] ranking, int offset = 0) 
        {
            List<Beitrag> beitraege = new List<Beitrag>();
            foreach (string rank in ranking) 
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand likedBeitraege = new MySqlCommand(@"
                        SELECT b.beitragid, b.text, b.titel, b.erstelltAm, b.autor, u.benutzerName, COUNT(l.beitragId) AS likes, b.tag
                        FROM beitrag b
                        JOIN nutzer u ON b.autor = u.nutzerId
                        LEFT JOIN likes l ON b.beitragid = l.beitragId
                        WHERE b.tag = @tag
                        GROUP BY b.beitragid
                        ORDER BY b.erstelltAm DESC
                        LIMIT 10 OFFSET @offset", conn);
                    likedBeitraege.Parameters.AddWithValue("@tag", rank);
                    likedBeitraege.Parameters.AddWithValue("@offset", offset);
                    using (MySqlDataReader reader = likedBeitraege.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            beitraege.Add(LeseBeitrag(reader));
                        }
                        reader.Close();
                    }
                }
            }
            return beitraege;
        }
        /// <summary>
        /// Lädt 10 Beiträge die der Nutzer selbst erstellt hat
        /// </summary>
        public List<Beitrag> HoleNutzerBeitraege(int nutzerId, int offset = 0)
        {
            List<Beitrag> beitraege = new List<Beitrag>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand get = new MySqlCommand(@"
                    SELECT b.beitragid, b.text, b.titel, b.erstelltAm, b.autor, b.tag, u.benutzerName, Count(l.beitragId) AS likes
                    FROM beitrag b
                    JOIN nutzer u ON b.autor = u.nutzerId
                    LEFT JOIN likes l ON b.beitragid = l.beitragId
                    WHERE b.autor = @nutzer
                    GROUP BY b.beitragid
                    ORDER BY b.erstelltAm DESC
                    LIMIT 10 OFFSET @offset", conn);
                get.Parameters.AddWithValue("@nutzer", nutzerId);
                get.Parameters.AddWithValue("@offset", offset);
                using (MySqlDataReader reader =  get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Beitrag b = LeseBeitrag(reader);

                        beitraege.Add(b);
                    }
                }
            }
            return beitraege;
        }
        /// <summary>
        /// Helfer Methode, die die Werte überdem Reader auslist und einen Beitrag zurückgibt
        /// </summary>
        private Beitrag LeseBeitrag(MySqlDataReader reader)
        {
            int beitragId = reader.GetInt32("beitragid");
            string titel = reader.GetString("titel");
            string text;
            string tag = reader.GetString("tag");
            if (reader.IsDBNull(reader.GetOrdinal("text")))
                text = null;
            else
                text = reader.GetString("text");
            DateTime erstelltAm = reader.GetDateTime("erstelltAm");
            int autorId = reader.GetInt32("autor");
            
            string autorName = reader.GetString("benutzerName");

            Nutzer autor = new Nutzer(autorName, "", "", autorId);
            Beitrag b = new Beitrag(autor, titel, new List<Bild>(), tag, text);
            b.Id = beitragId;
            b.setGeposted(erstelltAm);
            b.setAnzahlLikes(reader.GetInt32("likes"));
            if (text != null)
                b.ErstelleText(text);
            return b;
        }

        /// <summary>
        /// Holt alle Bilddateinamen von einem Beitrag
        /// </summary>
        public List<Bild> HoleBilder(int beitragId)
        {
            List<Bild> bilder = new List<Bild>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand get = new MySqlCommand("SELECT dateiname FROM bild WHERE beitragid = @beitragid", conn);
                get.Parameters.AddWithValue("@beitragid", beitragId);
                using (MySqlDataReader reader = get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bilder.Add(new Bild(reader.GetString("dateiname")));
                    }
                    return bilder;
                }
            }
        }

        /// <summary>
        /// Ändert das Passwort vom Nutzer wenn das alte Passwort korrekt eingegeben wurde
        /// </summary>
        public int ChangePassword(string oldPassword, string newPassword, int nutzerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand get = new MySqlCommand("SELECT passwort FROM nutzer WHERE nutzerId = @id", conn);
                get.Parameters.AddWithValue("@id", nutzerId);
                string saved = "";
                using (MySqlDataReader reader = get.ExecuteReader())
                {
                    if (reader.Read())
                        saved = reader.GetString("passwort");
                    reader.Close();
                }
                if (!VeriryPasswort(oldPassword, saved))
                    return -1;
                string hashed = HashPasswort(newPassword);
                MySqlCommand update = new MySqlCommand("UPDATE nutzer SET passwort = @p WHERE nutzerId = @id", conn);
                update.Parameters.AddWithValue("@p", hashed);
                update.Parameters.AddWithValue("@id", nutzerId);
                update.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
        }
        public List<Nutzer> ErmittleAbonnierteNutzer(Nutzer n)
        {
            List<Nutzer> result = new List<Nutzer>();
            foreach (Nutzer a in n.AbonnierteNutzer)
            {
                result.Add(a);
            }
            return result;
        }
        /// <summary>
        /// Sucht einen Nutzer nach deren BenutzerId und gibt ihn als Objekt zurück
        /// </summary>
        public Nutzer SucheNutzer(int nutzerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand get = new MySqlCommand(@"
                SELECT benutzerName, email, zuletztAktiv, profilBild
                FROM nutzer
                WHERE nutzerId = @nutzerId", conn);
                get.Parameters.AddWithValue("@nutzerId", nutzerId);
                Nutzer n = null;
                using (MySqlDataReader reader = get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString("benutzerName");
                        string email = reader.GetString("email");
                        DateTime time = reader.GetDateTime("zuletztAktiv");
                        int ordinale = reader.GetOrdinal("profilBild");
                        n = new Nutzer(name, "", email, nutzerId);
                        if (!reader.IsDBNull(ordinale))
                            n.ProfilBild = reader.GetString("profilBild");
                        n.ZuletztAktiv = time;
                    }
                }
                conn.Close();
                return n;
            }
        }
        /// <summary>
        /// Sucht für Nutzer die mit dem Suchbegriff übereinstimmen
        /// </summary>
        public List<Nutzer> SucheNutzer(string suchBegriff)
        {
            List<Nutzer> nutzer = new List<Nutzer>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand get = new MySqlCommand(@"
                SELECT nutzerId, benutzerName, profilBild
                FROM nutzer
                WHERE benutzerName LIKE @name LIMIT 20", conn);
                get.Parameters.AddWithValue("@name", "%" + suchBegriff + "%");

                using (MySqlDataReader reader = get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("nutzerId");
                        string name = reader.GetString("benutzerName");
                        int ordinale = reader.GetOrdinal("profilBild");
                        Nutzer n = new Nutzer(name, "", "", id);
                        if (!reader.IsDBNull(ordinale))
                            n.ProfilBild = reader.GetString("profilBild");
                        nutzer.Add(n);
                    }
                }
                conn.Close();
                return nutzer;
            }
        }
        /// <summary>
        /// Updated die Benutzerinformationen in der Datenbank
        /// </summary>
        public void AktualisiereProfil(int nutzerId, string name, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand update = new MySqlCommand(@"
                UPDATE nutzer
                SET benutzerName = @name, email = @email
                WHERE nutzerId = @id", conn);
                update.Parameters.AddWithValue("@name", name);
                update.Parameters.AddWithValue("@email", email);
                update.Parameters.AddWithValue("@id", nutzerId);
                update.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Updated das Profilbild vom Nutzer in der Datenbank
        /// </summary>
        public void AktualisiereProfilBild(int nutzerId, string filename)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand update = new MySqlCommand(@"
                UPDATE nutzer
                SET profilBild = @p
                WHERE nutzerId = @id", conn);
                update.Parameters.AddWithValue("@id", nutzerId);
                update.Parameters.AddWithValue("@p", filename);
                update.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Holt die Bilderdateinamen von einem Beitrag
        /// </summary>
        public List<string> HoleOriginalBilder(int beitragId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                List<string> bilder = new List<string>();
                MySqlCommand cmd = new MySqlCommand("SELECT dateiname FROM bild WHERE beitragid = @b", conn);
                cmd.Parameters.AddWithValue("@b", beitragId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString("dateiname");
                        bilder.Add(name);
                    }
                    reader.Close();
                }
                conn.Close();
                return bilder;
            }
        }

        public int UpdateZuletztAktiv(int nutzerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE nutzer SET zuletztAktiv = NOW() WHERE nutzerId = @n", conn);
                cmd.Parameters.AddWithValue("@n", nutzerId);
                cmd.ExecuteNonQuery();
            }
            return 0;
        }
        /// <summary>
        /// Überprüft ob der Benutzer bereits den Abonnent abonniert hat,
        /// andernfalls wird der Nutzer abonniert
        /// </summary>
        public int Abonnieren(int nutzerId, int abonnentId)
        {
            if (nutzerId == abonnentId)
                return 1;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand check = new MySqlCommand(@"
                SELECT COUNT(*)
                FROM abonnement
                WHERE abonnentId = @abonnentId
                AND abonnierteNutzerId = @nutzerId", conn);
                check.Parameters.AddWithValue("@abonnentId", abonnentId);
                check.Parameters.AddWithValue("@nutzerId", nutzerId);
                int verify = Convert.ToInt32(check.ExecuteScalar());
                if (verify != 0)
                {
                    Console.WriteLine("Abonnent wurde bereits vom Nutzer abonniert");
                    return 2;
                }
                MySqlCommand insert = new MySqlCommand(@"
                INSERT INTO abonnement (abonnentId, abonnierteNutzerId)
                VALUES (@abonnentId, @nutzerId)", conn);
                insert.Parameters.AddWithValue("@abonnentId", abonnentId);
                insert.Parameters.AddWithValue("@nutzerId", nutzerId);
                insert.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
        }
        /// <summary>
        /// Ermittelt die Abonnentenanzahl von einem Nutzer anhand der Id
        /// </summary>
        public int ErmittelAbonnentenAnzahl(int nutzerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(@"
                SELECT COUNT(abonnierteNutzerId)
                FROM abonnement
                WHERE abonnentId = @nutzerId", conn);
                command.Parameters.AddWithValue("@nutzerId", nutzerId);
                int abonnenten = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();
                return abonnenten;
            }
        }
        /// <summary>
        /// Überprüft zunächst, ob der Nutzer bereits diese Beitrag geliked hat.
        /// Danach wird überprüft ob der Beitrag den Nutzer gehört.
        /// Danach wird der Beitrag geliked.
        /// </summary>
        public int Like(int beitragId, int nutzerId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand check = new MySqlCommand(@"
                SELECT beitragid
                FROM beitrag
                WHERE autor = @nutzerId
                AND beitragid = @beitragId", conn);
                check.Parameters.AddWithValue("@nutzerId", nutzerId);
                check.Parameters.AddWithValue("@beitragId", beitragId);
                int verify = Convert.ToInt32(check.ExecuteScalar());
                if (verify != 0)
                    return -1;
                MySqlCommand check2 = new MySqlCommand("SELECT nutzerId FROM likes WHERE nutzerId = @n AND beitragId = @b", conn);
                check2.Parameters.AddWithValue("@n", nutzerId);
                check2.Parameters.AddWithValue("@b", beitragId);
                verify = Convert.ToInt32(check2.ExecuteScalar());
                if (verify != 0)
                    return -2;
                MySqlCommand like = new MySqlCommand(@"
                INSERT INTO likes (beitragId, nutzerId)
                VALUES (@bId, @nId)", conn);
                like.Parameters.AddWithValue("@bId", beitragId);
                like.Parameters.AddWithValue("@nId", nutzerId);
                like.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
        }
        /// <summary>
        /// Speichert Kommentarwerte in der Datenbank
        /// </summary>
        public int ErstelleKommentar(int beitragsId, int nutzerId, string text, int? oberKommentar)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand insert = new MySqlCommand(@"
                INSERT INTO kommentar (nachricht, beitragId, autor, oberKommentarId)
                VALUES (@text, @bId, @autorId, @obKommentarId)", conn);
                insert.Parameters.AddWithValue("@text", text);
                insert.Parameters.AddWithValue("@bId", beitragsId);
                insert.Parameters.AddWithValue("autorId", nutzerId);
                if (oberKommentar != null)
                    insert.Parameters.AddWithValue("@obKommentarId", oberKommentar);
                else
                    insert.Parameters.AddWithValue("@obKommentarId", DBNull.Value);
                insert.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
        }
        /// <summary>
        /// Lädt 10 Kommentare von einem Beitrag
        /// </summary>
        public List<Kommentar> LadeKommentare(int beitragId)
        {
            List<Kommentar> comments = new List<Kommentar>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand select = new MySqlCommand(@"
                SELECT kommentarid, nachricht, timestamp, autor, oberKommentarId, benutzerName, profilBild
                From kommentar, nutzer
                WHERE beitragId = @bId AND nutzerId = autor
                ORDER BY timestamp ASC", conn);
                select.Parameters.AddWithValue("@bId", beitragId);
                using (MySqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int kId = reader.GetInt32("kommentarid");
                        string nachricht = reader.GetString("nachricht");
                        DateTime timestamp = reader.GetDateTime("timestamp");
                        int autor = reader.GetInt32("autor");
                        var ordinal = reader.GetOrdinal("oberKommentarId");
                        string benutzername = reader.GetString("benutzerName");
                        int index = reader.GetOrdinal("profilBild");
                        string profil;
                        if (!reader.IsDBNull(index))
                        {
                            profil = reader.GetString("profilBild");
                        }
                        else
                        {
                            profil = null;
                        }
                        int? oKid = null;
                        if (!reader.IsDBNull(ordinal))
                            oKid = reader.GetInt32(ordinal);
                        Kommentar k = new Kommentar(nachricht, timestamp, autor);
                        k.autor = benutzername;
                        k.profil = profil;
                        comments.Add(k);
                    }
                }
                conn.Close();
                return comments;
            }
        }
        /// <summary>
        /// Überprüft ob bereits ein Chat zwischen zwei Nutzern existiert.
        /// Wenn ein Chat existiert wird diese chatId zurückgegeben. 
        /// Wenn nicht dann wird 0 zurückgegeben
        /// </summary>
        public int ChatDoesNotExist(int nutzer1, int nutzer2)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand check = new MySqlCommand(@"
                SELECT chatId
                FROM chatTeilnehmer
                WHERE nutzerId IN (@n1, @n2)
                GROUP BY chatId
                HAVING COUNT(DISTINCT nutzerId) = 2
                AND COUNT(*) = 2
                LIMIT 1", conn);
                check.Parameters.AddWithValue("@n1", nutzer1);
                check.Parameters.AddWithValue("@n2", nutzer2);
                var result = check.ExecuteScalar();
                if (result != null)
                    return Convert.ToInt32(result);
                return 0;
            }
        }
        /// <summary>
        /// Erstellt ein Chat wenn noch kein Chat existiert.
        /// Beim erstellen eines Chats, werden die zwei Nutzer noch eine Referenz zum Chat hinterlassen
        /// </summary>
        public int ChatErstellen(int nutzer1, int nutzer2)
        {
            int value = ChatDoesNotExist(nutzer1, nutzer2);
            if (value != 0)
            {
                return value;
            }
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //MySqlCommand check = new MySqlCommand("")

                MySqlCommand chat = new MySqlCommand("INSERT INTO chat(erstelltAm) VALUES (NOW())", conn);
                chat.ExecuteNonQuery();
                int chatId = Convert.ToInt32(chat.LastInsertedId);

                chat = new MySqlCommand("INSERT INTO chatTeilnehmer(chatId, nutzerId) VALUES (@c, @n)", conn);
                chat.Parameters.AddWithValue("@c", chatId);
                chat.Parameters.AddWithValue("@n", nutzer1);
                chat.ExecuteNonQuery();
                chat = new MySqlCommand("INSERT INTO chatTeilnehmer(chatId, nutzerId) VALUES (@c, @n)", conn);
                chat.Parameters.AddWithValue("@c", chatId);
                chat.Parameters.AddWithValue("@n", nutzer2);
                chat.ExecuteNonQuery();
                conn.Close();
                return chatId;
            }
        }
        /// <summary>
        /// Speichert die Nachricht in die Datenbank zum jeweilligen Chat
        /// </summary>
        public void SendeNachricht(int chatId, int sender, string text)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand insert = new MySqlCommand(@"
                INSERT INTO chatnachricht(chatId, senderId, text, gesendetAm)
                VALUES (@c, @s, @t, NOW())", conn);
                insert.Parameters.AddWithValue("@c", chatId);
                insert.Parameters.AddWithValue("@s", sender);
                insert.Parameters.AddWithValue("@t", text);
                insert.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Lädt alle Chats, wo der Nutzer eine Referenz hat.
        /// Für jedem Chat wird der andere Teilnehmer die Id, das ProfilBild und sein Benutzername mitentnommen
        /// Zudem wird die im chat älteste Nachricht ausgegeben.
        /// </summary>
        public List<Chat> LadeChats(int nutzerId)
        {
            List<Chat> chats = new List<Chat>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand get = new MySqlCommand(@"
                SELECT c.chatId, n.benutzerName, n.profilBild, na.text, na.gesendetAm
                FROM chatTeilnehmer t
                JOIN chat c ON t.chatId = c.chatId
                JOIN chatTeilnehmer t2 ON c.chatId = t2.chatId AND t2.nutzerId != @n
                JOIN nutzer n ON t2.nutzerId = n.nutzerId
                LEFT JOIN chatnachricht na ON na.chatId = c.chatId AND na.gesendetAm = (
                    SELECT MAX(gesendetAm)
                    FROM chatnachricht
                    WHERE chatId = c.chatId
                )
                WHERE t.nutzerId = @n
                ORDER BY na.gesendetAm DESC", conn);
                get.Parameters.AddWithValue("@n", nutzerId);
                using (MySqlDataReader reader = get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("chatId");
                        string name = reader.GetString("benutzerName");
                        Chat c = new Chat(id);
                        int ordinal = reader.GetOrdinal("text");
                        string text = null;
                        if (!reader.IsDBNull(ordinal))
                            text = reader.GetString("text");
                        ordinal = reader.GetOrdinal("gesendetAm");
                        DateTime? gesendetAm = null;
                        if (!reader.IsDBNull(ordinal))
                            gesendetAm = reader.GetDateTime("gesendetAm");
                        ordinal = reader.GetOrdinal("profilBild");
                        if (!reader.IsDBNull(ordinal))
                        {
                            string profil = reader.GetString("profilBild");
                            c.SetData(name, profil, text, gesendetAm);
                        }
                        else
                        {
                            c.SetData(name, null, text, gesendetAm);
                        }

                        chats.Add(c);
                    }
                    reader.Close();
                }
                conn.Close();
                return chats;
            }
        }
        /// <summary>
        /// Lädt die letzten 10 neusten Nachrichten aus einem Chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<Nachricht> LadeNachricht(int chatId, int offset)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                List<Nachricht> nachrichten = new List<Nachricht>();
                MySqlCommand get = new MySqlCommand(@"
                SELECT nachrichtId, n.text, n.senderId, n.gesendetAm, u.benutzerName, u.profilBild
                FROM chatnachricht n
                JOIN nutzer u ON n.senderId = u.nutzerId
                WHERE n.chatId = @c
                ORDER BY n.gesendetAm DESC
                LIMIT 10 OFFSET @offset", conn);
                get.Parameters.AddWithValue("@c", chatId);
                get.Parameters.AddWithValue("@offset", offset);
                using (MySqlDataReader reader = get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int nachrichtId = reader.GetInt32("nachrichtId");
                        string text = reader.GetString("text");
                        int id = reader.GetInt32("senderId");
                        DateTime gesendetAm = reader.GetDateTime("gesendetAm");
                        string name = reader.GetString("benutzerName");
                        Nutzer n = new Nutzer(name, "", "", id);
                        int ordinal = reader.GetOrdinal("profilBild");
                        if (!reader.IsDBNull(ordinal))
                            n.ProfilBild = reader.GetString("profilBild");
                        nachrichten.Add(new Nachricht(id, n, text, gesendetAm, nachrichtId));
                    }
                    reader.Close();
                }
                conn.Close();
                return nachrichten;
            }
        }
        /// <summary>
        /// Schneidet ein Bild aus, dass es ein Quadrat wird.
        /// Es wird den mittleren Bereich des Bildes verwendet.
        /// </summary>
        public static Image CropToSquare(Image img)
        {
            int size = Math.Min(img.Width, img.Height);  // kleinere Seite auswählen
            int x = (img.Width - size) / 2;
            int y = (img.Height - size) / 2;

            Rectangle cropArea = new Rectangle(x, y, size, size); // Bereich der ausgeschnitten werden soll

            Bitmap bmp = new Bitmap(size, size); // Neue Bitmap mit der Zielgröße
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(img, new Rectangle(0, 0, size, size), cropArea, GraphicsUnit.Pixel);
            }
            return bmp;
        }
        /// <summary>
        /// Skaliert ein Bild auf eine feste Größe
        /// </summary>
        public static Image ResizeImage(Image img, int size = 512)
        {
            Bitmap resized = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; // Hochwertigere Interpolation für bessere Bildqualität
                g.DrawImage(img, 0, 0, size, size);
            }
            return resized;
        }
        /// <summary>
        /// Überprüft ob ein eingegebenes Passwort mit dem gespeicherten Hash übereinstimmt.
        /// </summary>
        private bool VeriryPasswort(string enteredPass, string savedPass)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPass);
            byte[] salt = new byte[16]; // Ersten 16 Bytes enthalten den Salt
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

            byte[] passwortBytes = Encoding.UTF8.GetBytes(enteredPass);
            byte[] saltedPasswort = new byte[passwortBytes.Length + salt.Length];

            Buffer.BlockCopy(passwortBytes, 0, saltedPasswort, 0, passwortBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPasswort, passwortBytes.Length, salt.Length);

            using (SHA256Managed sha256 = new SHA256Managed()) // Hash des eingegebenen Passworts berechnen
            {
                byte[] neuerHash = sha256.ComputeHash(saltedPasswort); 
                for (int i = 0; i < neuerHash.Length; i++)
                {
                    if (hashBytes[i + 16] != neuerHash[i]) // Für jedes byte überprüfen, ob es gleicht ist
                    {
                        return false;
                    } 
                }
            }
            return true;
        }
        /// <summary>
        /// Hashed ein eingegebenes Passwort
        /// Es wirt ein zufälliges 16 byte großer Salt generiert und mit gespeichert
        /// </summary>
        private string HashPasswort(string passwort)
        {
            byte[] salt = GenerateSalt();
            using (SHA256Managed sHA256 = new SHA256Managed())
            {
                byte[] passwortBytes = Encoding.UTF8.GetBytes(passwort);
                byte[] saltedPasswort = new byte[passwortBytes.Length + salt.Length];

                Buffer.BlockCopy(passwortBytes, 0, saltedPasswort, 0, passwortBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPasswort, passwortBytes.Length, salt.Length);

                byte[] hashedBytes = sHA256.ComputeHash(saltedPasswort);

                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }
        /// <summary>
        /// Erstellt ein zufälliges Salt für das Passwort
        /// </summary>
        private byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}
