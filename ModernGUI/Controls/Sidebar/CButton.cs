using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
