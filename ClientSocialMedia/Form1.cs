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
        public static Client client = new Client();
        public Form1()
        {
            InitializeComponent();
            ErstellePanel();
        }

        public void ErstellePanel()
        {
            panel = new Panel();
            panel.Location = new Point(this.Width / 2, this.Height / 2);
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

            Button anmeldeButton = new Button()
            {
                Size = new Size(100, 50),
                Location = new Point(0, anmelden.Location.Y + 60),
                BackColor = Color.White,
                Text = "Anmelden"
            };
            panel.Controls.Add(anmeldeButton);
            anmeldeButton.Click += anmeldeButton_Click;
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

        private void anmeldeButton_Click(object sender, EventArgs e) 
        {
            client.anmelden(tbNutzername.Text, tbPasswort.Text);
            panel.Hide();
            zeigeProgram();
        }
    }
}
