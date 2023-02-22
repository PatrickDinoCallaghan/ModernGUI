using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Design;
using System.Windows.Forms.Integration;
using System.Windows.Media;

namespace ModernGUI.Controls.WPF
{
    [Designer(typeof(ControlDesigner))]
 
    public class WPFTextBox : ElementHost
    {
        public WPFTextBox()
        {
            box = new System.Windows.Controls.TextBox();
            base.Child = box;
            box.TextChanged += (s, e) => OnTextChanged(EventArgs.Empty);
            box.SpellCheck.IsEnabled = true;
            box.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.Size = new System.Drawing.Size(100, 20);
        }
        public override string Text
        {
            get { return box.Text; }
            set { box.Text = value; }
        }
        [DefaultValue(false)]
        public bool Multiline
        {
            get { return box.AcceptsReturn; }
            set { box.AcceptsReturn = value; }
        }
        [DefaultValue(false)]
        public bool WordWrap
        {
            get { return box.TextWrapping != TextWrapping.NoWrap; }
            set { box.TextWrapping = value ? TextWrapping.Wrap : TextWrapping.NoWrap; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Windows.UIElement Child
        {
            get { return base.Child; }
            set { /* Do nothing to solve a problem with the serializer !! */ }
        }

        public new System.Drawing.Color BackColor
        {
            get
            {
                if (box != null)
                {
                    if (((SolidColorBrush)box.Background) != null)
                    {
                        return System.Drawing.Color.FromArgb(((SolidColorBrush)box.Background).Color.A,
                  ((SolidColorBrush)box.Background).Color.R,
                  ((SolidColorBrush)box.Background).Color.G,
                  ((SolidColorBrush)box.Background).Color.B);

                    }

                }

                return base.BackColor;
            }
            set {




                 System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(value.R, value.G, value.B);
               box.Background = new SolidColorBrush(color);

              //this.BackColor = value; 
               
            }
        }
        private System.Windows.Controls.TextBox box;

        #region Textbox methods needed

        public BorderStyle BorderStyle
        {
            get;
            set;
        }
        public void Undo() { }
        public void Cut() { }
        public void Copy() { }
        public void Paste() { }
                     
        public bool CanUndo
        { get; set; }

        public string SelectedText
        {
            get;
            set;
        }

        public bool ContainsText()
        {
            return true;
        }

        public int MaxLength
        {
            get; set;
        }

        public int SelectionStart
        {
            get; set;
        }
        public int SelectionLength
        {
            get; set;
        }
        public int TextLength
        {
            get; set;
        }
        public void Clear()
        {

        }

        public char PasswordChar
        {
            get;
            set;
        }

        public new void SelectAll()
        {


        }
        #endregion 
    }
}
