using ModernGUI.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace ModernGUI.Controls
{

    public class CustomContextMenuStripItem : ToolStripControlHost, IControl
    {
        //Properties for managing the design properties
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }


        internal AnimationManager AnimationManager;
        internal Point AnimationSource;

        public new string Text { get { return BaseControl.Text; } set { BaseControl.Text = value; } }

        private CustomContextMenuStripPanel BaseControl
        {
            get
            {
                return Control as CustomContextMenuStripPanel;
            }
        }

        public CustomContextMenuStripItem(Control _control) : base(new CustomContextMenuStripPanel(_control) )
        {
            AnimationManager = new AnimationManager(false)
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            }; 
            AnimationManager.OnAnimationProgress += sender => Invalidate();

            BaseControl.Paint += _CustomControl_Paint;
            BackColor = SkinManager.GetApplicationBackgroundColor();
            BaseControl.Dock = DockStyle.Fill;
            BaseControl.ForeColor = Color.Red;
        }

        #region Color

        protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
        {
            base.OnParentChanged(oldParent, newParent);


            Parent.Paint += (o, e) =>
            {
                this.Invalidate();
            };

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(SkinManager.GetApplicationBackgroundColor());
        }


        private void _CustomControl_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(SkinManager.GetApplicationBackgroundColor());
            BaseControl.label.ForeColor = SkinManager.GetPrimaryTextColor();
            BaseControl.label.BackColor = SkinManager.GetApplicationBackgroundColor();
            Parent.BackColor = SkinManager.GetApplicationBackgroundColor(); 
        }

        #endregion
    }
}
