using System.Windows.Forms;

namespace SagiriUI.Controls
{
    /// <summary>
    /// !! WIP - Don't use. !!
    /// </summary>
    public partial class SagiriStatusStrip : StatusStrip
    {
        public SagiriStatusStrip()
        {
            InitializeComponent();
            var button = new Button()
            {
                Size = new(40, 10),
                Text = "Test"
            };
            //var toolStripItemList = new List<ToolStripItem>() { button };
            //this.Items.AddRange();
        }
    }
}
