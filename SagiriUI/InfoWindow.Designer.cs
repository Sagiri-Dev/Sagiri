
namespace SagiriUI
{
    partial class InfoWindow
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
            this.BorderPanel = new SagiriUI.Controls.BorderPanel();
            this.PreviewUrlLabel = new System.Windows.Forms.Label();
            this.DurationLabel = new System.Windows.Forms.Label();
            this.ReleaseDateLabel = new System.Windows.Forms.Label();
            this.AlbumLabel = new System.Windows.Forms.Label();
            this.ArtistLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ClosePanel = new System.Windows.Forms.Panel();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.BorderPanel.SuspendLayout();
            this.TitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BorderPanel
            // 
            this.BorderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(160)))));
            this.BorderPanel.Controls.Add(this.PreviewUrlLabel);
            this.BorderPanel.Controls.Add(this.DurationLabel);
            this.BorderPanel.Controls.Add(this.ReleaseDateLabel);
            this.BorderPanel.Controls.Add(this.AlbumLabel);
            this.BorderPanel.Controls.Add(this.ArtistLabel);
            this.BorderPanel.Controls.Add(this.TitleLabel);
            this.BorderPanel.Location = new System.Drawing.Point(0, 28);
            this.BorderPanel.Name = "BorderPanel";
            this.BorderPanel.Size = new System.Drawing.Size(300, 130);
            this.BorderPanel.TabIndex = 15;
            // 
            // PreviewUrlLabel
            // 
            this.PreviewUrlLabel.AutoEllipsis = true;
            this.PreviewUrlLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PreviewUrlLabel.ForeColor = System.Drawing.Color.White;
            this.PreviewUrlLabel.Location = new System.Drawing.Point(12, 104);
            this.PreviewUrlLabel.Name = "PreviewUrlLabel";
            this.PreviewUrlLabel.Size = new System.Drawing.Size(280, 15);
            this.PreviewUrlLabel.TabIndex = 5;
            this.PreviewUrlLabel.Text = "PreviewUrl : ";
            // 
            // DurationLabel
            // 
            this.DurationLabel.AutoSize = true;
            this.DurationLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DurationLabel.ForeColor = System.Drawing.Color.White;
            this.DurationLabel.Location = new System.Drawing.Point(12, 86);
            this.DurationLabel.Name = "DurationLabel";
            this.DurationLabel.Size = new System.Drawing.Size(62, 15);
            this.DurationLabel.TabIndex = 4;
            this.DurationLabel.Text = "Duration : ";
            // 
            // ReleaseDateLabel
            // 
            this.ReleaseDateLabel.AutoSize = true;
            this.ReleaseDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ReleaseDateLabel.ForeColor = System.Drawing.Color.White;
            this.ReleaseDateLabel.Location = new System.Drawing.Point(12, 68);
            this.ReleaseDateLabel.Name = "ReleaseDateLabel";
            this.ReleaseDateLabel.Size = new System.Drawing.Size(79, 15);
            this.ReleaseDateLabel.TabIndex = 3;
            this.ReleaseDateLabel.Text = "ReleaseDate : ";
            // 
            // AlbumLabel
            // 
            this.AlbumLabel.AutoEllipsis = true;
            this.AlbumLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AlbumLabel.ForeColor = System.Drawing.Color.White;
            this.AlbumLabel.Location = new System.Drawing.Point(12, 50);
            this.AlbumLabel.Name = "AlbumLabel";
            this.AlbumLabel.Size = new System.Drawing.Size(280, 15);
            this.AlbumLabel.TabIndex = 2;
            this.AlbumLabel.Text = "Album : ";
            // 
            // ArtistLabel
            // 
            this.ArtistLabel.AutoEllipsis = true;
            this.ArtistLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ArtistLabel.ForeColor = System.Drawing.Color.White;
            this.ArtistLabel.Location = new System.Drawing.Point(12, 32);
            this.ArtistLabel.Name = "ArtistLabel";
            this.ArtistLabel.Size = new System.Drawing.Size(280, 15);
            this.ArtistLabel.TabIndex = 1;
            this.ArtistLabel.Text = "Artist : ";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoEllipsis = true;
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(12, 14);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(38, 15);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title : ";
            // 
            // ClosePanel
            // 
            this.ClosePanel.BackgroundImage = global::SagiriUI.Properties.Resources.close;
            this.ClosePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClosePanel.Location = new System.Drawing.Point(275, 4);
            this.ClosePanel.Name = "ClosePanel";
            this.ClosePanel.Size = new System.Drawing.Size(20, 20);
            this.ClosePanel.TabIndex = 15;
            this.toolTip1.SetToolTip(this.ClosePanel, "Close.");
            this.ClosePanel.Click += new System.EventHandler(this.ClosePanel_Click);
            // 
            // TitlePanel
            // 
            this.TitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(160)))));
            this.TitlePanel.Controls.Add(this.label9);
            this.TitlePanel.Controls.Add(this.ClosePanel);
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(300, 28);
            this.TitlePanel.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(8, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "InfoWindow";
            // 
            // InfoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(300, 160);
            this.Controls.Add(this.TitlePanel);
            this.Controls.Add(this.BorderPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "InfoWindow";
            this.Load += new System.EventHandler(this.InfoWindow_Load);
            this.BorderPanel.ResumeLayout(false);
            this.BorderPanel.PerformLayout();
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TitlePanel;
        private SagiriUI.Controls.BorderPanel BorderPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label ArtistLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label PreviewUrlLabel;
        private System.Windows.Forms.Label DurationLabel;
        private System.Windows.Forms.Label ReleaseDateLabel;
        private System.Windows.Forms.Label AlbumLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel ClosePanel;
    }
}