using System.ComponentModel;

namespace ModernGUI.Controls
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ToolboxItem(false)]
    public class CButton : Button
    {
        public CButton Group { get; set; }

        public ButtonItem ButtonItem { get; set; }

    }
}
