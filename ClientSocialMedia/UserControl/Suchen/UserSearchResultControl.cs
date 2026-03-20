using socialMedia;
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
using System.Xml.Linq;

namespace ClientSocialMedia
{
    /// <summary>
    /// Control für ein einzelnes Suchergebnis in der Nutzer-Suche
    /// </summary>
    public partial class UserSearchResultControl : UserControl
    {
        private Nutzer nutzer;
        public Action<Nutzer> OnUserClick;
        public UserSearchResultControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Übernimmt die Daten des Nutzers in das Control
        /// </summary>
        /// <param name="n"></param>
        public void Load(Nutzer n)
        {
            this.nutzer = n;
            nameLb.Text = n.BenutzerName;
            byte[] bytes = Convert.FromBase64String(n.ProfilBild);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image img = Image.FromStream(ms);
                profilPic.Image = img;
            }
        }

        private void UserSearchResultControl_Click(object sender, EventArgs e)
        {

            OnUserClick?.Invoke(nutzer);
        }

        private void UserSearchResultControl_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            Cursor = Cursors.Hand;
        }

        private void UserSearchResultControl_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            Cursor = Cursors.Default;
        }
    }
}
