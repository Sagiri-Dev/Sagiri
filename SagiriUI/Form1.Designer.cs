
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
            this.pictureBoxAlbumArt = new System.Windows.Forms.PictureBox();
            this.SeparatePanel = new System.Windows.Forms.Panel();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.AccountPanel = new System.Windows.Forms.Panel();
            this.NowPlayingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ClosePanel = new System.Windows.Forms.Panel();
            this.BorderPanel = new SagiriUI.Controls.BorderPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlbumArt)).BeginInit();
            this.TitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxAlbumArt
            // 
            this.pictureBoxAlbumArt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxAlbumArt.Location = new System.Drawing.Point(0, 28);
            this.pictureBoxAlbumArt.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxAlbumArt.Name = "pictureBoxAlbumArt";
            this.pictureBoxAlbumArt.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxAlbumArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAlbumArt.TabIndex = 8;
            this.pictureBoxAlbumArt.TabStop = false;
            // 
            // SeparatePanel
            // 
            this.SeparatePanel.BackColor = System.Drawing.Color.White;
            this.SeparatePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SeparatePanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.SeparatePanel.ForeColor = System.Drawing.Color.White;
            this.SeparatePanel.Location = new System.Drawing.Point(50, 7);
            this.SeparatePanel.Margin = new System.Windows.Forms.Padding(4);
            this.SeparatePanel.Name = "SeparatePanel";
            this.SeparatePanel.Size = new System.Drawing.Size(1, 16);
            this.SeparatePanel.TabIndex = 1;
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.TitlePanel.Controls.Add(this.InfoPanel);
            this.TitlePanel.Controls.Add(this.AccountPanel);
            this.TitlePanel.Controls.Add(this.NowPlayingPanel);
            this.TitlePanel.Controls.Add(this.label1);
            this.TitlePanel.Controls.Add(this.ClosePanel);
            this.TitlePanel.Controls.Add(this.SeparatePanel);
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(200, 28);
            this.TitlePanel.TabIndex = 14;
            this.TitlePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitlePanel_MouseDown);
            this.TitlePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitlePanel_MouseMove);
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackgroundImage = global::SagiriUI.Properties.Resources.info;
            this.InfoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InfoPanel.Location = new System.Drawing.Point(151, 5);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(18, 18);
            this.InfoPanel.TabIndex = 18;
            this.toolTip1.SetToolTip(this.InfoPanel, "NowPlaying Info.");
            this.InfoPanel.Click += new System.EventHandler(this.InfoPanel_Click);
            // 
            // AccountPanel
            // 
            this.AccountPanel.BackgroundImage = global::SagiriUI.Properties.Resources.account;
            this.AccountPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AccountPanel.Location = new System.Drawing.Point(59, 5);
            this.AccountPanel.Name = "AccountPanel";
            this.AccountPanel.Size = new System.Drawing.Size(18, 18);
            this.AccountPanel.TabIndex = 17;
            this.toolTip1.SetToolTip(this.AccountPanel, "Account for Spotify.");
            this.AccountPanel.Click += new System.EventHandler(this.AccountPanel_Click);
            // 
            // NowPlayingPanel
            // 
            this.NowPlayingPanel.BackgroundImage = global::SagiriUI.Properties.Resources.send;
            this.NowPlayingPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.NowPlayingPanel.Location = new System.Drawing.Point(84, 5);
            this.NowPlayingPanel.Name = "NowPlayingPanel";
            this.NowPlayingPanel.Size = new System.Drawing.Size(18, 18);
            this.NowPlayingPanel.TabIndex = 16;
            this.toolTip1.SetToolTip(this.NowPlayingPanel, "Post with Twitter.");
            this.NowPlayingPanel.Click += new System.EventHandler(this.NowPlayingPanel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Sagiri";
            // 
            // ClosePanel
            // 
            this.ClosePanel.BackgroundImage = global::SagiriUI.Properties.Resources.close;
            this.ClosePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClosePanel.Location = new System.Drawing.Point(175, 4);
            this.ClosePanel.Name = "ClosePanel";
            this.ClosePanel.Size = new System.Drawing.Size(20, 20);
            this.ClosePanel.TabIndex = 15;
            this.toolTip1.SetToolTip(this.ClosePanel, "Close App.");
            this.ClosePanel.Click += new System.EventHandler(this.ClosePanel_Click);
            // 
            // BorderPanel
            // 
            this.BorderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.BorderPanel.Location = new System.Drawing.Point(0, 0);
            this.BorderPanel.Name = "BorderPanel";
            this.BorderPanel.Size = new System.Drawing.Size(200, 228);
            this.BorderPanel.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(200, 228);
            this.Controls.Add(this.TitlePanel);
            this.Controls.Add(this.pictureBoxAlbumArt);
            this.Controls.Add(this.BorderPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Spotify NowPlaying";
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
        private System.Windows.Forms.Panel NowPlayingPanel;
        private SagiriUI.Controls.BorderPanel BorderPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Panel AccountPanel;
    }
}

