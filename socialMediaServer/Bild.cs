using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class Bild
    {
        private int id;
        private static int autowert = 1;
        private string dateiname;
        public string Dateiname { get => dateiname; }
        public Bild(string dateiname)
        {
            this.dateiname = dateiname;
            this.id = autowert++;
        }
    }
}
