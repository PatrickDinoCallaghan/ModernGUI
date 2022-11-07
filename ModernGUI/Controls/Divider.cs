using System.ComponentModel;
using System.Windows.Forms;

namespace ModernGUI.Controls
{
    public sealed class Divider : Control, IControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public Divider()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Height = 1;
            BackColor = SkinManager.GetDividersColor();
        }
    }
}
