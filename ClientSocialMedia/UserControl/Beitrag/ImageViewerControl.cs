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
    /// Größere Ansicht der Bilder aus Inhalt
    /// </summary>
    public partial class ImageViewerControl : UserControl
    {
        private List<Image> images;
        private int index = 0;
        public ImageViewerControl(List<Image> bilder, int startIndex)
        {
            InitializeComponent();
            images = bilder;
            index = startIndex;
            ShowImage();
        }

        private void ShowImage()
        {
            picturePB.Image = images[index];
            if (index > 0)
            {
                leftBtn.Visible = true;
            }
            else
            {
                leftBtn.Visible = false;
            }
            if (index < images.Count - 1)
            {
                rightBtn.Visible = true;
            }
            else
            {
                rightBtn.Visible = false;
            }
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            if (index < images.Count - 1)
            {
                index++;
                ShowImage();
            }
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index--;
                ShowImage();
            }

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
    }
}
