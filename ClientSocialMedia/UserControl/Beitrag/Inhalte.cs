using socialMediaServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSocialMedia
{
    public partial class Inhalte : UserControl
    {
        public List<string> pictures;
        public string titel;
        public string tagBeitrag;
        public List<Image> anzeigeBilder = new List<Image>();
        private int scrollIndex = 0;
        private int beitragId;
        private Beitrag beitrag;
        private string text;
        public Beitrag Beitrag { get => beitrag; set => beitrag = value; }
        public Nutzer Autor { get; set; }
        Kommentaruebersicht ku;
        public Action<ChatOverviewControl> OnChatClicked;
        public Inhalte(Beitrag beitrag)
        {
            InitializeComponent();
            if (Form1.connectionLost)
                return;
            this.beitrag = beitrag;
            this.pictures = new List<string>();
            this.titel = beitrag.Titel;
            this.beitragId = beitrag.Id;
            this.tagBeitrag = beitrag.Tag;
            this.text = beitrag.Text.text;
            ku = new Kommentaruebersicht(this.beitrag, this);
            this.Controls.Add(ku);
            ku.Visible = false;
            if (Autor == null)
                Autor = new Nutzer("Nutzername", "", "", beitrag.Autor.BenutzerId);
            foreach (Bild b in beitrag.Bilder)
            {

                this.pictures.Add(b.bilddata);
            }
            setDaten(pictures);
        }

        private void SetToolTipps()
        {
            ToolTip name = new ToolTip();
            name.SetToolTip(nutzerNameLb, nutzerNameLb.Text);
            ToolTip titel = new ToolTip();
            titel.SetToolTip(beitragTitel, beitragTitel.Text);
        }
        //Alle Daten werden für die Existenz des Beitrags festgelegt.
        public void setDaten(List<string> bilder) 
        {
            this.beitragTitel.Text = this.titel;
            this.tag.Text = this.tagBeitrag;
            this.beitragText.Text = this.text;
            this.beitragTitel.Left = (this.Width - beitragTitel.Width) / 2;
            likesLb.Text = $"Anzahl Likes: {this.beitrag.gebeAnzahlLikes()}";
            nutzerNameLb.Text = Autor.BenutzerName;
            timeLb.Text = $"Erstellt am: {beitrag.Geposted.ToString("g")}";
            if (bilder.Count != 0)
            {
                konvertiereBilder(bilder);
                this.beitragBild.BackgroundImage = anzeigeBilder[0];
            }
            if (anzeigeBilder.Count == 1)
            {
                next.Visible = false;
            }
            if (Autor.ProfilBild != null)
            {
                byte[] imageBytes = Convert.FromBase64String(Autor.ProfilBild);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);
                    profilePicPb.Image = img;
                }
            }
            SetToolTipps();
        }

        public Nutzer GetUserData()
        {
            return Form1.client.LadeNutzer(beitrag.Autor.BenutzerId);
            
        }
        public void konvertiereBilder(List<string> bilder)
        {
            //Die Übertragung von Bildern ist nur mittels Strings möglich. Die Strings werden in Bilder übersetzt und den Beitrag gegeben.
            if (anzeigeBilder.Count > 0)
                return;
            foreach (string str in bilder)
            {
                byte[] imageBytes = Convert.FromBase64String(str);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);
                    anzeigeBilder.Add(new Bitmap(img));
                }
            }
        }
        //Dasselbe wie oben nur mit einzelnen Bildern. -> Wichtig für Profilbilder
        public static Image konvertiereBild(string bild) 
        {
            if(bild == null || bild == "" || bild == "null") 
                return null;
            byte[] imageBytes = Convert.FromBase64String(bild);
            Image img;
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                img = Image.FromStream(ms);
            }
            return img;
        }
        //Navigierung der jeweiligen Bildelemente
        private void next_Click(object sender, EventArgs e)
        {
            if (scrollIndex == anzeigeBilder.Count - 1)
            {
                next.Visible = false;
                return;
            }
            else
            {
                last.Visible = true;
                scrollIndex++;
                this.beitragBild.BackgroundImage = anzeigeBilder[scrollIndex];
            }
            if (scrollIndex == anzeigeBilder.Count - 1)
                next.Visible = false;
        }

        private void last_Click(object sender, EventArgs e)
        {
            if(scrollIndex == 0) 
            {
                last.Visible = false;
                return;
            }
            else 
            {
                next.Visible = true;
                scrollIndex--;
                this.beitragBild.BackgroundImage = anzeigeBilder[scrollIndex];
            }
            if (scrollIndex == 0)
                last.Visible = false;
        }

        private void likeBtn_Click(object sender, EventArgs e)
        {
            if (Form1.laedGerade)
                return;
            string reply = Form1.client.Like(beitragId);
            if (Form1.connectionLost)
                return;
            string[] parts = reply.Split(';');
            if (parts[0] == "+")
                likesLb.Text = $"Anzahl Likes: {this.beitrag.gebeAnzahlLikes() + 1}";
            else
                MessageBox.Show(parts[1]);
        }

        private void anzeigen_Click(object sender, EventArgs e)
        {
            ku.kommentareAnzeigen();
            ku.Visible = true;
            ku.BringToFront();
        }

        public List<Kommentar> ladekomm() 
        {
            List<Kommentar> k = new List<Kommentar>();
            k = Form1.client.LadeKommentare(this.beitrag.Id);
            if (Form1.connectionLost)
                return null;
            foreach (Kommentar kom in k) 
            {
                this.beitrag.kommentarHinzufuegen(kom);
            }

            return k;
        }

        public void ladeVorschau() 
        {
            this.kommentareVorschau.Controls.Clear();
            for(int i = 0; i < beitrag.gebeKommentare().Count; i++) 
            {
                KommentarControl kc = new KommentarControl(beitrag.gebeKommentare()[i].autor, beitrag.gebeKommentare()[i].Nachricht, beitrag.gebeKommentare()[i]);
                kommentareVorschau.Controls.Add(kc);
            }
        }

        private async void profilePicPb_Click(object sender, EventArgs e)
        {
            if (Form1.laedGerade)
                return;
            UserOverviewControl userOverview = new UserOverviewControl();
            userOverview.Location = new Point((this.Parent.Parent.Width - userOverview.Width) / 2, (this.Parent.Parent.Height - userOverview.Height) / 2);
            this.Parent.Parent.Controls.Add(userOverview);
            userOverview.BringToFront();
            userOverview.OnChatCreated += ShowChat;
            await userOverview.LadeNutzer(beitrag.Autor.BenutzerId);
        }

        private void ShowChat(ChatOverviewControl coc)
        {
            OnChatClicked?.Invoke(coc);
        }
        private void profilePicPb_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 250;
            tt.SetToolTip(profilePicPb, "Click to view");
        }

        private void beitragBild_Click(object sender, EventArgs e)
        {
            if (Form1.laedGerade)
                return;
            List<Image> images = Form1.client.HoleOriginalBilder(beitragId);
            ImageViewerControl viewer = new ImageViewerControl(images, scrollIndex);
            Form form = this.FindForm();
            viewer.Dock = DockStyle.Fill;
            form.Controls.Add(viewer);
            viewer.BringToFront();
        }

        private void beitragBild_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void beitragBild_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}
