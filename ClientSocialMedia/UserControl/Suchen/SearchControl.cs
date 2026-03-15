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
    /// <summary>
    /// Control zum Suchen von Nutzern.
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Wird aufgerufen, wenn sich der Text in der Suchbox ändert.
        /// Startet automatisch eine Nutzersuche
        /// </summary>
        private async void searchTb_TextChanged(object sender, EventArgs e)
        {
            overviewPanel.Visible = false;
            resultPanel.Visible = true;
            if (searchTb.Text.Length < 2)
            {
                resultPanel.Controls.Clear();
                return;
            }
            await Suche(searchTb.Text);
        }
        /// <summary>
        /// Führt die Nutzersuche auf dem Server aus und erstellt Controls jeden gefundenen Nutzer
        /// </summary>
        private async Task Suche(string name)
        {
            resultPanel.Controls.Clear();

            List<Nutzer> nutzer = await Task.Run(() => Form1.client.SucheNutzer(name));
            if (Form1.connectionLost)
                return;
            foreach (Nutzer n in nutzer)
            {
                UserSearchResultControl u = new UserSearchResultControl();
                u.Load(n);
                u.OnUserClick += OpenUserOverview;

                resultPanel.Controls.Add(u);
            }

        }
        /// <summary>
        /// Wird aufgerufen wenn die Nutzerübersicht geschlossen wird. Zeigt dann wieder die Suchergebnisse an
        /// </summary>
        private async void OnUserOvererviewClose()
        {
            overviewPanel.Visible = false;
            resultPanel.Visible = true;
            if (searchTb.Text.Length < 3)
                return;
            await Suche(searchTb.Text);
        }
        /// <summary>
        /// Öffnet die Detailansicht eines Nutzers.
        /// </summary>
        public void OpenUserOverview(Nutzer n)
        {
            resultPanel.Visible = false;
            overviewPanel.Controls.Clear();
            overviewPanel.Visible = true;
            UserOverviewControl u = new UserOverviewControl();
            u.OnClose += OnUserOvererviewClose;
            u.OnChatCreated += ShowChat;   
            u.LadeNutzer(n);
            overviewPanel.Controls.Add(u);
        }
        private void ShowChat(ChatOverviewControl coc)
        {
            this.Controls.Clear();
            this.Controls.Add(coc);
        }
    }
}
