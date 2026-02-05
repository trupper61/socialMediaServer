using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class SocialMediaPlatform
    {
        private List<Nutzer> nutzer;

        public SocialMediaPlatform()
        {
            nutzer = new List<Nutzer>();
        }
        public int Registrieren(string name, string passwort, string email)
        {
            foreach (Nutzer n in nutzer)
            {
                if (n.BenutzerName == name || n.Email == email)
                {
                    return -1;
                }
            }
            Nutzer neuerNutzer = new Nutzer(name, passwort, email);
            nutzer.Add(neuerNutzer);
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
