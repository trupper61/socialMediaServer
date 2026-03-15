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
    public partial class ChatControl : UserControl
    {
        public Action<int> ChatSelected;
        public ChatControl()
        {
            InitializeComponent();
            LoadChat();
        }
        /// <summary>
        /// Lädt Chat übersichten vom Client und zeigt sie an
        /// </summary>
        private void LoadChat()
        {
            chatPanel.Controls.Clear();
            List<Chat> chats = Form1.client.LadeChats();
            if (Form1.connectionLost)
                return;
            foreach (Chat chat in chats)
            {
                ChatItemControl cic = new ChatItemControl(chat);
                cic.Width = chatPanel.Width - 10;
                cic.ChatClicked += ChatClicked;
                chatPanel.Controls.Add(cic);
            }
        }
        /// <summary>
        /// Event, das ausgelöst wird, wenn ein Chat angeklickt wurde, damit die Forms die Ansicht handeln kann.
        /// </summary>
        /// <param name="chatId">Id des Chats</param>
        private void ChatClicked(int chatId)
        {
            ChatSelected?.Invoke(chatId);
        }
    }
}
