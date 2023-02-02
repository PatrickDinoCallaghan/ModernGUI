using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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

        public new bool Enabled { get { return base.Enabled; } set { base.Enabled = value; label.Enabled = value; } }
        public new string Text { get { return label.Text; } set { label.Text = value; } }

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
            this.Dock = DockStyle.Fill;

            this.Padding = Padding.Empty;
            this.Margin = Padding.Empty;
            InControl.Padding = Padding.Empty;
            InControl.Margin = Padding.Empty;

            label.Padding = Padding.Empty;
            label.Margin = Padding.Empty;

            label.Location = new System.Drawing.Point(0, 0);

        }
        [ToolboxItem(false)]
        public class RedrawLabel : System.Windows.Forms.Label
        {
            public SkinManager SkinManager => SkinManager.Instance;


            public RedrawLabel()
            {
                Padding = Padding.Empty;
                Margin = Padding.Empty;

            }

            protected override void OnPaint(PaintEventArgs e)
            {

                var g = e.Graphics;
                g.Clear(SkinManager.GetApplicationBackgroundColor());
                Parent.BackColor = SkinManager.GetApplicationBackgroundColor();
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                var itemRect = this.ClientRectangle;
                var textRect = new Rectangle(-3, itemRect.Y, itemRect.Width, itemRect.Height);
                g.DrawString(
                   Text,
                     SkinManager.openSans[10, OpenSans.Weight.Medium],
                    this.Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush(),
                    textRect,
                    new StringFormat { LineAlignment = StringAlignment.Center });
            }
        }
    }



}
