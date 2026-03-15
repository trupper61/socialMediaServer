using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMediaServer
{
    public class Nachricht
    {
        public int ChatId { get; set; }
        public Nutzer Sender { get; set; }
        public string Text { get; set; }
        public DateTime GesendetAm { get; set; }
        public int NachrichtId { get; set; }
        public Nachricht(int chatId, Nutzer nutzer, string text, DateTime gesendetAm, int nachrichtId)
        {
            ChatId = chatId;
            Sender = nutzer;
            Text = text;
            GesendetAm = gesendetAm;
            NachrichtId = nachrichtId;
        }
    }
}
