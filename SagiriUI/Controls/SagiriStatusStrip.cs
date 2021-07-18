using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SagiriUI.Controls
{
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
