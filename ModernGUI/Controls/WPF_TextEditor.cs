using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms.Integration;

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

        public void Save()
        {
            MessageBox.Show(box.GetRTF());
            
        }

        private RTFEditor.RTFBox box;
    }
}
