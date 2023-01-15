using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms.Integration;

namespace ModernGUI.Controls.WPF
{
    [Designer(typeof(ControlDesigner))]
    public partial class WPFTextEditor : ElementHost
    {

        public WPFTextEditor()
        {
            box = new RTFEditor.RTFBox();
            base.Child = box;
            this.Size = new System.Drawing.Size(600, 385);
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Windows.UIElement Child
        {
            get { return base.Child; }
            set { /* Do nothing to solve a problem with the serializer !! */ }
        }

        public void Save()
        {
            //MessageBox.Show(box.GetRTF());
            MessageBox.Show(BitConverter.ToString(box.Save()).ToString());
        }

        private RTFEditor.RTFBox box;

    }
}
