using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class Nutzer
    {
        private string benutzerName;
        private string passwort;
        private string email;
        private DateTime zuletztAktiv;
        private List<Nutzer> abonnenten;
        private List<Nutzer> abonnierteNutzer;
        private List<Beitrag> beitraege;
        // bilder Liste

        public Nutzer(string name, string passwort, string email)
        {
            this.benutzerName = name;
            this.passwort = passwort;
            this.email = email;
            zuletztAktiv = DateTime.Now;
            abonnenten = new List<Nutzer>();
            abonnierteNutzer = new List<Nutzer>();
            beitraege = new List<Beitrag>();

        }



    }
}
