using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Adapters;

namespace ModernGUI.Controls
{
    public partial class Panel : System.Windows.Forms.Panel
    {
        private bool _ReadOnly = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool ReadOnly { get { return _ReadOnly; } set { _ReadOnly = value; MakeChildrenReadonly(this, value); } }


        public Panel()
        {

        }


        private void MakeChildrenReadonly(System.Windows.Forms.Control control, bool readOnly)
        {
            try
            {
                Type type = control.GetType();

                if (type != typeof(ModernGUI.Controls.Panel))
                {
                    if (type != null)
                    {
                        PropertyInfo enProp = type.GetProperty("ReadOnly");

                        if (enProp != null)
                        {
                            enProp.SetValue(control, readOnly, null);
                        }
                    }
                }
                if (type == typeof(ModernGUI.Controls.Checkbox) || type == typeof(System.Windows.Forms.CheckBox))
                {
                    control.Enabled = !readOnly;
                }
                foreach (System.Windows.Forms.Control ctrl in control.Controls)
                {
                    MakeChildrenReadonly(ctrl, readOnly);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Panel was unable to make a child readonly, it caused an error in the ModernGUI.Panel Control. Please revise", ex);
            }

        }


        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            MakeChildrenReadonly(e.Control, _ReadOnly);
        }

    }

}
