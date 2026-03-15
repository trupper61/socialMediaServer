namespace ClientSocialMedia
{
    partial class ChatOverviewControl
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
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.sendBtn = new System.Windows.Forms.Button();
            this.messageTb = new System.Windows.Forms.RichTextBox();
            this.messagesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.White;
            this.bottomPanel.Controls.Add(this.sendBtn);
            this.bottomPanel.Controls.Add(this.messageTb);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 304);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(481, 70);
            this.bottomPanel.TabIndex = 0;
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(389, 27);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(64, 23);
            this.sendBtn.TabIndex = 1;
            this.sendBtn.Text = "Senden";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // messageTb
            // 
            this.messageTb.Location = new System.Drawing.Point(36, 6);
            this.messageTb.Name = "messageTb";
            this.messageTb.Size = new System.Drawing.Size(324, 61);
            this.messageTb.TabIndex = 0;
            this.messageTb.Text = "";
            // 
            // messagesPanel
            // 
            this.messagesPanel.AutoScroll = true;
            this.messagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.messagesPanel.Location = new System.Drawing.Point(0, 0);
            this.messagesPanel.Name = "messagesPanel";
            this.messagesPanel.Padding = new System.Windows.Forms.Padding(10);
            this.messagesPanel.Size = new System.Drawing.Size(481, 304);
            this.messagesPanel.TabIndex = 1;
            this.messagesPanel.WrapContents = false;
            // 
            // ChatOverviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.messagesPanel);
            this.Controls.Add(this.bottomPanel);
            this.Name = "ChatOverviewControl";
            this.Size = new System.Drawing.Size(481, 374);
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.FlowLayoutPanel messagesPanel;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.RichTextBox messageTb;
    }
}
