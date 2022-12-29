using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms.Design;
using System.Windows.Forms.Integration;
using System.Windows;

namespace ModernGUI.Controls.WPF
{
    [Designer(typeof(ControlDesigner))]
    //[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public partial class WPFTextEditor : ElementHost
    {
        public WPFTextEditor()
        {
            box = new RTFEditor.RTFBox();
            base.Child = box;
            this.Size = new System.Drawing.Size(600, 385);
        }


        /*
        public override string Text
        {
            get { return box.Text; }
            set { box.Text = value; }
        }
        */

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Windows.UIElement Child
        {
            get { return base.Child; }
            set { /* Do nothing to solve a problem with the serializer !! */ }
        }
        private RTFEditor.RTFBox box;
    }
}
