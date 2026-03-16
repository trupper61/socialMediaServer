namespace ClientSocialMedia
{
    partial class ChatItemControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.profilPic = new System.Windows.Forms.PictureBox();
            this.nameLb = new System.Windows.Forms.Label();
            this.lastLb = new System.Windows.Forms.Label();
            this.dateLb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.profilPic)).BeginInit();
            this.SuspendLayout();
            // 
            // profilPic
            // 
            this.profilPic.Location = new System.Drawing.Point(3, 3);
            this.profilPic.Name = "profilPic";
            this.profilPic.Size = new System.Drawing.Size(40, 40);
            this.profilPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilPic.TabIndex = 0;
            this.profilPic.TabStop = false;
            this.profilPic.MouseLeave += new System.EventHandler(this.ChatItem_MouseLeave);
            this.profilPic.MouseHover += new System.EventHandler(this.ChatItem_MouseHover);
            // 
            // nameLb
            // 
            this.nameLb.AutoEllipsis = true;
            this.nameLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLb.Location = new System.Drawing.Point(49, 3);
            this.nameLb.MaximumSize = new System.Drawing.Size(159, 20);
            this.nameLb.Name = "nameLb";
            this.nameLb.Size = new System.Drawing.Size(159, 20);
            this.nameLb.TabIndex = 1;
            this.nameLb.Text = "BenutzerName";
            this.nameLb.MouseLeave += new System.EventHandler(this.ChatItem_MouseLeave);
            this.nameLb.MouseHover += new System.EventHandler(this.ChatItem_MouseHover);
            // 
            // lastLb
            // 
            this.lastLb.AutoEllipsis = true;
            this.lastLb.Location = new System.Drawing.Point(50, 32);
            this.lastLb.MaximumSize = new System.Drawing.Size(260, 20);
            this.lastLb.Name = "lastLb";
            this.lastLb.Size = new System.Drawing.Size(260, 20);
            this.lastLb.TabIndex = 2;
            this.lastLb.Text = "Letzte Nachricht...";
            // 
            // dateLb
            // 
            this.dateLb.AutoSize = true;
            this.dateLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLb.ForeColor = System.Drawing.Color.Gray;
            this.dateLb.Location = new System.Drawing.Point(214, 11);
            this.dateLb.Name = "dateLb";
            this.dateLb.Size = new System.Drawing.Size(90, 12);
            this.dateLb.TabIndex = 3;
            this.dateLb.Text = "dd/MM/yyyy hh:mm";
            this.dateLb.MouseLeave += new System.EventHandler(this.ChatItem_MouseLeave);
            this.dateLb.MouseHover += new System.EventHandler(this.ChatItem_MouseHover);
            // 
            // ChatItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dateLb);
            this.Controls.Add(this.lastLb);
            this.Controls.Add(this.nameLb);
            this.Controls.Add(this.profilPic);
            this.Name = "ChatItemControl";
            this.Size = new System.Drawing.Size(342, 60);
            this.MouseLeave += new System.EventHandler(this.ChatItem_MouseLeave);
            this.MouseHover += new System.EventHandler(this.ChatItem_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.profilPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox profilPic;
        private System.Windows.Forms.Label nameLb;
        private System.Windows.Forms.Label lastLb;
        private System.Windows.Forms.Label dateLb;
    }
}
