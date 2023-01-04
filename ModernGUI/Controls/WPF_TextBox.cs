using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Design;
using System.Windows.Forms.Integration;

namespace ModernGUI.Controls.WPF
{
    [Designer(typeof(ControlDesigner))]
    //[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class TextBox : ElementHost
    {
        public TextBox()
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
