using System;
using System.Drawing;
using System.Windows.Forms;

namespace SagiriUI.Controls
{
    public partial class BorderPanel : Panel
    {
        public BorderPanel() => InitializeComponent();

        private Color _BorderColor { get; set; } = Color.FromArgb(0, 160, 112);// Color.FromArgb(0, 102, 204);

        public Color BorderColor
        {
            get => _BorderColor;
            set => _BorderColor = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int right = ClientRectangle.Right - 1;
            int bottom = ClientRectangle.Bottom - 1;

            Pen pen = new (_BorderColor);
            Graphics g = CreateGraphics();
            g.DrawLine(pen, 0, 0, right, 0);
            g.DrawLine(pen, 0, 0, 0, bottom);
            g.DrawLine(pen, right, 0, right, bottom);
            g.DrawLine(pen, 0, bottom, right, bottom);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.Refresh();
            base.OnSizeChanged(e);
        }
    }
}

