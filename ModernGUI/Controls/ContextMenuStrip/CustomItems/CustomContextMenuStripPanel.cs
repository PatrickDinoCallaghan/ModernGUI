using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernGUI.Controls
{
    [ToolboxItem(false)]
    public partial class CustomContextMenuStripPanel : TableLayoutPanel
    {

        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;

        public new string Text { get { return label.Text; } set { label.Text = value;
        
            } }
        public CustomContextMenuStripPanel(Control InControl)
        {
            InitializeComponent();

            this.Controls.Add((Control)InControl, 1, 0);
            InControl.Dock = DockStyle.Fill;
            this.label.ForeColor = Color.AliceBlue;
            this.label.AutoSize = true;
            this.label.Visible = true;
            this.label.TextAlign = ContentAlignment.MiddleLeft;
            this.label.Dock = DockStyle.Fill;
            label.Font = SkinManager.openSans[10, OpenSans.Weight.Regular];
            this.Dock = DockStyle.Fill;
            // MessageBox.Show(label1.Font.ToString() + "\n" + SkinManager.openSans[10, OpenSans.Weight.Medium].ToString());
        }
    }
}
