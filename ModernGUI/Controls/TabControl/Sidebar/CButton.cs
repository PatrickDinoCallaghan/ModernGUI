using System.ComponentModel;
using System.Windows.Controls;

namespace ModernGUI.Controls
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ToolboxItem(false)]
    public class CButton : System.Windows.Forms.Button
    {
        public CButton Group { get; set; }

        public ButtonItem ButtonItem { get; set; }

        private DockStyle  _Dock = DockStyle.Top;
        public new DockStyle Dock { get { return _Dock; } set { _Dock = value; base.Dock = value; } }
    }
}
