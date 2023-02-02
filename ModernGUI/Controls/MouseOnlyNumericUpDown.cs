using System.ComponentModel;

namespace ModernGUI.Controls
{
    public class MouseOnlyNumericUpDown : NumericUpDown
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int HideCaret(nint hwnd);

        public MouseOnlyNumericUpDown() 
        {
        
            foreach (System.Windows.Forms.Control item in base.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.UpDownBase+UpDownEdit")
                {
                    ((System.Windows.Forms.TextBox)(item)).ReadOnly = true;
                    ((System.Windows.Forms.TextBox)(item)).BackColor = Color.White;
                    
                    break;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (System.Windows.Forms.Control item in base.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.UpDownBase+UpDownEdit")
                {
                    HideCaret(item.Handle);
                    break;
                }
            }
        }
        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);

            foreach (System.Windows.Forms.Control item in base.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.UpDownBase+UpDownEdit")
                {
                     HideCaret(item.Handle);
                    break;
                }
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

        }

    }
}
