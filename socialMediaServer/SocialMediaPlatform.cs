using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace socialMediaServer
{
    public class SocialMediaPlatform
    {
        private List<Nutzer> nutzer;
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=smpdb;User=root;Password=;";

        public SocialMediaPlatform()
        {
            nutzer = new List<Nutzer>();
        }

        public void ErstelleBeitrag(Nutzer nutzer, string titel, string text, List<string> bilder)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand beitrag = new MySqlCommand("INSERT INTO beitrag (text, titel, erstelltAm, autor) VALUES (@text, @titel, @erstelltAm, @autor); SELECT LAST_INSERT_ID()", conn);
            beitrag.Parameters.AddWithValue("@text", text);
            beitrag.Parameters.AddWithValue("@titel", titel);
            beitrag.Parameters.AddWithValue("@erstelltAm", DateTime.Now);
            beitrag.Parameters.AddWithValue("@autor", nutzer.BenutzerId);

            int beitragId = Convert.ToInt32(beitrag.ExecuteScalar());

            foreach (string dateiNamen in bilder)
            {
                MySqlCommand bild = new MySqlCommand("INSERT INTO bild (dateiname, beitragid) VALUES (@dateiname, @beitragid)", conn);
                bild.Parameters.AddWithValue("@dateiname", dateiNamen);
                bild.Parameters.AddWithValue("@beitragid", beitragId);
                bild.ExecuteNonQuery();
            }
            
            conn.Close();
        }
        public int Registrieren(string name, string passwort, string email)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand checkSelect = new MySqlCommand("SELECT COUNT(*) FROM nutzer WHERE benutzerName=@benutzerName OR email=@email", conn);
            checkSelect.Parameters.AddWithValue("@benutzerName", name);
            checkSelect.Parameters.AddWithValue("@email", email);
            if (Convert.ToInt32(checkSelect.ExecuteScalar()) > 0)
            {
                conn.Close();
                return -1;
            }

            MySqlCommand insert = new MySqlCommand("INSERT INTO nutzer (benutzerName, passwort, email, zuletztAktiv) VALUES (@benutzerName, @pass, @email, @aktiv)", conn);
            insert.Parameters.AddWithValue("@benutzerName", name);
            insert.Parameters.AddWithValue("@pass", passwort);
            insert.Parameters.AddWithValue("@email", email);
            insert.Parameters.AddWithValue("@aktiv", DateTime.Now);
            insert.ExecuteNonQuery();

            MySqlCommand getId = new MySqlCommand("SELECT LAST_INSERT_ID()", conn);
            int id = Convert.ToInt32(getId.ExecuteScalar());
            Nutzer neuerNutzer = new Nutzer(name, passwort, email, id);
            conn.Close();
            return 0;
        }
        public Nutzer Anmelden(string name, string passwort)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand search = new MySqlCommand("SELECT nutzerId, passwort, email FROM nutzer WHERE benutzerName=@benutzerName", conn);
            search.Parameters.AddWithValue("@benutzerName", name);
            MySqlDataReader reader = search.ExecuteReader();
            if (!reader.Read() || reader.GetString("passwort") != passwort)
                return null;
            Nutzer n = new Nutzer(name, passwort, reader.GetString("email"), reader.GetInt32("nutzerId"));
            lock (nutzer)
            {
                nutzer.Add(n);
            }
            reader.Close();
            conn.Close();
            return n;
        }
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
        public List<Beitrag> ErmittleNeueBeitraege(Nutzer n)
        {
            List<Beitrag> beitraege = new List<Beitrag>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand neusteBeitraege = new MySqlCommand(@"
                SELECT b.beitragid, b.text, b.titel, b.erstelltAm, b.autor, b.likes, u.benutzerName
                FROM beitrag b
                JOIN nutzer u ON b.autor = u.nutzerId
                WHERE b.erstelltAm > @zuletztAktiv
                ORDER BY b.erstelltAm DESC
                LIMIT 10", conn);
            neusteBeitraege.Parameters.AddWithValue("@nutzerId", n);
            neusteBeitraege.Parameters.AddWithValue("@zuletztAktiv", n.ZuletztAktiv);
            MySqlDataReader reader = neusteBeitraege.ExecuteReader();
            while (reader.Read())
            {
                beitraege.Add(LeseBeitrag(reader));
            }
            reader.Close();
            if (beitraege.Count < 10)
            {
                int remaining = 10 - beitraege.Count;
                MySqlCommand alteBeitraege = new MySqlCommand(@"
                    SELECT b.beitragId, b.titel, b.text, b.erstelltAm, b.autor, b.likes, u.benutzerName
                    FROM beitrag b
                    JOIN nutzer u ON b.autor = u.nutzerId
                    WHERE b.erstelltAm <= @zuletztAktiv
                    ORDER BY b.erstelltAm DESC
                    LIMIT @max", conn);
                alteBeitraege.Parameters.AddWithValue("@zuletztAktiv", n.ZuletztAktiv);
                alteBeitraege.Parameters.AddWithValue("@max", remaining);
                reader = alteBeitraege.ExecuteReader();
                while (reader.Read())
                {
                    beitraege.Add(LeseBeitrag(reader));
                }
                reader.Close();
            }
            conn.Close();
            return beitraege;
        }
        private Beitrag LeseBeitrag(MySqlDataReader reader)
        {
            int beitragId = reader.GetInt32("beitragId");
            string titel = reader.GetString("titel");
            string text;
            if (reader.IsDBNull(reader.GetOrdinal("text")))
                text = null;
            else
                text = reader.GetString("text");
            DateTime erstelltAm = reader.GetDateTime("erstelltAm");
            int autorId = reader.GetInt32("autor");
            int likes = reader.GetInt32("likes");
            string autorName = reader.GetString("benutzerName");

            Nutzer autor = new Nutzer(autorName, "", "", autorId);
            Beitrag b = new Beitrag(autor, titel, new List<Bild>());
            b.Id = beitragId;
            b.setAnzahlLikes(likes);
            if (text != null)
                b.ErstelleText(text);
            return b;
        }
        public List<Bild> HoleBilder(int beitragId)
        {
            List<Bild> bilder = new List<Bild>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand get = new MySqlCommand("SELECT dateiname FROM bild b.beitragid = @beitragid", conn);
            get.Parameters.AddWithValue("@beitragid", beitragId);
            MySqlDataReader reader = get.ExecuteReader();
            while (reader.Read())
            {
                bilder.Add(new Bild(reader.GetString("dateiname")));
            }
            return bilder;
        }
        public List<Nutzer> ErmittleAbonnierteNutzerMitNeuenBeitraegen(Nutzer n)
        {
            List<Nutzer> result = new List<Nutzer>();
            foreach (Nutzer a in n.AbonnierteNutzer)
            {
                foreach(Beitrag b in a.Beitraege)
                {
                    if (b.Geposted > n.ZuletztAktiv)
                    {
                        result.Add(a);
                        break;
                    }
                }
            }
            return result;
        }
        public Nutzer SucheNutzer(string name)
        {
            foreach (Nutzer n in nutzer)
            {
                if (n.BenutzerName == name)
                {
                    return n;
                }
            }
            return null;
        }

        public int Abonnieren(int nutzerId, int abonnentId)
        {
            if (nutzerId == abonnentId)
                return 1;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand check = new MySqlCommand(@"
                SELECT *
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
        public int ErmittelAbonnentenAnzahl(int nutzerId)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
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
        public int Like(int beitragId, int nutzerId)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
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
            MySqlCommand like = new MySqlCommand(@"
                UPDATE beitrag
                SET likes += 1
                WHERE beitragid = @beitragid", conn);
            like.Parameters.AddWithValue("@beitragid", beitragId);
            like.ExecuteNonQuery();
            conn.Close();
            return 0;
        }
    }
}
