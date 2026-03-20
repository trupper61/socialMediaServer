using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMedia
{
    public class Nutzer
    {
        private int benutzerId;
        public int BenutzerId { get => benutzerId; }
        public string BenutzerName { get; set; }
        private string passwort;
        public string Passwort { get => passwort; }
        public string Email { get; set; }
        private DateTime zuletztAktiv;
        public DateTime ZuletztAktiv { get; set; }
        private List<Nutzer> abonnenten;
        private List<Nutzer> abonnierteNutzer;
        public List<Nutzer> AbonnierteNutzer { get => abonnierteNutzer; }
        private List<Beitrag> beitraege;
        public List<Beitrag> Beitraege { get => beitraege; }
        private List<Bild> bilder;
        public int AbonnentenAnzahl { get; set; }
        public string ProfilBild { get; set; }
        public Nutzer(string name, string passwort, string email, int benutzerId)
        {
            this.benutzerId = benutzerId;
            this.BenutzerName = name;
            this.passwort = passwort;
            this.Email = email;
            zuletztAktiv = DateTime.Now;
            abonnenten = new List<Nutzer>();
            abonnierteNutzer = new List<Nutzer>();
            beitraege = new List<Beitrag>();
            bilder = new List<Bild>();
            AbonnentenAnzahl = 0;
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
