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
    public partial class Inhalte : UserControl
    {
        public List<string> pictures;
        public string titel;
        public List<Image> anzeigeBilder = new List<Image>();
        public Inhalte(List<string> pictures, string titel)
        {
            InitializeComponent();
            this.pictures = pictures;
            this.titel = titel;
            setDaten(titel, pictures);
        }

        public void setDaten(string titel, List<string> bilder) 
        {
            this.beitragTitel.Text = titel;
            konvertiereBilder(bilder);
            this.beitragBild.BackgroundImage = anzeigeBilder[0];
        }

        public void konvertiereBilder(List<string> bilder)
        {
            foreach(string str in bilder)
            {
                string[] newData = str.Split('|');
                byte[] imageBytes = Convert.FromBase64String(newData[1]);
                //MemoryStream ms = new MemoryStream();
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(imageBytes));
                foreach (var b in imageBytes) 
                {
                    
                    //ms.WriteByte(b);
                    //ms.Position = 0;
                    //Image img = Image.FromStream(ms);
                    
                }
                anzeigeBilder.Add(x);

            }
        }
    }
}
