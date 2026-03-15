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
    public partial class ChatOverviewControl : UserControl
    {
        private int chatId;
        private List<Nachricht> nachrichten;
        private Timer timer;
        private int offset = 0;
        private Button loadOlderBtn;
        public ChatOverviewControl(int chat)
        {
            InitializeComponent();
            this.chatId = chat;
            this.nachrichten = new List<Nachricht>();
            timer = new Timer();
            timer.Interval = 10000;
            timer.Tick += (s, e) => CheckNeueNachrichten();
            loadOlderBtn = new Button()
            {
                Size = new Size(140, 22),
                Text = "Lade ältere Nachrichten"
            };
            loadOlderBtn.Click += loadOlder_Click;
        }

        public async void LoadNachrichten()
        {
            List<Nachricht> letzte = await Task.Run(() => Form1.client.LadeNachrichten(this.chatId, 0));
            if (nachrichten == null)
                return;
            letzte.Reverse();
            nachrichten.AddRange(letzte);
            messagesPanel.Controls.Clear();
            foreach (Nachricht n in letzte)
            {
                MessageControl m = new MessageControl(n);
                m.Margin = new Padding(0, 0, 0, 10);
                messagesPanel.Controls.Add(m);
            }
            offset += nachrichten.Count;
            messagesPanel.ScrollControlIntoView(messagesPanel.Controls[messagesPanel.Controls.Count - 1]);
            messagesPanel.Controls.Add(loadOlderBtn);
            messagesPanel.Controls.SetChildIndex(loadOlderBtn, 0);
            timer.Start();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string text = messageTb.Text.Trim();
            if (string.IsNullOrEmpty(text))
                return;
            Form1.client.SendeNachricht(chatId, text);
            messageTb.Text = "";
            LoadNachrichten();
        }
        
        public async void CheckNeueNachrichten()
        {
            List<Nachricht> neue = await Task.Run(() => Form1.client.LadeNachrichten(chatId, 0));
            if (Form1.connectionLost || neue == null)
                return;
            List<Nachricht> filtered = neue.Where(n => !nachrichten.Any(existing => existing.NachrichtId == n.NachrichtId)).ToList();
            if (filtered.Count == 0)
                return;
            foreach (Nachricht n in filtered)
            {
                MessageControl m = new MessageControl(n);
                m.Margin = new Padding(0, 0, 0, 10);
                messagesPanel.Controls.Add(m);
                nachrichten.Add(n);
            }
            messagesPanel.ScrollControlIntoView(messagesPanel.Controls[messagesPanel.Controls.Count - 1]);
            
        }
        private async void loadOlder_Click(object sender, EventArgs e)
        {
            List<Nachricht> alte = await Task.Run(() => Form1.client.LadeNachrichten(chatId, offset));
            loadOlderBtn.Enabled = false;
            if (alte.Count == 0)
            {
                
                loadOlderBtn.Text = "Keine älteren Nachrichten";
                return;
            }
            alte.Reverse();
            int previous = messagesPanel.VerticalScroll.Value;
            foreach (Nachricht n in alte)
            {
                MessageControl m = new MessageControl(n);
                m.Margin = new Padding(0, 0, 0, 10);
                messagesPanel.Controls.Add(m);
                messagesPanel.Controls.SetChildIndex(m, 0);
                nachrichten.Insert(0, n);
            }
            loadOlderBtn.Enabled = true;
            offset += alte.Count;
            messagesPanel.VerticalScroll.Value = previous;
            messagesPanel.Controls.SetChildIndex(loadOlderBtn, 0);
            
        }
    }
}
