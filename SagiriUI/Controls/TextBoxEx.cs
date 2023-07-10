using System.Drawing;
using System.Windows.Forms;

namespace SagiriUI.Controls
{
    public partial class TextBoxEx : TextBox
    {
        public TextBoxEx() => InitializeComponent();
        
        private Color _borderColor;
        public Color BorderColor
        {
            get => this._borderColor;
            set => this._borderColor = value;
        }

        protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);
        
        protected override void WndProc(ref Message m)
        {
            // WM_NCPAINT
            if (m.Msg == 0x85)
            {
                Graphics g = this.Parent.CreateGraphics();

                Rectangle rectangle = new(this.Location, this.Size);
                rectangle.Inflate(1, 1);

                ControlPaint.DrawBorder(g, rectangle, this._borderColor, ButtonBorderStyle.Solid);
            }
            base.WndProc(ref m);
        }
    }
}
