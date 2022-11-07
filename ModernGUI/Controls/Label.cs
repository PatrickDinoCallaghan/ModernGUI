using System.ComponentModel;
using System.Windows.Forms;

namespace ModernGUI.Controls
{
    public class Label : System.Windows.Forms.Label, IControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            ForeColor = SkinManager.GetPrimaryTextColor();
            Font = SkinManager.OpenSans_REGULAR_11;

            BackColorChanged += (sender, args) => ForeColor = SkinManager.GetPrimaryTextColor();
        }
    }
}
