
namespace SagiriUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
		private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBoxAlbumArt = new System.Windows.Forms.PictureBox();
            this.SeparatePanel = new System.Windows.Forms.Panel();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.MisskeyPostPanel = new System.Windows.Forms.Panel();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.AccountPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ClosePanel = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbumArt)).BeginInit();
            this.TitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxAlbumArt
            // 
            this.pictureBoxAlbumArt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxAlbumArt.Location = new System.Drawing.Point(0, 37);
            this.pictureBoxAlbumArt.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBoxAlbumArt.Name = "pictureBoxAlbumArt";
            this.pictureBoxAlbumArt.Size = new System.Drawing.Size(229, 229);
            this.pictureBoxAlbumArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAlbumArt.TabIndex = 8;
            this.pictureBoxAlbumArt.TabStop = false;
            // 
            // SeparatePanel
            // 
            this.SeparatePanel.BackColor = System.Drawing.Color.White;
            this.SeparatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SeparatePanel.ForeColor = System.Drawing.Color.White;
            this.SeparatePanel.Location = new System.Drawing.Point(57, 9);
            this.SeparatePanel.Margin = new System.Windows.Forms.Padding(5);
            this.SeparatePanel.Name = "SeparatePanel";
            this.SeparatePanel.Size = new System.Drawing.Size(1, 21);
            this.SeparatePanel.TabIndex = 1;
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.TitlePanel.Controls.Add(this.MisskeyPostPanel);
            this.TitlePanel.Controls.Add(this.InfoPanel);
            this.TitlePanel.Controls.Add(this.AccountPanel);
            this.TitlePanel.Controls.Add(this.label1);
            this.TitlePanel.Controls.Add(this.ClosePanel);
            this.TitlePanel.Controls.Add(this.SeparatePanel);
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(229, 37);
            this.TitlePanel.TabIndex = 14;
            this.TitlePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitlePanel_MouseDown);
            this.TitlePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitlePanel_MouseMove);
            // 
            // MisskeyPostPanel
            // 
            this.MisskeyPostPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MisskeyPostPanel.BackgroundImage")));
            this.MisskeyPostPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MisskeyPostPanel.Location = new System.Drawing.Point(94, 7);
            this.MisskeyPostPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MisskeyPostPanel.Name = "MisskeyPostPanel";
            this.MisskeyPostPanel.Size = new System.Drawing.Size(21, 24);
            this.MisskeyPostPanel.TabIndex = 17;
            this.toolTip1.SetToolTip(this.MisskeyPostPanel, "Post with Misskey.");
            this.MisskeyPostPanel.Click += new System.EventHandler(this.MisskeyPostPanel_Click);
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackgroundImage = global::SagiriUI.Properties.Resources.info;
            this.InfoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InfoPanel.Location = new System.Drawing.Point(173, 7);
            this.InfoPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(21, 24);
            this.InfoPanel.TabIndex = 18;
            this.toolTip1.SetToolTip(this.InfoPanel, "NowPlaying Info.");
            this.InfoPanel.Click += new System.EventHandler(this.InfoPanel_Click);
            // 
            // AccountPanel
            // 
            this.AccountPanel.BackgroundImage = global::SagiriUI.Properties.Resources.account;
            this.AccountPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AccountPanel.Location = new System.Drawing.Point(67, 7);
            this.AccountPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AccountPanel.Name = "AccountPanel";
            this.AccountPanel.Size = new System.Drawing.Size(21, 24);
            this.AccountPanel.TabIndex = 17;
            this.toolTip1.SetToolTip(this.AccountPanel, "Account for Spotify.");
            this.AccountPanel.Click += new System.EventHandler(this.AccountPanel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Sagiri";
            // 
            // ClosePanel
            // 
            this.ClosePanel.BackgroundImage = global::SagiriUI.Properties.Resources.close;
            this.ClosePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClosePanel.Location = new System.Drawing.Point(200, 5);
            this.ClosePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ClosePanel.Name = "ClosePanel";
            this.ClosePanel.Size = new System.Drawing.Size(23, 27);
            this.ClosePanel.TabIndex = 15;
            this.toolTip1.SetToolTip(this.ClosePanel, "Close App.");
            this.ClosePanel.Click += new System.EventHandler(this.ClosePanel_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(229, 266);
            this.Controls.Add(this.TitlePanel);
            this.Controls.Add(this.pictureBoxAlbumArt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Spotify NowPlaying";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbumArt)).EndInit();
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxAlbumArt;
        private System.Windows.Forms.Panel SeparatePanel;
        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ClosePanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Panel AccountPanel;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Panel MisskeyPostPanel;
    }
}

