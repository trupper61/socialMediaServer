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
    public partial class KommentarControl : UserControl
    {
        private string benutzer;
        private string text;
        private Kommentar kommentar;
        private int nutzerId;

        public KommentarControl(string nutzer, string text, Kommentar kommentar)
        {
            InitializeComponent();
            setKommentar(nutzer, text, kommentar);
            nutzerId = kommentar.AutorId;
            this.kommentar = kommentar;
        }
        //Alle relevanten Daten für ein Kommentar werden gesetzt
        public void setKommentar(string nutzer, string text, Kommentar kommentar) 
        {
            this.text = text;
            this.komm.Text = this.text;

            this.benutzer = nutzer;

            this.autor.Text = benutzer;
            this.profil.Image = Inhalte.konvertiereBild(kommentar.profil);
            this.timeLb.Text = kommentar.Timestamp.ToString("g");
        }
        //Öffnen des Nutzerprofils
        private async void profil_Click(object sender, EventArgs e)
        {
            UserOverviewControl userOverview = new UserOverviewControl();
            userOverview.Location = new Point((this.Parent.Parent.Width - userOverview.Width) / 2, (this.Parent.Parent.Height - userOverview.Height) / 2);
            this.Parent.Parent.Controls.Add(userOverview);
            userOverview.BringToFront();
            await userOverview.LadeNutzer(nutzerId);
        }

        private void profil_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 250;
            tt.SetToolTip(profil, "Click to view");
        }
    }
}
