namespace ClientSocialMedia
{
    partial class Inhalte
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.beitragTitel = new System.Windows.Forms.Label();
            this.next = new System.Windows.Forms.Button();
            this.last = new System.Windows.Forms.Button();
            this.kommentareVorschau = new System.Windows.Forms.FlowLayoutPanel();
            this.Kommentarsektion = new System.Windows.Forms.Label();
            this.likeBtn = new System.Windows.Forms.Button();
            this.likesLb = new System.Windows.Forms.Label();
            this.anzeigen = new System.Windows.Forms.Button();
            this.tag = new System.Windows.Forms.Label();
            this.beitragBild = new System.Windows.Forms.PictureBox();
            this.profilePicPb = new System.Windows.Forms.PictureBox();
            this.nutzerNameLb = new System.Windows.Forms.Label();
            this.timeLb = new System.Windows.Forms.Label();
            this.beitragText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.beitragBild)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePicPb)).BeginInit();
            this.SuspendLayout();
            // 
            // beitragTitel
            // 
            this.beitragTitel.AutoEllipsis = true;
            this.beitragTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.beitragTitel.Location = new System.Drawing.Point(0, 2);
            this.beitragTitel.MaximumSize = new System.Drawing.Size(436, 25);
            this.beitragTitel.Name = "beitragTitel";
            this.beitragTitel.Size = new System.Drawing.Size(436, 25);
            this.beitragTitel.TabIndex = 0;
            this.beitragTitel.Text = "Titel";
            this.beitragTitel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(319, 117);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(28, 23);
            this.next.TabIndex = 2;
            this.next.Text = "Nxt";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // last
            // 
            this.last.Location = new System.Drawing.Point(98, 117);
            this.last.Name = "last";
            this.last.Size = new System.Drawing.Size(28, 23);
            this.last.TabIndex = 3;
            this.last.Text = "Lst";
            this.last.UseVisualStyleBackColor = true;
            this.last.Visible = false;
            this.last.Click += new System.EventHandler(this.last_Click);
            // 
            // kommentareVorschau
            // 
            this.kommentareVorschau.AutoScroll = true;
            this.kommentareVorschau.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.kommentareVorschau.Location = new System.Drawing.Point(3, 472);
            this.kommentareVorschau.Name = "kommentareVorschau";
            this.kommentareVorschau.Size = new System.Drawing.Size(433, 186);
            this.kommentareVorschau.TabIndex = 4;
            this.kommentareVorschau.WrapContents = false;
            // 
            // Kommentarsektion
            // 
            this.Kommentarsektion.AutoSize = true;
            this.Kommentarsektion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kommentarsektion.Location = new System.Drawing.Point(3, 436);
            this.Kommentarsektion.Name = "Kommentarsektion";
            this.Kommentarsektion.Size = new System.Drawing.Size(133, 25);
            this.Kommentarsektion.TabIndex = 5;
            this.Kommentarsektion.Text = "Kommentare";
            // 
            // likeBtn
            // 
            this.likeBtn.BackColor = System.Drawing.Color.White;
            this.likeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.likeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.likeBtn.Location = new System.Drawing.Point(342, 51);
            this.likeBtn.Name = "likeBtn";
            this.likeBtn.Size = new System.Drawing.Size(34, 35);
            this.likeBtn.TabIndex = 6;
            this.likeBtn.Text = "👍";
            this.likeBtn.UseVisualStyleBackColor = false;
            this.likeBtn.Click += new System.EventHandler(this.likeBtn_Click);
            // 
            // likesLb
            // 
            this.likesLb.AutoSize = true;
            this.likesLb.Location = new System.Drawing.Point(342, 89);
            this.likesLb.Name = "likesLb";
            this.likesLb.Size = new System.Drawing.Size(70, 13);
            this.likesLb.TabIndex = 7;
            this.likesLb.Text = "Anzahl Likes:";
            // 
            // anzeigen
            // 
            this.anzeigen.Location = new System.Drawing.Point(156, 436);
            this.anzeigen.Name = "anzeigen";
            this.anzeigen.Size = new System.Drawing.Size(97, 23);
            this.anzeigen.TabIndex = 9;
            this.anzeigen.Text = "Alle Anzeigen";
            this.anzeigen.UseVisualStyleBackColor = true;
            this.anzeigen.Click += new System.EventHandler(this.anzeigen_Click);
            // 
            // tag
            // 
            this.tag.AutoSize = true;
            this.tag.Location = new System.Drawing.Point(354, 216);
            this.tag.Name = "tag";
            this.tag.Size = new System.Drawing.Size(22, 13);
            this.tag.TabIndex = 10;
            this.tag.Text = "tag";
            // 
            // beitragBild
            // 
            this.beitragBild.BackgroundImage = global::ClientSocialMedia.Properties.Resources.empty;
            this.beitragBild.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.beitragBild.Location = new System.Drawing.Point(112, 30);
            this.beitragBild.Name = "beitragBild";
            this.beitragBild.Size = new System.Drawing.Size(224, 199);
            this.beitragBild.TabIndex = 1;
            this.beitragBild.TabStop = false;
            this.beitragBild.Click += new System.EventHandler(this.beitragBild_Click);
            this.beitragBild.MouseLeave += new System.EventHandler(this.beitragBild_MouseLeave);
            this.beitragBild.MouseHover += new System.EventHandler(this.beitragBild_MouseHover);
            // 
            // profilePicPb
            // 
            this.profilePicPb.Image = global::ClientSocialMedia.Properties.Resources.profile;
            this.profilePicPb.Location = new System.Drawing.Point(16, 209);
            this.profilePicPb.Name = "profilePicPb";
            this.profilePicPb.Size = new System.Drawing.Size(50, 50);
            this.profilePicPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilePicPb.TabIndex = 10;
            this.profilePicPb.TabStop = false;
            this.profilePicPb.Click += new System.EventHandler(this.profilePicPb_Click);
            this.profilePicPb.MouseHover += new System.EventHandler(this.profilePicPb_MouseHover);
            // 
            // nutzerNameLb
            // 
            this.nutzerNameLb.AutoEllipsis = true;
            this.nutzerNameLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nutzerNameLb.Location = new System.Drawing.Point(72, 241);
            this.nutzerNameLb.MaximumSize = new System.Drawing.Size(180, 20);
            this.nutzerNameLb.Name = "nutzerNameLb";
            this.nutzerNameLb.Size = new System.Drawing.Size(180, 18);
            this.nutzerNameLb.TabIndex = 11;
            this.nutzerNameLb.Text = "Nutzername";
            // 
            // timeLb
            // 
            this.timeLb.AutoSize = true;
            this.timeLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLb.Location = new System.Drawing.Point(272, 246);
            this.timeLb.Name = "timeLb";
            this.timeLb.Size = new System.Drawing.Size(140, 12);
            this.timeLb.TabIndex = 12;
            this.timeLb.Text = "Erstellt am dd/MM/yyyy HH:mm";
            // 
            // beitragText
            // 
            this.beitragText.Location = new System.Drawing.Point(16, 276);
            this.beitragText.Multiline = true;
            this.beitragText.Name = "beitragText";
            this.beitragText.ReadOnly = true;
            this.beitragText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.beitragText.Size = new System.Drawing.Size(396, 154);
            this.beitragText.TabIndex = 13;
            // 
            // Inhalte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.beitragText);
            this.Controls.Add(this.tag);
            this.Controls.Add(this.timeLb);
            this.Controls.Add(this.nutzerNameLb);
            this.Controls.Add(this.profilePicPb);
            this.Controls.Add(this.anzeigen);
            this.Controls.Add(this.likesLb);
            this.Controls.Add(this.likeBtn);
            this.Controls.Add(this.Kommentarsektion);
            this.Controls.Add(this.kommentareVorschau);
            this.Controls.Add(this.last);
            this.Controls.Add(this.next);
            this.Controls.Add(this.beitragBild);
            this.Controls.Add(this.beitragTitel);
            this.Name = "Inhalte";
            this.Size = new System.Drawing.Size(439, 663);
            ((System.ComponentModel.ISupportInitialize)(this.beitragBild)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePicPb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label beitragTitel;
        private System.Windows.Forms.PictureBox beitragBild;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button last;
        private System.Windows.Forms.FlowLayoutPanel kommentareVorschau;
        private System.Windows.Forms.Label Kommentarsektion;
        private System.Windows.Forms.Button likeBtn;
        private System.Windows.Forms.Label likesLb;
        private System.Windows.Forms.Button anzeigen;
        private System.Windows.Forms.Label tag;
        private System.Windows.Forms.PictureBox profilePicPb;
        private System.Windows.Forms.Label nutzerNameLb;
        private System.Windows.Forms.Label timeLb;
        private System.Windows.Forms.TextBox beitragText;
    }
}
