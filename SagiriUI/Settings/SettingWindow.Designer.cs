
namespace SagiriUI
{
    partial class SettingWindow
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ClosePanel = new System.Windows.Forms.Panel();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxEx1 = new SagiriUI.Controls.TextBoxEx();
            this.textBoxPostingFormat = new SagiriUI.Controls.TextBoxEx();
            this.label12 = new System.Windows.Forms.Label();
            this.licenseLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.PreviewLabel = new System.Windows.Forms.Label();
            this.TitlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClosePanel
            // 
            this.ClosePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClosePanel.BackgroundImage = global::SagiriUI.Properties.Resources.close;
            this.ClosePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClosePanel.Location = new System.Drawing.Point(531, 5);
            this.ClosePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ClosePanel.Name = "ClosePanel";
            this.ClosePanel.Size = new System.Drawing.Size(23, 27);
            this.ClosePanel.TabIndex = 15;
            this.toolTip1.SetToolTip(this.ClosePanel, "Close.");
            this.ClosePanel.Click += new System.EventHandler(this.ClosePanel_Click);
            // 
            // TitlePanel
            // 
            this.TitlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitlePanel.AutoSize = true;
            this.TitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(112)))));
            this.TitlePanel.Controls.Add(this.label9);
            this.TitlePanel.Controls.Add(this.ClosePanel);
            this.TitlePanel.Location = new System.Drawing.Point(0, 0);
            this.TitlePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(562, 37);
            this.TitlePanel.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(9, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "SettingWindow";
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.textBoxEx1.BorderColor = System.Drawing.Color.White;
            this.textBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxEx1.Font = new System.Drawing.Font("メイリオ", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxEx1.ForeColor = System.Drawing.Color.White;
            this.textBoxEx1.Location = new System.Drawing.Point(14, 261);
            this.textBoxEx1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxEx1.Multiline = true;
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.Size = new System.Drawing.Size(253, 173);
            this.textBoxEx1.TabIndex = 20;
            this.textBoxEx1.Text = "Available tags:\r\n{Title} : Embed track title\r\n{TrackNum} : Embed track number\r\n{A" +
    "rtist} : Embed artist name\r\n{Album} : Embed album name\r\n";
            // 
            // textBoxPostingFormat
            // 
            this.textBoxPostingFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPostingFormat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.textBoxPostingFormat.BorderColor = System.Drawing.Color.White;
            this.textBoxPostingFormat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPostingFormat.Font = new System.Drawing.Font("メイリオ", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxPostingFormat.ForeColor = System.Drawing.Color.White;
            this.textBoxPostingFormat.Location = new System.Drawing.Point(14, 73);
            this.textBoxPostingFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPostingFormat.Multiline = true;
            this.textBoxPostingFormat.Name = "textBoxPostingFormat";
            this.textBoxPostingFormat.Size = new System.Drawing.Size(530, 151);
            this.textBoxPostingFormat.TabIndex = 21;
            this.textBoxPostingFormat.TextChanged += new System.EventHandler(this.textBoxPostingFormat_TextChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("メイリオ", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(283, 239);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 20);
            this.label12.TabIndex = 23;
            this.label12.Text = "Preview:";
            // 
            // licenseLinkLabel
            // 
            this.licenseLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.licenseLinkLabel.AutoSize = true;
            this.licenseLinkLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.licenseLinkLabel.LinkColor = System.Drawing.Color.Yellow;
            this.licenseLinkLabel.Location = new System.Drawing.Point(14, 443);
            this.licenseLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.licenseLinkLabel.Name = "licenseLinkLabel";
            this.licenseLinkLabel.Size = new System.Drawing.Size(57, 20);
            this.licenseLinkLabel.TabIndex = 24;
            this.licenseLinkLabel.TabStop = true;
            this.licenseLinkLabel.Text = "License";
            this.licenseLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.licenseLinkLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 25;
            this.label1.Text = "Posting format:";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Font = new System.Drawing.Font("メイリオ", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonOk.Location = new System.Drawing.Point(450, 435);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(94, 29);
            this.buttonOk.TabIndex = 26;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // PreviewLabel
            // 
            this.PreviewLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PreviewLabel.ForeColor = System.Drawing.Color.Cyan;
            this.PreviewLabel.Location = new System.Drawing.Point(283, 259);
            this.PreviewLabel.Name = "PreviewLabel";
            this.PreviewLabel.Size = new System.Drawing.Size(253, 173);
            this.PreviewLabel.TabIndex = 27;
            this.PreviewLabel.Text = "preview area";
            // 
            // SettingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(561, 472);
            this.Controls.Add(this.PreviewLabel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.licenseLinkLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxPostingFormat);
            this.Controls.Add(this.textBoxEx1);
            this.Controls.Add(this.TitlePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "SettingWindow";
            this.Load += new System.EventHandler(this.SettingWindow_Load);
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel ClosePanel;
        private Controls.TextBoxEx textBoxEx1;
        private Controls.TextBoxEx textBoxPostingFormat;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel licenseLinkLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label PreviewLabel;
    }
}