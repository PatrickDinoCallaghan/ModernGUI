using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms.Integration;
using System.Windows.Media;

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



        public new System.Drawing.Color BackColor
        {
            get
            {
                if (box != null)
                {
                    if (((SolidColorBrush)box.Background)!=null)
                    {
                        return System.Drawing.Color.FromArgb(((SolidColorBrush)box.Background).Color.A,
                  ((SolidColorBrush)box.Background).Color.R,
                  ((SolidColorBrush)box.Background).Color.G,
                  ((SolidColorBrush)box.Background).Color.B);

                    }

                }

                return base.BackColor; 
            }
            set
            {




             System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(value.R, value.G, value.B);
                //(System.Windows.Media.Color)
                box.Background = new SolidColorBrush(color);

              ///  this.BackColor = value;

            }
        }
    }
}
