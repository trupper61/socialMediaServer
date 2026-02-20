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
            this.beitragBild = new System.Windows.Forms.PictureBox();
            this.next = new System.Windows.Forms.Button();
            this.last = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.beitragBild)).BeginInit();
            this.SuspendLayout();
            // 
            // beitragTitel
            // 
            this.beitragTitel.AutoSize = true;
            this.beitragTitel.Location = new System.Drawing.Point(58, 9);
            this.beitragTitel.Name = "beitragTitel";
            this.beitragTitel.Size = new System.Drawing.Size(27, 13);
            this.beitragTitel.TabIndex = 0;
            this.beitragTitel.Text = "Titel";
            // 
            // beitragBild
            // 
            this.beitragBild.BackgroundImage = global::ClientSocialMedia.Properties.Resources.empty;
            this.beitragBild.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.beitragBild.Location = new System.Drawing.Point(3, 36);
            this.beitragBild.Name = "beitragBild";
            this.beitragBild.Size = new System.Drawing.Size(144, 111);
            this.beitragBild.TabIndex = 1;
            this.beitragBild.TabStop = false;
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(122, 74);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(28, 23);
            this.next.TabIndex = 2;
            this.next.Text = "Nxt";
            this.next.UseVisualStyleBackColor = true;
            // 
            // last
            // 
            this.last.Location = new System.Drawing.Point(0, 74);
            this.last.Name = "last";
            this.last.Size = new System.Drawing.Size(28, 23);
            this.last.TabIndex = 3;
            this.last.Text = "Lst";
            this.last.UseVisualStyleBackColor = true;
            // 
            // Inhalte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.last);
            this.Controls.Add(this.next);
            this.Controls.Add(this.beitragBild);
            this.Controls.Add(this.beitragTitel);
            this.Name = "Inhalte";
            ((System.ComponentModel.ISupportInitialize)(this.beitragBild)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label beitragTitel;
        private System.Windows.Forms.PictureBox beitragBild;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button last;
    }
}
