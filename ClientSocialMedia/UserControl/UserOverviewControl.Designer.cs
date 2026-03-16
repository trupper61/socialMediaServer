namespace ClientSocialMedia
{
    partial class UserOverviewControl
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
            this.nameLb = new System.Windows.Forms.Label();
            this.abonnentenLb = new System.Windows.Forms.Label();
            this.abonnierenBtn = new System.Windows.Forms.Button();
            this.nutzerPb = new System.Windows.Forms.PictureBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.chatBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nutzerPb)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLb
            // 
            this.nameLb.AutoEllipsis = true;
            this.nameLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLb.Location = new System.Drawing.Point(23, 62);
            this.nameLb.MaximumSize = new System.Drawing.Size(161, 24);
            this.nameLb.Name = "nameLb";
            this.nameLb.Size = new System.Drawing.Size(161, 24);
            this.nameLb.TabIndex = 2;
            this.nameLb.Text = "Benutzername";
            // 
            // abonnentenLb
            // 
            this.abonnentenLb.AutoSize = true;
            this.abonnentenLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.abonnentenLb.Location = new System.Drawing.Point(24, 117);
            this.abonnentenLb.Name = "abonnentenLb";
            this.abonnentenLb.Size = new System.Drawing.Size(90, 18);
            this.abonnentenLb.TabIndex = 3;
            this.abonnentenLb.Text = "Abonnenten:";
            // 
            // abonnierenBtn
            // 
            this.abonnierenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abonnierenBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.abonnierenBtn.Location = new System.Drawing.Point(44, 219);
            this.abonnierenBtn.Name = "abonnierenBtn";
            this.abonnierenBtn.Size = new System.Drawing.Size(160, 40);
            this.abonnierenBtn.TabIndex = 4;
            this.abonnierenBtn.Text = "Abonnieren";
            this.abonnierenBtn.UseVisualStyleBackColor = true;
            this.abonnierenBtn.Click += new System.EventHandler(this.abonnierenBtn_Click);
            // 
            // nutzerPb
            // 
            this.nutzerPb.Location = new System.Drawing.Point(190, 62);
            this.nutzerPb.Name = "nutzerPb";
            this.nutzerPb.Size = new System.Drawing.Size(120, 120);
            this.nutzerPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.nutzerPb.TabIndex = 1;
            this.nutzerPb.TabStop = false;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(255, 14);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(55, 23);
            this.closeBtn.TabIndex = 5;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // chatBtn
            // 
            this.chatBtn.Location = new System.Drawing.Point(246, 229);
            this.chatBtn.Name = "chatBtn";
            this.chatBtn.Size = new System.Drawing.Size(75, 23);
            this.chatBtn.TabIndex = 6;
            this.chatBtn.Text = "Chatten!";
            this.chatBtn.UseVisualStyleBackColor = true;
            this.chatBtn.Click += new System.EventHandler(this.chatBtn_Click);
            // 
            // UserOverviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chatBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.abonnierenBtn);
            this.Controls.Add(this.abonnentenLb);
            this.Controls.Add(this.nameLb);
            this.Controls.Add(this.nutzerPb);
            this.MaximumSize = new System.Drawing.Size(330, 300);
            this.Name = "UserOverviewControl";
            this.Size = new System.Drawing.Size(330, 300);
            ((System.ComponentModel.ISupportInitialize)(this.nutzerPb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox nutzerPb;
        private System.Windows.Forms.Label nameLb;
        private System.Windows.Forms.Label abonnentenLb;
        private System.Windows.Forms.Button abonnierenBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button chatBtn;
    }
}
