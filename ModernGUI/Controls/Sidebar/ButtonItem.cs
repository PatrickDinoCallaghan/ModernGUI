using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernGUI.Controls
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Category("SubReportParams")]
    [DisplayName("Parameters")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Serializable]
    public class ButtonItem
    {
        public string Text { get; set; } = "Menu Item";

        public Image Icon { get; set; }

        public string[] SubItems { get; set; } = new string[0];

        public bool IsExpanded { get; set; }

        public Image ImageIdle { get; set; }

        public Image ImageActive { get; set; }
    }

}

