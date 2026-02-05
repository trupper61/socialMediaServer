using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class Beitrag
    {
        private DateTime geposted;
        private string titel;
        private int anzahlLikes;
        private Nutzer autor;
        private Text text;

        public Beitrag(Nutzer autor, string titel)
        {
            this.autor = autor;
            this.titel = titel;
            anzahlLikes = 0;
            geposted = DateTime.Now;
            // Bild hinzufuegen
        }

        public void Hinzufuegen()
        {
            // Bild
        }

        public void ErstelleText(string text)
        {
            this.text = new Text(text);
        }

        public void Like()
        {
            anzahlLikes++;
        }
    }
}
