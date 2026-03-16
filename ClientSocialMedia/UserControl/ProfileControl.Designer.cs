namespace ClientSocialMedia
{
    partial class ProfileControl
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
            this.profileLb = new System.Windows.Forms.Label();
            this.nameTb = new System.Windows.Forms.TextBox();
            this.mailTb = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.abonnentenLb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.passwortPanel = new System.Windows.Forms.Panel();
            this.savePassword = new System.Windows.Forms.Button();
            this.passwortBtn = new System.Windows.Forms.Button();
            this.profilePictureBox = new System.Windows.Forms.PictureBox();
            this.profilePictureBtn = new System.Windows.Forms.Button();
            this.abmeldenBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.loadBeitraegeBtn = new System.Windows.Forms.Button();
            this.beitraegePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.passwortPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // profileLb
            // 
            this.profileLb.AutoSize = true;
            this.profileLb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileLb.Location = new System.Drawing.Point(18, 18);
            this.profileLb.Name = "profileLb";
            this.profileLb.Size = new System.Drawing.Size(80, 25);
            this.profileLb.TabIndex = 0;
            this.profileLb.Text = "Profile";
            // 
            // nameTb
            // 
            this.nameTb.Location = new System.Drawing.Point(199, 109);
            this.nameTb.Name = "nameTb";
            this.nameTb.Size = new System.Drawing.Size(159, 20);
            this.nameTb.TabIndex = 1;
            this.nameTb.Text = "Benutzername...";
            // 
            // mailTb
            // 
            this.mailTb.Location = new System.Drawing.Point(199, 157);
            this.mailTb.Name = "mailTb";
            this.mailTb.ReadOnly = true;
            this.mailTb.Size = new System.Drawing.Size(159, 20);
            this.mailTb.TabIndex = 2;
            this.mailTb.Text = "E-Mail...";
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.ForeColor = System.Drawing.Color.White;
            this.saveBtn.Location = new System.Drawing.Point(149, 309);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(94, 31);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Speichern";
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // abonnentenLb
            // 
            this.abonnentenLb.AutoSize = true;
            this.abonnentenLb.Location = new System.Drawing.Point(196, 49);
            this.abonnentenLb.Name = "abonnentenLb";
            this.abonnentenLb.Size = new System.Drawing.Size(96, 13);
            this.abonnentenLb.TabIndex = 4;
            this.abonnentenLb.Text = "Abonnentenanzahl";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Aktuelles Passwort:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Neues Passwort:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Passwort Bestätigen:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(131, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(159, 20);
            this.textBox1.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(131, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(159, 20);
            this.textBox2.TabIndex = 9;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(131, 54);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(159, 20);
            this.textBox3.TabIndex = 10;
            // 
            // passwortPanel
            // 
            this.passwortPanel.Controls.Add(this.savePassword);
            this.passwortPanel.Controls.Add(this.label1);
            this.passwortPanel.Controls.Add(this.textBox3);
            this.passwortPanel.Controls.Add(this.label2);
            this.passwortPanel.Controls.Add(this.textBox2);
            this.passwortPanel.Controls.Add(this.label3);
            this.passwortPanel.Controls.Add(this.textBox1);
            this.passwortPanel.Location = new System.Drawing.Point(8, 202);
            this.passwortPanel.Name = "passwortPanel";
            this.passwortPanel.Size = new System.Drawing.Size(394, 85);
            this.passwortPanel.TabIndex = 11;
            this.passwortPanel.Visible = false;
            // 
            // savePassword
            // 
            this.savePassword.Location = new System.Drawing.Point(305, 30);
            this.savePassword.Name = "savePassword";
            this.savePassword.Size = new System.Drawing.Size(75, 23);
            this.savePassword.TabIndex = 11;
            this.savePassword.Text = "Speichern";
            this.savePassword.UseVisualStyleBackColor = true;
            this.savePassword.Click += new System.EventHandler(this.savePassword_Click);
            // 
            // passwortBtn
            // 
            this.passwortBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passwortBtn.Location = new System.Drawing.Point(8, 313);
            this.passwortBtn.Name = "passwortBtn";
            this.passwortBtn.Size = new System.Drawing.Size(103, 23);
            this.passwortBtn.TabIndex = 12;
            this.passwortBtn.Text = "Passwort ändern";
            this.passwortBtn.UseVisualStyleBackColor = true;
            this.passwortBtn.Click += new System.EventHandler(this.passwortBtn_Click);
            // 
            // profilePictureBox
            // 
            this.profilePictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.profilePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profilePictureBox.Image = global::ClientSocialMedia.Properties.Resources.profile;
            this.profilePictureBox.Location = new System.Drawing.Point(23, 46);
            this.profilePictureBox.Name = "profilePictureBox";
            this.profilePictureBox.Size = new System.Drawing.Size(143, 155);
            this.profilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.profilePictureBox.TabIndex = 13;
            this.profilePictureBox.TabStop = false;
            // 
            // profilePictureBtn
            // 
            this.profilePictureBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.profilePictureBtn.Location = new System.Drawing.Point(23, 202);
            this.profilePictureBtn.Name = "profilePictureBtn";
            this.profilePictureBtn.Size = new System.Drawing.Size(105, 23);
            this.profilePictureBtn.TabIndex = 14;
            this.profilePictureBtn.TabStop = false;
            this.profilePictureBtn.Text = "Profil hinzufügen";
            this.profilePictureBtn.UseVisualStyleBackColor = true;
            this.profilePictureBtn.Click += new System.EventHandler(this.profilePictureBtn_Click);
            // 
            // abmeldenBtn
            // 
            this.abmeldenBtn.Location = new System.Drawing.Point(199, 18);
            this.abmeldenBtn.Margin = new System.Windows.Forms.Padding(2);
            this.abmeldenBtn.Name = "abmeldenBtn";
            this.abmeldenBtn.Size = new System.Drawing.Size(86, 23);
            this.abmeldenBtn.TabIndex = 15;
            this.abmeldenBtn.Text = "Abmelden";
            this.abmeldenBtn.UseVisualStyleBackColor = true;
            this.abmeldenBtn.Click += new System.EventHandler(this.abmeldenBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(304, 18);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 23);
            this.closeBtn.TabIndex = 16;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.loadBeitraegeBtn);
            this.headerPanel.Controls.Add(this.abmeldenBtn);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(400, 350);
            this.headerPanel.TabIndex = 17;
            // 
            // loadBeitraegeBtn
            // 
            this.loadBeitraegeBtn.Location = new System.Drawing.Point(278, 309);
            this.loadBeitraegeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.loadBeitraegeBtn.Name = "loadBeitraegeBtn";
            this.loadBeitraegeBtn.Size = new System.Drawing.Size(100, 27);
            this.loadBeitraegeBtn.TabIndex = 0;
            this.loadBeitraegeBtn.Text = "Eigene Beiträge";
            this.loadBeitraegeBtn.UseVisualStyleBackColor = true;
            this.loadBeitraegeBtn.Click += new System.EventHandler(this.loadBeitraegeBtn_Click);
            // 
            // beitraegePanel
            // 
            this.beitraegePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.beitraegePanel.AutoScroll = true;
            this.beitraegePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.beitraegePanel.Location = new System.Drawing.Point(0, 350);
            this.beitraegePanel.Margin = new System.Windows.Forms.Padding(2);
            this.beitraegePanel.Name = "beitraegePanel";
            this.beitraegePanel.Size = new System.Drawing.Size(402, 48);
            this.beitraegePanel.TabIndex = 18;
            this.beitraegePanel.WrapContents = false;
            // 
            // ProfileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.beitraegePanel);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.profilePictureBtn);
            this.Controls.Add(this.profilePictureBox);
            this.Controls.Add(this.passwortBtn);
            this.Controls.Add(this.passwortPanel);
            this.Controls.Add(this.abonnentenLb);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.mailTb);
            this.Controls.Add(this.nameTb);
            this.Controls.Add(this.profileLb);
            this.Controls.Add(this.headerPanel);
            this.Name = "ProfileControl";
            this.Size = new System.Drawing.Size(400, 398);
            this.passwortPanel.ResumeLayout(false);
            this.passwortPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label profileLb;
        private System.Windows.Forms.TextBox nameTb;
        private System.Windows.Forms.TextBox mailTb;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label abonnentenLb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel passwortPanel;
        private System.Windows.Forms.Button savePassword;
        private System.Windows.Forms.Button passwortBtn;
        private System.Windows.Forms.PictureBox profilePictureBox;
        private System.Windows.Forms.Button profilePictureBtn;
        private System.Windows.Forms.Button abmeldenBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.FlowLayoutPanel beitraegePanel;
        private System.Windows.Forms.Button loadBeitraegeBtn;
    }
}
