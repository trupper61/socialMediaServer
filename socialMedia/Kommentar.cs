using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMedia
{
    public class Kommentar
    {
        public int Id { get; set; }
        public string Nachricht { get; set; }
        public DateTime Timestamp { get; set; }
        public int AutorId { get; set; }
        public string autor; //Muss noch als Parameter für den Konstruktor übergeben werden!
        public string profil;
        public int? OberKommentarId { get; set; }
        public List<Kommentar> Antworten { get; set; }
        public int likes;
        public Kommentar(string nachricht, DateTime timestamp, int autorId)
        {
            Nachricht = nachricht;
            Timestamp = timestamp;
            AutorId = autorId;
            Antworten = new List<Kommentar>();
        }
        public void FuegeAntwortHinzu(Kommentar kommentar)
        {
            Antworten.Add(kommentar);
        }
    }
}
