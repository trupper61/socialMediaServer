using socialMediaServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSocialMedia
{
    public partial class Kommentaruebersicht : UserControl
    {
        private Beitrag beitrag;
        private List<Kommentar> kommentare;
        private Inhalte inhalte;
        public Kommentaruebersicht(Beitrag beitrag, Inhalte inhalte)
        {
            InitializeComponent();
            this.beitrag = beitrag;
            this.kommentare = new List<Kommentar>();
            this.kommentare = beitrag.gebeKommentare();
            kommentareAnzeigen();
            this.inhalte = inhalte;
        }

        private void erstellen_Click(object sender, EventArgs e)
        {
            Form1.client.ErstelleKommentar(beitrag.Id, this.inhaltKommentar.Text, null);
            this.inhalte.Beitrag.SetKommentare(inhalte.ladekomm());
            this.kommentare = beitrag.gebeKommentare();
            this.inhalte.ladeVorschau();
            kommentarsektion.Controls.Clear();
            kommentarVerfassen.Visible = false;
            kommentareAnzeigen();
        }

        public void kommentareAnzeigen() 
        {
            this.kommentarsektion.Controls.Clear();
            if(kommentare == null) { return; }
            foreach(Kommentar k in kommentare) 
            {
                KommentarControl kc = new KommentarControl(k.autor, k.Nachricht, k);
                this.kommentarsektion.Controls.Add(kc);
            }
        }

        private void schliessen_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void verfassen_Click(object sender, EventArgs e)
        {
            kommentarVerfassen.Visible = true;
        }
    }
}
