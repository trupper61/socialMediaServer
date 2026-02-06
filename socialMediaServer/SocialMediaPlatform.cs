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
        public int Registrieren(string name, string passwort, string email)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand checkSelect = new MySqlCommand("SELECT COUNT(*) FROM nutzer WHERE benutzerName=@benutzerName OR email=@email", conn);
            checkSelect.Parameters.AddWithValue("@benutzerName", name);
            checkSelect.Parameters.AddWithValue("@email", email);
            if (Convert.ToInt32(checkSelect.ExecuteScalar()) > 0)
            {
                return -1;
            }

            MySqlCommand insert = new MySqlCommand("INSERT INTO nutzer (benutzerName, passwort, email, zuletztAktiv) VALUES (@benutzerName, @pass, @email, @aktiv)", conn);
            insert.Parameters.AddWithValue("@benutzerName", name);
            insert.Parameters.AddWithValue("@pass", passwort);
            insert.Parameters.AddWithValue("@email", email);
            insert.Parameters.AddWithValue("@aktiv", DateTime.Now);
            insert.ExecuteNonQuery();

            Nutzer neuerNutzer = new Nutzer(name, passwort, email);
            return 0;
        }
        public Nutzer Anmelden(string name, string passwort)
        {
            Nutzer n = SucheNutzer(name);
            if (n != null && n.Passwort == passwort)
            {
                return n;
            }
            return null;
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
    }
}
