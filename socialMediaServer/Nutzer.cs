using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class Nutzer
    {
        private int benutzerId;
        public int BenutzerId { get => benutzerId; }
        private string benutzerName;
        public string BenutzerName { get => benutzerName; }
        private string passwort;
        public string Passwort { get => passwort; }
        private string email;
        public string Email { get => email; }
        private DateTime zuletztAktiv;
        public DateTime ZuletztAktiv { get => zuletztAktiv; }
        private List<Nutzer> abonnenten;
        private List<Nutzer> abonnierteNutzer;
        public List<Nutzer> AbonnierteNutzer { get => abonnierteNutzer; }
        private List<Beitrag> beitraege;
        public List<Beitrag> Beitraege { get => beitraege; }
        private List<Bild> bilder;

        public Nutzer(string name, string passwort, string email, int benutzerId)
        {
            this.benutzerId = benutzerId;
            this.benutzerName = name;
            this.passwort = passwort;
            this.email = email;
            zuletztAktiv = DateTime.Now;
            abonnenten = new List<Nutzer>();
            abonnierteNutzer = new List<Nutzer>();
            beitraege = new List<Beitrag>();
            bilder = new List<Bild>();
        }

        public void Abonnieren(Nutzer n)
        {
            zuletztAktiv = DateTime.Now;
            if (!abonnierteNutzer.Contains(n) && n != this)
            {
                abonnierteNutzer.Add(n);
                n.abonnenten.Add(this);
            }
        }

        public void Like(Beitrag beitrag)
        {
            zuletztAktiv = DateTime.Now;
            if (!beitraege.Contains(beitrag))
            {
                beitrag.Like();
            }
        }
        public void HinzufuegenBild(Bild bild)
        {
            zuletztAktiv = DateTime.Now;
            bilder.Add(bild);
        }
    }
}
