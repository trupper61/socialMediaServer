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
        public DateTime Geposted { get => geposted; }
        private string titel;
        private int anzahlLikes;
        private Nutzer autor;
        private Text text;
        private List<Bild> bilder;
        public Beitrag(Nutzer autor, string titel, Bild bild)
        {
            this.autor = autor;
            this.titel = titel;
            anzahlLikes = 0;
            geposted = DateTime.Now;
            Hinzufuegen(bild);
        }

        public void Hinzufuegen(Bild bild)
        {
            bilder.Add(bild);
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
