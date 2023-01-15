using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace ModernGUI.Controls
{
    public class TabControl : System.Windows.Forms.TabControl, IControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public TabControl()
        {

        }
        //Removes tabs from control so they arn't shown

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode)
            {
                m.Result = (IntPtr)1;
            }
            else
            {
                try
                {
                    base.WndProc(ref m);
                }
                catch { }
            }
        }
    }
}
