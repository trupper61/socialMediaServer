using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMedia
{
    public class Bild
    {
        private int id;
        private static int autowert = 1;
        private string dateiname;
        public string Dateiname { get => dateiname; }
        public string bilddata;
        public Bild(string dateiname)
        {
            this.dateiname = dateiname;
            this.id = autowert++;
        }

        //Gibt die String-Daten eines Bildes wider
        public string toString() 
        {
            return null; // Noch nicht implementiert
        }
    }
}
