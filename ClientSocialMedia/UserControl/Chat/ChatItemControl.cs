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
    /// <summary>
    /// Einzelne Untereinheit für eine Chatübersicht. Sie zeigt mit welchem Nutzer man einen Chat hat, was die letzte Nachricht war und das Profilbild des Nutzers.
    /// Sie wird aufgelistet in ChatControl
    /// </summary>
    public partial class ChatItemControl : UserControl
    {
        public Action<int> ChatClicked;

        private int chatId;
        public ChatItemControl(Chat chat)
        {
            InitializeComponent();
            chatId = chat.ChatId;
            nameLb.Text = chat.BenutzerName;
            if (chat.LetzteNachricht != null)
            {
                lastLb.Text = $"{chat.LetzteNachricht}";
                dateLb.Text = $"Von: {chat.LetzteZeit.ToString()}";
            }
            else
            {
                lastLb.Text = "Noch keine Nachrichten hinterlassen!";
                dateLb.Visible = false;
            }
            byte[] bytes = Convert.FromBase64String(chat.ProfilBild);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image img = Image.FromStream(ms);
                profilPic.Image = img;
            }
            this.Click += ChatItem_Click;
            foreach(Control c in this.Controls)
            {
                c.Click += ChatItem_Click;
            }
        }
        /// <summary>
        /// Ausgelöstes Event, wenn man einen Chat anklickt. Daraufhin wird ein weiters Event ausgelöst welches die jeweilige Chat-Id weitergibt.
        /// Daraufhin wird der jeweillige Chat angezeigt
        /// </summary>
        private void ChatItem_Click(object sender, EventArgs e)
        {
            ChatClicked?.Invoke(chatId);
        }

        private void ChatItem_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
            this.BackColor = Color.WhiteSmoke;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ChatItem_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.None;
        }
    }
}
