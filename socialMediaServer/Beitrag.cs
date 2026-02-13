using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class Beitrag
    {
        public int Id { get; set; } = 0;
        private DateTime geposted;
        public DateTime Geposted { get => geposted; }
        private string titel;
        public string Titel { get => titel; }
        private int anzahlLikes;
        private Nutzer autor;
        public Nutzer Autor { get => autor; }
        private Text text;
        public Text Text { get => text; }
        private List<Bild> bilder;
        public List<Bild> Bilder { get => bilder; }
        public Beitrag(Nutzer autor, string titel, List<Bild> bild)
        {
            this.autor = autor;
            this.titel = titel;
            anzahlLikes = 0;
            geposted = DateTime.Now;
            bilder = new List<Bild>();
            foreach (Bild bildItem in bild) 
                Hinzufuegen(bildItem);
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
