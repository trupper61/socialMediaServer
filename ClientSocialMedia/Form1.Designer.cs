namespace ClientSocialMedia
{
    partial class Form1
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuPanel = new System.Windows.Forms.Panel();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.momentaneAnsicht = new System.Windows.Forms.Panel();
            this.anzeigeFenster = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.Location = new System.Drawing.Point(-1, -1);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(235, 455);
            this.menuPanel.TabIndex = 0;
            // 
            // profilePic
            // 
            this.profilePic.Location = new System.Drawing.Point(685, -1);
            this.profilePic.Name = "profilePic";
            this.profilePic.Size = new System.Drawing.Size(116, 113);
            this.profilePic.TabIndex = 1;
            this.profilePic.TabStop = false;
            // 
            // momentaneAnsicht
            // 
            this.momentaneAnsicht.Location = new System.Drawing.Point(232, -1);
            this.momentaneAnsicht.Name = "momentaneAnsicht";
            this.momentaneAnsicht.Size = new System.Drawing.Size(455, 50);
            this.momentaneAnsicht.TabIndex = 2;
            // 
            // anzeigeFenster
            // 
            this.anzeigeFenster.HideSelection = false;
            this.anzeigeFenster.Location = new System.Drawing.Point(241, 56);
            this.anzeigeFenster.Name = "anzeigeFenster";
            this.anzeigeFenster.Size = new System.Drawing.Size(438, 398);
            this.anzeigeFenster.TabIndex = 3;
            this.anzeigeFenster.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.anzeigeFenster);
            this.Controls.Add(this.momentaneAnsicht);
            this.Controls.Add(this.profilePic);
            this.Controls.Add(this.menuPanel);
            this.Name = "Form1";
            this.Text = "s";
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.PictureBox profilePic;
        private System.Windows.Forms.Panel momentaneAnsicht;
        private System.Windows.Forms.ListView anzeigeFenster;
    }
}

