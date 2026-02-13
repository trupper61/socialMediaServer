using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private Panel panel;
        private Button registrieren;
        private Button anmeldeButton;
        private TextBox email;
        private bool registerToggle = false;
        public static Client client = new Client();
        public Form1()
        {
            InitializeComponent();
            ErstellePanel();
        }

        public void ErstellePanel()
        {
            panel = new Panel();
            panel.Left = (this.ClientSize.Width - panel.Width + 50) / 2;
            panel.Top = (this.ClientSize.Height - panel.Height - 20) / 2;
            panel.Width = this.Width;
            panel.Height = this.Height;
            this.Controls.Add(panel);

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
            email.Click += email_Click;
            panel.Controls.Add(anmeldeButton);
            panel.Controls.Add(registrieren);
            panel.Controls.Add(email);
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
            Button buttonChat = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 130),
                BackColor = Color.White,
                Text = "Chat"
            };
            Button buttonGruppen = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 190),
                BackColor = Color.White,
                Text = "Gruppen"
            };
            Button buttonSuchen = new Button()
            {
                Size = new Size(215, 60),
                Location = new Point(10, 250),
                BackColor = Color.White,
                Text = "Suchen"
            };
            menuPanel.Controls.Add(buttonBeitraege);
            menuPanel.Controls.Add(buttonBeliebt);
            menuPanel.Controls.Add(buttonChat);
            menuPanel.Controls.Add(buttonGruppen);
            menuPanel.Controls.Add(buttonSuchen);

            zeigeInhalte();
        }
        private void zeigeInhalte() 
        {
            inhaltAnzeige.Enabled = true;
            inhaltAnzeige.Visible = true;
            Inhalte inhalt = new Inhalte();
            inhaltAnzeige.Controls.Add(inhalt);
            Inhalte inhalt2 = new Inhalte();
            inhaltAnzeige.Controls.Add(inhalt2);
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
                client.anmelden(tbNutzername.Text, tbPasswort.Text);
                panel.Hide();
                zeigeProgram();
            }
            if(registerToggle) 
            {
                NutzerRegistrieren();
            
            }
        }
        private void registrieren_Click(object sender, EventArgs e) 
        {
            if(!registerToggle) 
            {
                tbNutzername.Text = "Nutzername...";
                tbPasswort.Text = "Passwort festlegen...";
                registrieren.Text = "Anmelden";
                anmeldeButton.Text = "Registrieren";
                email.Visible = true;
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
            }
            
        }

        private void NutzerRegistrieren() 
        {
            client.registrieren(tbNutzername.Text, tbPasswort.Text, email.Text);
        }
    }
}
