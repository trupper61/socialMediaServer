using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialMedia
{
    public class Chat
    {
        public int ChatId { get; set; }
        public string BenutzerName { get; set; }
        public string ProfilBild { get; set; }
        public string LetzteNachricht { get; set; }
        public DateTime? LetzteZeit { get; set; }
        public Chat(int chatId)
        {
            this.ChatId = chatId;
        }

        public void SetData(string nutzerName, string profilBild, string letzteNachricht, DateTime? letzteZeit)
        {
            BenutzerName = nutzerName;
            ProfilBild = profilBild;
            LetzteNachricht = letzteNachricht;
            LetzteZeit = letzteZeit;
        }
    }
}
