using ModernGUI.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernGUI.Controls
{
    public partial class FieldLabel : Label
    {
        //Properties for managing the design properties
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private readonly AnimationManager _animationManager;


        public FieldLabel()
        {
            base.Font = SkinManager.openSans[11, OpenSans.Weight.Regular];
            base.ForeColor = SkinManager.GetPrimaryTextColor();
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
        }


    }
}
