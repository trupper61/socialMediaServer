using socialMediaServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSocialMedia
{
    public partial class Form1 : Form
    {
        private TextBox tbNutzername;
        private TextBox tbPasswort;
        private TextBox titelEingabe;
        private Panel panel;
        private Button registrieren;
        private Button anmeldeButton;
        private Button passVergessen;
        private Button generierePasswort;
        private TextBox email;
        private PictureBox logo;
        private bool registerToggle = false;
        public List<string> bilder = new List<string>();
        private List<Beitrag> beitraege = new List<Beitrag>();
        private Button loadMoreBtn = new Button();
        public static Client client = new Client();
        private int beitragOffset = 0;
        private bool laedGerade = false;
        public static bool connectionLost = false;

        public Form1()
        {
            InitializeComponent();
            Form1.client.OnBeitragErhalten += BeitragErhalten;
            Form1.client.OnConnectionLost += ConnectionLost;
            ErstellePanel();
        }
        private void ConnectionLost()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ConnectionLost));
                return;
            }
            MessageBox.Show("Verbindung zum Server verloren");
            connectionLost = true;
            verbindenBtn.Visible = true;
            Abmelden();
        }
        public void UpdateProfilePicture()
        {
            byte[] profileBytes = client.LadeProfilePicture();
            if (connectionLost)
                return;
            using (MemoryStream ms = new MemoryStream(profileBytes))
            {
                Image img = Image.FromStream(ms);
                profilePic.Image = img;
            }
            profilePic.BringToFront();
        }
        //Das Laden aller Elemente innerhalb des Login-Bildschirms.
        public void ErstellePanel()
        {
            panel = new Panel();
            panel.Left = (this.ClientSize.Width - panel.Width + 50) / 2;
            panel.Top = (this.ClientSize.Height - panel.Height - 20) / 2;
            panel.Width = this.Width;
            panel.Height = this.Height;
            this.Controls.Add(panel);

            logo = new PictureBox()
            {
                Width = 150,
                Height = 100,
                BackColor = Color.Transparent,
                BackgroundImage = Properties.Resources.logo              
            };
            this.Controls.Add(logo);
            logo.BringToFront();
            Label anmelden = new Label()
            {
                Width = 150,
                Height = 15,
                Text = "Anmelden"
                
            };
            panel.Controls.Add(anmelden);

            tbNutzername = new TextBox()
            {
                Width = 150,
                Height = 15,
                Location = new Point(0, anmelden.Location.Y + 20),
                Text = "Benutzername...",
                
                
            };
            tbPasswort = new TextBox()
            {
                Width = 150,
                Height = 15,
                Location = new Point(0, anmelden.Location.Y + 40),
                Text = "Passwort..."
            };
            panel.Controls.Add(tbNutzername);
            panel.Controls.Add(tbPasswort);

            tbNutzername.Click += tbNutzername_Click;
            tbPasswort.Click += tbPasswort_Click;

            passVergessen = new Button()
            {
                Width = 75,
                Height = 25,
                Location = new Point(150, anmelden.Location.Y + 38),
                BackColor = Color.White,
                Text = "Vergessen?"
            };
            anmeldeButton = new Button()
            {
                
                Size = new Size(150, 25),
                Location = new Point(0, anmelden.Location.Y + 60),
                BackColor = Color.White,
                Text = "Anmelden"
            };
            registrieren = new Button()
            {
                Size = new Size(150, 25),
                Location = new Point(0, anmelden.Location.Y + 85),
                BackColor = Color.White,
                Text = "Noch kein Nutzer?"
            };
            email = new TextBox()
            {
                Visible = false,
                Width = 150,
                Height = 15,
                Location = new Point(0, anmelden.Location.Y + 60),
                Text = "Email Eingeben"
            };
            generierePasswort = new Button()
            {
                Size = new Size(150, 25),
                Location = new Point(0, anmelden.Location.Y + 85),
                BackColor = Color.White,
                Text = "Neues Passwort Anfordern"
            };
            email.Click += email_Click;
            panel.Controls.Add(anmeldeButton);
            panel.Controls.Add(registrieren);
            panel.Controls.Add(email);
            panel.Controls.Add(passVergessen);
            passVergessen.Click += passVergessen_Click;
            generierePasswort.Click += generierePasswort_Click;
            if(!registerToggle) 
            {
                anmeldeButton.Click += anmeldeButton_Click;
                registrieren.Click += registrieren_Click;
            }
            else if(registerToggle) 
            {
                anmeldeButton.Click += anmeldeButton_Click;
            }                    
        }
        //Wird Nach Login aufgerufen. Anzeige der Gesamten UI des eigentlichen Programs.
        private void zeigeProgram() 
        {
            menuPanel.BackColor = Color.White;
            Button buttonBeitraege = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 10),
                BackColor = Color.White,
                Text = "Beiträge"
            };
            Button buttonBeliebt = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 70),
                BackColor = Color.White,
                Text = "Beliebt"
            };
            Button buttonNurAbos = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 130),
                BackColor = Color.White,
                Text = "Beiträge Abonnierter Nutzer"
            };
            Button buttonErstellen = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 190),
                BackColor= Color.White,
                Text = "Beitrag Erstellen"
            };
            Button empfehlungen = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 250),
                BackColor = Color.White,
                Text = "Empfehlungen"
            };
            Button buttonChat = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 310),
                BackColor = Color.White,
                Text = "Chat"
            };
            Button buttonSuchen = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 370),
                BackColor = Color.White,
                Text = "Suchen"
            };

            loadMoreBtn = new Button()
            {
                Text = "Weitere Beiträge laden",
                Width = 200,
                Height = 40,
            };
            loadMoreBtn.Click += LoadMoreBtn_Click;


            
            menuPanel.Controls.Add(buttonBeitraege);
            menuPanel.Controls.Add(buttonBeliebt);
            menuPanel.Controls.Add(buttonNurAbos);
            menuPanel.Controls.Add(empfehlungen);
            menuPanel.Controls.Add(buttonErstellen);
            menuPanel.Controls.Add(buttonChat);
            menuPanel.Controls.Add(buttonSuchen);
            if(!laedGerade) 
            {
                buttonErstellen.Click += erstellen_Click;
                buttonChat.Click += Chat_Click;
                buttonSuchen.Click += Suche_Click;
                buttonBeitraege.Click += buttonBeitraege_Click;
                buttonBeliebt.Click += buttonBeliebt_Click;
                buttonNurAbos.Click += buttonNurAbos_Click;
                empfehlungen.Click += empfehlungen_Click;
            }
            zeigeInhalte();
        }

        private void buttonBeitraege_Click(object sender, EventArgs e)
        {
            laedGerade = true;
            zeigeInhalte();
            laedGerade = false;
        }

        private void zeigeInhalte() 
        {
            Cursor = Cursors.WaitCursor;
            UpdateProfilePicture();
            EmpfangeDaten();
            inhaltAnzeige.Enabled = true;
            inhaltAnzeige.Visible = true;
            menuPanel.Visible = true;
            Cursor = Cursors.Default;
        }
        //Wird nach anmeldung ausgeführt. Die Anzeige aller neusten Beiträge ist Standard.
        private async void EmpfangeDaten() 
        {
            beitragOffset = 0;
            inhaltAnzeige.Controls.Clear();
            beitraege = await Task.Run(() => client.beitraegeAnfragen(false, false, false, beitragOffset));
            if(beitraege == null || connectionLost) 
            {
                return;
            }
            List<Control> controls = this.Controls.Find("Inhalte", true).ToList();
            foreach (Control c in controls)
            {
                Inhalte i = c as Inhalte;
                i.Beitrag.SetKommentare(i.ladekomm());
                i.Autor = i.GetUserData();
                i.setDaten(i.pictures);
                i.ladeVorschau();
            }
            beitragOffset = beitraege.Count;
            loadMoreBtn.Tag = "neue";
            inhaltAnzeige.Controls.Add(loadMoreBtn);
        }

        private async void BeitragErhalten(Beitrag b)
        {
            if (connectionLost)
                return;
            if (this.InvokeRequired)
                this.Invoke((Action<Beitrag>)BeitragErhalten, b);
            else
            {
                Inhalte inhalt = new Inhalte(b);
                beitraege.Add(b);
                inhalt.setDaten(inhalt.pictures);
                inhaltAnzeige.Controls.Add(inhalt);
            }
        }
        private async void LoadMoreBtn_Click(object sender, EventArgs e)
        {
            laedGerade = true;
            loadMoreBtn.Enabled = false;
            loadMoreBtn.Text = "Lade...";
            List<Beitrag> neue = new List<Beitrag>();
            if (loadMoreBtn.Tag == "abos")
                neue = await Task.Run(() => client.beitraegeAnfragen(true, false, false, beitragOffset));
            else if (loadMoreBtn.Tag == "neue")
                neue = await Task.Run(() => client.beitraegeAnfragen(false, false, false, beitragOffset));
            else if (loadMoreBtn.Tag == "beliebt")
                neue = await Task.Run(() => client.beitraegeAnfragen(false, false, true, beitragOffset));
            else if (loadMoreBtn.Tag == "empfehlung")
                neue = await Task.Run(() => client.beitraegeAnfragen(false, true, false, beitragOffset));
            List<Control> controls = this.Controls.Find("Inhalte", true).ToList();
            for (int i = controls.Count - 1; i >= beitragOffset; i--) 
            {
                Inhalte inhalt = controls[i] as Inhalte;
                inhalt.Beitrag.SetKommentare(inhalt.ladekomm());
                inhalt.Autor = inhalt.GetUserData();
                inhalt.setDaten(inhalt.pictures);
            }
            inhaltAnzeige.Controls.Remove(loadMoreBtn);
            inhaltAnzeige.Controls.Add(loadMoreBtn);
            beitragOffset += neue.Count;
            loadMoreBtn.Text = "Weitere Beiträge laden";
            loadMoreBtn.Enabled = true;
            if (neue.Count == 0)
            {
                loadMoreBtn.Text = "Keine weiteren Beiträge vorhanden";
            }
            laedGerade = false;
        }

        private void refresh() 
        {
            
        }
        private void tbNutzername_Click(object sender, EventArgs e) 
        {
            TextBox t = (TextBox)sender;

            t.Text = "";
        }

        private void tbPasswort_Click(object sender, EventArgs e) 
        {
            TextBox t = (TextBox)sender;

            t.Text = "";
        }
        private void email_Click(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;

            t.Text = "";
        }
        private void anmeldeButton_Click(object sender, EventArgs e) 
        {
            if(!registerToggle) 
            {
                string antwort = client.anmelden(tbNutzername.Text, tbPasswort.Text);
                if (connectionLost)
                    return;
                if (antwort.Contains("+")) 
                {
                    panel.Hide();
                    profilePic.Visible = true;
                    this.Controls.Remove(logo);
                    zeigeProgram();
                }
            }
            if(registerToggle) 
            {
                NutzerRegistrieren();
            
            }
        }

        private void passVergessen_Click(object sender, EventArgs e) 
        {
            this.panel.Controls.Clear();
            this.panel.Controls.Add(this.email);
            email.Visible = true;
            this.panel.Controls.Add(generierePasswort);
        }
        private void generierePasswort_Click(object sender, EventArgs e) 
        {
            string antwort = client.PasswortVergessenAktualisierung(email.Text);
            if (connectionLost)
                return;
            MessageBox.Show(antwort);
            this.panel.Controls.Clear();
            this.Controls.Remove(panel);
            ErstellePanel();
        }
        private void registrieren_Click(object sender, EventArgs e) 
        {
            //Um weniger Elemente für das Handhaben von Anmelden und Registrieren zu benötigen, ist die Funktionalität der Knöpfe von dem bool "Registertoggle" abhängig.
            if(!registerToggle) 
            {
                tbNutzername.Text = "Nutzername...";
                tbPasswort.Text = "Passwort festlegen...";
                registrieren.Text = "Anmelden";
                anmeldeButton.Text = "Registrieren";
                email.Visible = true;
                passVergessen.Visible = false;
                registrieren.Location = new Point(registrieren.Location.X, registrieren.Location.Y + 20);
                anmeldeButton.Location = new Point(anmeldeButton.Location.X, anmeldeButton.Location.Y + 20);
                registerToggle = true;
            }
            else 
            {
                tbNutzername.Text = "Benutzername...";
                tbPasswort.Text = "Passwort...";
                registrieren.Text = "Noch kein Nutzer?";
                anmeldeButton.Text = "Anmelden";
                registrieren.Location = new Point(registrieren.Location.X, registrieren.Location.Y - 20);
                anmeldeButton.Location = new Point(anmeldeButton.Location.X, anmeldeButton.Location.Y - 20);
                registerToggle = false;
                passVergessen.Visible = true;
            }
            
        }

        private void NutzerRegistrieren() 
        {
            if (tbNutzername.Text.Count() < 3)
            {
                MessageBox.Show("Der Benutzername muss mind. 4 Zeichen lang sein");
                return;
            }
            else if (tbPasswort.Text.Count() < 3)
            {
                MessageBox.Show("Das Passwort muss mind. 4 Zeichen lang sein");
                return;
            }
            else if (!email.Text.Contains("@") && email.Text.Count() < 4)
            {
                MessageBox.Show("Die E-Mail muss eine gültige E-Mail sein");
                return;
            }
            client.registrieren(tbNutzername.Text, tbPasswort.Text, email.Text);
        }

        private void bildauswaehlen_OnClick(object sender, EventArgs e) 
        {
            bilder = Client.BilderAuswaehlen();
        }
        //Logik für das Erstellen eines Beitrags für den Nutzer. 
        private void erstellen_Click(object sender, EventArgs e)
        {
            laedGerade = true;
            if (beitragsErstellungsPanel.Visible)
            {
                beitragsErstellungsPanel.Visible = false;
                return;
            }
            beitragsErstellungsPanel.Controls.Clear();
            beitragsErstellungsPanel.Controls.Add(this.tagPick);
            beitragsErstellungsPanel.Controls.Add(this.tagLabel);
            beitragsErstellungsPanel.Controls.Add(this.textVerfassung);
            beitragsErstellungsPanel.Controls.Add(this.verfassungLabel);
            beitragsErstellungsPanel.Visible = true;
            tagPick.Items.Clear();
            tagPick.Visible = true;
            tagPick.Items.AddRange(new string[] {
                "Tiere",
                "Memes",
                "Sonstiges",
                "News"
            });
            inhaltAnzeige.Show();

            titelEingabe = new TextBox();
            beitragsErstellungsPanel.Controls.Add(titelEingabe);
            titelEingabe.Location = new Point(titelEingabe.Location.X + 10, titelEingabe.Location.Y + 10);

            bilder = new List<string>();

            Button bildauswaehlen = new Button();
            bildauswaehlen.Location = new Point(bildauswaehlen.Location.X + 10, bildauswaehlen.Location.Y + 40);
            bildauswaehlen.Width = 100;
            bildauswaehlen.Height = 25;
            bildauswaehlen.Text = "Bild auswählen";
            beitragsErstellungsPanel.Controls.Add(bildauswaehlen);
            bildauswaehlen.Click += bildauswaehlen_OnClick;

            Button beitragErstellen = new Button();
            beitragErstellen.Location = new Point(bildauswaehlen.Location.X, bildauswaehlen.Location.Y + 70);
            beitragErstellen.Width = 100;
            beitragErstellen.Height = 25;
            beitragErstellen.Text = "Beitrag erstellen";
            beitragsErstellungsPanel.Controls.Add(beitragErstellen);

            Button closeBtn = new Button()
            {
                Width = 50,
                Height = 20,
                Text = "Close",
                Location = new Point(beitragsErstellungsPanel.Width - 70, titelEingabe.Location.Y)
            };
            closeBtn.Click += (s, e2) =>
            {
                beitragsErstellungsPanel.Visible = false;
            };
            beitragsErstellungsPanel.Controls.Add(closeBtn);

            beitragsErstellungsPanel.BringToFront();

            tagPick.Visible = true;
            tagPick.BringToFront();
            beitragsErstellungsPanel.Controls.Add(tagPick);
            beitragErstellen.Click += beitragErstellen_Click;
            laedGerade = false;
        }
        //Logik für das Senden des erstellten Beitrags an den Server.
        private void beitragErstellen_Click(object sender, EventArgs e) 
        {
            if(tagPick.Text == "") 
            {
                MessageBox.Show("Wähle ein Tag aus!");
                return;
            }
            tagPick.Visible = false;
            client.beitragSenden(titelEingabe.Text, bilder, tagPick.Text, this.textVerfassung.Text);
            if (connectionLost)
                return;
            beitragsErstellungsPanel.Visible = false;
            laedGerade = true;
            EmpfangeDaten();
            laedGerade = false;
        }
        
        public void Abmelden()
        {
            inhaltAnzeige.Controls.Clear();
            inhaltAnzeige.Visible = false;
            profilePic.Visible = false;
            menuPanel.Visible = false;
            profilePic.Tag = null;
            tbNutzername.Text = "Benutzername...";
            tbPasswort.Text = "Passwort...";
            email.Text = "E-Mail...";
            ErstellePanel();
        }

        private void profilePic_Click(object sender, EventArgs e)
        {
            inhaltAnzeige.Controls.Clear();
            ProfileControl profil = new ProfileControl();
            profil.OnProfileChange = (img) =>
            {
                profilePic.Image = img;
            };
            profil.OnAbmelden = () =>
            {
                Form1.client.OnBeitragErhalten = BeitragErhalten;
                Abmelden();
            };
            profil.OnClose = () =>
            {
                inhaltAnzeige.Controls.Clear();
                zeigeInhalte();
            };
            inhaltAnzeige.Controls.Add(profil);            
        }

        private void profilePic_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.InitialDelay = 250;
            tt.SetToolTip(profilePic, "Profil anpassen");
            Cursor = Cursors.Hand;
            profilePic.BackColor = Color.FromArgb(230, 230, 230);
            profilePic.Size = new Size(54, 54);
        }

        private void profilePic_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            profilePic.Size = new Size(50, 50);
        }

        private async void buttonBeliebt_Click(object sender, EventArgs e) 
        {
            laedGerade = true;
            beitragOffset = 0;
            inhaltAnzeige.Controls.Clear();
            beitraege = await Task.Run(() => client.beitraegeAnfragen(false, false, true, beitragOffset));
            if (beitraege == null)
            {
                return;
            }
            List<Control> controls = this.Controls.Find("Inhalte", true).ToList();

            //Laden der Beiträge in Winforms
            foreach (Control c in controls)
            {
                Inhalte i = c as Inhalte;
                i.Beitrag.SetKommentare(i.ladekomm());
                i.Autor = i.GetUserData();
                i.setDaten(i.pictures);
                i.ladeVorschau();
            }
            beitragOffset += beitraege.Count;
            loadMoreBtn.Tag = "beliebt";
            inhaltAnzeige.Controls.Add(loadMoreBtn);
            laedGerade = false;
        }

        private async void buttonNurAbos_Click(object sender, EventArgs e) 
        {
            laedGerade = true;
            inhaltAnzeige.Controls.Clear();
            beitraege = await Task.Run(() => client.beitraegeAnfragen(true, false, false, beitragOffset));
            if (beitraege == null)
            {
                return;
            }
            List<Control> controls = this.Controls.Find("Inhalte", true).ToList();
            foreach (Control c in controls)
            {
                Inhalte i = c as Inhalte;
                i.Beitrag.SetKommentare(i.ladekomm());
                i.Autor = i.GetUserData();
                i.setDaten(i.pictures);
                i.ladeVorschau();
            }
            beitragOffset = beitraege.Count;
            loadMoreBtn.Tag = "abos";
            inhaltAnzeige.Controls.Add(loadMoreBtn);
            laedGerade = false;
        }
        
        private async void empfehlungen_Click(object sender, EventArgs e) 
        {
            laedGerade = true;
            inhaltAnzeige.Controls.Clear();
            beitragOffset = 0;
            beitraege = await Task.Run(() => client.beitraegeAnfragen(false, true, false, beitragOffset));
            if (beitraege == null)
            {
                return;
            }
            List<Control> controls = this.Controls.Find("Inhalte", true).ToList();
            foreach (Control c in controls)
            {
                Inhalte i = c as Inhalte;
                i.Beitrag.SetKommentare(i.ladekomm());
                i.Autor = i.GetUserData();
                i.setDaten(i.pictures);
                i.ladeVorschau();
            }
            beitragOffset = beitraege.Count;
            loadMoreBtn.Tag = "empfehlung";
            inhaltAnzeige.Controls.Add(loadMoreBtn);
            laedGerade = false;
        }
        private void Suche_Click(object sender, EventArgs e)
        {
            laedGerade = true;
            inhaltAnzeige.Controls.Clear();
            SearchControl searchControl = new SearchControl();
            inhaltAnzeige.Controls.Add(searchControl);
            laedGerade = false;
        }

        private void Chat_Click(object sender, EventArgs e)
        {
            laedGerade = true;
            ChatControl cc = new ChatControl();
            cc.ChatSelected += ChatControl_ChatSelected;
            inhaltAnzeige.Controls.Clear();
            inhaltAnzeige.Controls.Add(cc);
            laedGerade = false;
        }

        private async void ChatControl_ChatSelected(int chatId)
        {
            ChatOverviewControl coc = new ChatOverviewControl(chatId);
            inhaltAnzeige.Controls.Clear();
            inhaltAnzeige.Controls.Add(coc);
            coc.LoadNachrichten();
        }

        private void verbindenBtn_Click(object sender, EventArgs e)
        {
            if (Form1.client.Verbinden())
            {
                MessageBox.Show("Verbindung zum Server aufgebaut");
                verbindenBtn.Visible = false;
                connectionLost = false;
            }
            else
            {
                MessageBox.Show("Verbindung zum Server fehlgeschlagen");
            }
        }
    }
}
