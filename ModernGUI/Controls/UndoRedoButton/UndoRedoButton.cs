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
    internal class UndoButton : FormHeaderButton
    {
            ToolTip tooltip = new ToolTip();
        public UndoButton() : base(Properties.Resources.Undo)
        {
            tooltip.SetToolTip(this, "Undo");

        }

    }
    [ToolboxItem(false)]
    internal class RedoButton : FormHeaderButton
    {
        ToolTip tooltip = new ToolTip();
        public RedoButton() : base(Properties.Resources.Redo)
        {

            tooltip.SetToolTip(this, "Redo");
        }
    }
}
