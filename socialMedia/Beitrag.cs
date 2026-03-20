using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMedia
{
    public class Beitrag
    {
        public int Id { get; set; } = 0;
        private DateTime geposted;
        public DateTime Geposted { get => geposted; }
        private string titel;
        private string tag;
        public string Tag { get => tag; }
        public string Titel { get => titel; }
        private int anzahlLikes;
        private Nutzer autor;
        public Nutzer Autor { get => autor; }
        private Text text;
        public Text Text { get => text; }
        private List<Bild> bilder;
        public List<Bild> Bilder { get => bilder; }
        private List<Kommentar> kommentare;

        private int gewichtung;
        public int Gewichtung { get => gewichtung; }
        public Beitrag(Nutzer autor, string titel, List<Bild> bild, string tag, string text)
        {
            this.autor = autor;
            this.titel = titel;
            this.tag = tag;
            this.text = new Text(text);
            anzahlLikes = 0;
            geposted = DateTime.Now;
            bilder = new List<Bild>();
            kommentare = new List<Kommentar>();
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

        public int gebeAnzahlLikes() 
        {
            return anzahlLikes;
        }

        public List<Kommentar> gebeKommentare() 
        {
            return kommentare;
        }

        public void setAnzahlLikes(int value) 
        {
            anzahlLikes = value;
        }

        public void setGeposted(DateTime gepostet) 
        {
            this.geposted = gepostet;
        }

        public void kommentarHinzufuegen(Kommentar kommentar) 
        {
            this.kommentare.Add(kommentar);
        }

        public void SetKommentare(List<Kommentar> kommentare) 
        {
            this.kommentare = kommentare;
        }

        public void setGewichtung(int gewichtung) 
        {
            this.gewichtung = gewichtung;
        }
    }
}
