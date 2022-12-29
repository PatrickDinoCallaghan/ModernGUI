using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using ColorPicker;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Windows.Threading;

namespace RTFEditor
{
    /// <summary>
    /// Interaction logic for "RTFBox.xaml"
    /// </summary>
    public partial class RTFBox 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RTFBox()
        {
            SetRenderFinishedEvent();
            this.InitializeComponent();            
        }

        #region Rendering Finished Event
        public delegate void StartProgressCommand(object sender);

        public StartProgressCommand startProgressEvent;

        private void SetRenderFinishedEvent()
        {
            System.Windows.Application.Current?.Dispatcher?.Invoke(
               DispatcherPriority.ApplicationIdle,
               new Action(() =>
               {
                   startProgressEvent?.Invoke(this);

               }));
        }
        #endregion

        #region Variables

        // unsaved text changes
        private bool dataChanged = false;

        // Content of the RTFBox in txt format
        private string privateText = null; 
        public string text
        {
            get
            {
                TextRange range = new TextRange(RichTextControl.Document.ContentStart, RichTextControl.Document.ContentEnd);
                return range.Text;
            }
            set
            {
                privateText = value;
            }
        }

        public bool ShowFileToolBar
        {
            get { return ToolBarAbove.IsVisible; }
            set
            {
                if (value == true)
                {
                    ToolBarAbove.Visibility = Visibility.Visible;
                }
                else
                {
                    ToolBarAbove.Visibility = Visibility.Collapsed;
                }


            }
        }
        public bool ShowFontToolBar
        {
            get { return ToolBarBelow.IsVisible; }
            set 
            {
                if (value == true) 
                {
                    ToolBarBelow.Visibility = Visibility.Visible;
                }
                else 
                {
                    ToolBarBelow.Visibility = Visibility.Collapsed; 
                }
                 
            
            }
        }


        private int _CursorLineNo = 1; 
        public int CursorLineNo
        {
            get { return _CursorLineNo; }
            set 
            { 
                _CursorLineNo = value;
            }
        }

        private int _CursorColumnNo = 1; 
        public int CursorColumnNo
        {
            get { return _CursorColumnNo; }
            set 
            { 
                _CursorColumnNo = value;
            }
        }

        #endregion

        #region Control Handler

        //
        // After loading the control
        //
        private void RTFEditor_Loaded(object sender, RoutedEventArgs e)
        {

            // Font type and size initialization
            TextRange range = new TextRange(RichTextControl.Selection.Start, RichTextControl.Selection.End);
            Fonttype.SelectedValue = range.GetPropertyValue(FlowDocument.FontFamilyProperty).ToString();
            Fontheight.SelectedValue = range.GetPropertyValue(FlowDocument.FontSizeProperty).ToString();

            // Specify current row and column positions
            CursorLineNo = LineNumber();
            CursorColumnNo = ColumnNumber();           

            RichTextControl.Focus();

        }


        #endregion

        #region ToolBarHandler


        //
        // ToolStripButton Print was pressed
        //
        private void ToolStripButtonPrint_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            // Configure printer dialog box
            PrintDialog dlg = new PrintDialog();
            dlg.PageRangeSelection = PageRangeSelection.AllPages;
            dlg.UserPageRangeEnabled = true;            

            // Show and process save file dialog box results
            if (dlg.ShowDialog() == true)
            {
                //use either one of the below    
                // dlg.PrintVisual(RichTextControl as Visual, "printing as visual");
                dlg.PrintDocument((((IDocumentPaginatorSource)RichTextControl.Document).DocumentPaginator), "printing as paginator");
            }
		}

        //
        // ToolStripButton Strikeout was pressed
        // (cannot be processed by Command in XAML)
        //
        private void ToolStripButtonStrikeout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Paste event handler
            TextRange range = new TextRange(RichTextControl.Selection.Start, RichTextControl.Selection.End);

            TextDecorationCollection tdc = (TextDecorationCollection)RichTextControl.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            if (tdc == null || !tdc.Equals(TextDecorations.Strikethrough))
            {
                tdc = TextDecorations.Strikethrough;

            }
            else
            {
                tdc = new TextDecorationCollection();
            }
            range.ApplyPropertyValue(Inline.TextDecorationsProperty, tdc);
        }

        //
        // ToolStripButton Textcolor was pressed
        //
        private void ToolStripButtonTextcolor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            //colorDialog.Owner = this;
            if ((bool)colorDialog.ShowDialog())
            {
                TextRange range = new TextRange(RichTextControl.Selection.Start, RichTextControl.Selection.End);

                range.ApplyPropertyValue(FlowDocument.ForegroundProperty, new SolidColorBrush(colorDialog.SelectedColor));                
            }
        }

        //
        // ToolStripButton Backgroundcolor was pressed
        //
        private void ToolStripButtonBackcolor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            //colorDialog.Owner = this;
            if ((bool)colorDialog.ShowDialog())
            {
                TextRange range = new TextRange(RichTextControl.Selection.Start, RichTextControl.Selection.End);

                range.ApplyPropertyValue(FlowDocument.BackgroundProperty, new SolidColorBrush(colorDialog.SelectedColor));
            }
        }

        //
        // ToolStripButton Subscript was pressed
        // (can also be done using Command in XAML.)
        // In order for a real subscript to be displayed, the font used must be OpenType:
        // http://msdn.microsoft.com/en-us/library/ms745109.aspx#variants
        // In order to also implement Subscript for all other fonts, the Baseline property can be changed instead)
        //
        private void ToolStripButtonSubscript_Click(object sender, RoutedEventArgs e)
        {
            var currentAlignment = RichTextControl.Selection.GetPropertyValue(Inline.BaselineAlignmentProperty);

            BaselineAlignment newAlignment = ((BaselineAlignment)currentAlignment == BaselineAlignment.Subscript) ? BaselineAlignment.Baseline : BaselineAlignment.Subscript;
            RichTextControl.Selection.ApplyPropertyValue(Inline.BaselineAlignmentProperty, newAlignment);
        }

        //
        // ToolStripButton Superscript was pressed
        // (can also be done with Command in XAML.
        // In order for a real superscript to be displayed, the font used must be OpenType:
        // http://msdn.microsoft.com/en-us/library/ms745109.aspx#variants
        // In order to implement Superscript for all other fonts, the Baseline property can be changed instead)
        //
        private void ToolStripButtonSuperscript_Click(object sender, RoutedEventArgs e)
        { 
	        var currentAlignment = RichTextControl.Selection.GetPropertyValue(Inline.BaselineAlignmentProperty);
    	 
	        BaselineAlignment newAlignment = ((BaselineAlignment)currentAlignment == BaselineAlignment.Superscript) ? BaselineAlignment.Baseline : BaselineAlignment.Superscript;
	        RichTextControl.Selection.ApplyPropertyValue(Inline.BaselineAlignmentProperty, newAlignment);
        }

        //
        // Text size changed by user
        //
        private void Fontheight_DropDownClosed(object sender, EventArgs e)
        {
            string fontHeight = (string)Fontheight.SelectedItem;

            if (fontHeight != null)
            {                
                RichTextControl.Selection.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontSizeProperty, fontHeight);
                RichTextControl.Focus();
            }
        }

        //
        // other font selected by user
        //
        private void Fonttype_DropDownClosed(object sender, EventArgs e)
        {            
            string fontName = (string)Fonttype.SelectedItem;

            if (fontName != null)
            {                
                RichTextControl.Selection.ApplyPropertyValue(System.Windows.Controls.RichTextBox.FontFamilyProperty, fontName);
                RichTextControl.Focus();
            }
        }

        //
        // Adjust alignment
        //
        private void ToolStripButtonAlignLeft_Click(object sender, RoutedEventArgs e)
        {
            if (ToolStripButtonAlignLeft.IsChecked == true)
            {
                ToolStripButtonAlignCenter.IsChecked = false;
                ToolStripButtonAlignRight.IsChecked = false;
            }
        }

        //
        // Alignmentstatus to adjust
        //
        private void ToolStripButtonAlignCenter_Click(object sender, RoutedEventArgs e)
        {
            if (ToolStripButtonAlignCenter.IsChecked == true)
            {
                ToolStripButtonAlignLeft.IsChecked = false;
                ToolStripButtonAlignRight.IsChecked = false;
            }

        }

        //
        // Alignment status to adjust
        //
        private void ToolStripButtonAlignRight_Click(object sender, RoutedEventArgs e)
        {
            if (ToolStripButtonAlignRight.IsChecked == true)
            {
                ToolStripButtonAlignCenter.IsChecked = false;
                ToolStripButtonAlignLeft.IsChecked = false;
            }

        }

        #endregion

        #region RichTextBoxHandler

        //
        // Formatting of the selected text
        //
        private void RichTextControl_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // get selected text
            TextRange selectionRange = new TextRange(RichTextControl.Selection.Start, RichTextControl.Selection.End);
            
            
            if (selectionRange.GetPropertyValue(FontWeightProperty).ToString() == "Bold")
            {
                ToolStripButtonBold.IsChecked = true;
            }
            else
            {
                ToolStripButtonBold.IsChecked = false;
            }

            if (selectionRange.GetPropertyValue(FontStyleProperty).ToString() == "Italic")
            {
                ToolStripButtonItalic.IsChecked = true;
            }
            else
            {
                ToolStripButtonItalic.IsChecked = false;
            }

            if (selectionRange.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline)
            {
                ToolStripButtonUnderline.IsChecked = true;
            }
            else
            {
                ToolStripButtonUnderline.IsChecked = false;
            }

            if (selectionRange.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Strikethrough)
            {
                ToolStripButtonStrikeout.IsChecked = true;
            }
            else
            {
                ToolStripButtonStrikeout.IsChecked = false;
            } 

            if (selectionRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).ToString() == "Left")
            {
                ToolStripButtonAlignLeft.IsChecked = true;
            }

            if (selectionRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).ToString() == "Center")
            {
                ToolStripButtonAlignCenter.IsChecked = true;
            }

            if (selectionRange.GetPropertyValue(FlowDocument.TextAlignmentProperty).ToString() == "Right")
            {
                ToolStripButtonAlignRight.IsChecked = true;
            }
            
            // Superscript Buttons
            try
            {                
                switch ((BaselineAlignment)selectionRange.GetPropertyValue(Inline.BaselineAlignmentProperty))
                {
                    case BaselineAlignment.Subscript:
                        ToolStripButtonSubscript.IsChecked = true;
                        ToolStripButtonSuperscript.IsChecked = false;
                        break;

                    case BaselineAlignment.Superscript:
                        ToolStripButtonSubscript.IsChecked = false;
                        ToolStripButtonSuperscript.IsChecked = true;
                        break;

                    default:
                        ToolStripButtonSubscript.IsChecked = false;
                        ToolStripButtonSuperscript.IsChecked = false;
                        break;
                }
            }
            catch (Exception) 
            {
                ToolStripButtonSubscript.IsChecked = false;
                ToolStripButtonSuperscript.IsChecked = false;
            }                    

            // Get selected font and height and update selection in ComboBoxes
            Fonttype.SelectedValue = selectionRange.GetPropertyValue(FlowDocument.FontFamilyProperty).ToString();
            Fontheight.SelectedValue = selectionRange.GetPropertyValue(FlowDocument.FontSizeProperty).ToString();

            // Output of the line number
            CursorLineNo = LineNumber();

            // Output of the column number
            CursorColumnNo = ColumnNumber(); 
        }

        //
        // text changes made
        //
        private void RichTextControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataChanged = true;
        }

        //
        // Pressing a key creates a new character in the selected font
        //
        private void RichTextControl_KeyDown(object sender, KeyEventArgs e)
        {
            dataChanged = true;

            string fontName = (string)Fonttype.SelectedValue;
            string fontHeight = (string)Fontheight.SelectedValue;
            TextRange range = new TextRange(RichTextControl.Selection.Start, RichTextControl.Selection.End);

            range.ApplyPropertyValue(TextElement.FontFamilyProperty, fontName);
            range.ApplyPropertyValue(TextElement.FontSizeProperty, fontHeight);
        }

        //
        // Evaluate key combinations
        //
        private void RichTextControl_KeyUp(object sender, KeyEventArgs e)
        {
            // Ctrl + B
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.B))
            {
                if (ToolStripButtonBold.IsChecked == true)
                {
                    ToolStripButtonBold.IsChecked = false;
                }
                else
                {
                    ToolStripButtonBold.IsChecked = true;
                }
            }

            // Ctrl + I
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.I))
            {
                if (ToolStripButtonItalic.IsChecked == true)
                {
                    ToolStripButtonItalic.IsChecked = false;
                }
                else
                {
                    ToolStripButtonItalic.IsChecked = true;
                }
            }

            // Ctrl + U
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.U))
            {
                if (ToolStripButtonUnderline.IsChecked == true)
                {
                    ToolStripButtonUnderline.IsChecked = false;
                }
                else
                {
                    ToolStripButtonUnderline.IsChecked = true;
                }
            }

        }

        #endregion

        #region private functions

        //
        // Returns the line number of the current cursor position
        //
        private int LineNumber()
        {
            TextPointer caretLineStart = RichTextControl.CaretPosition.GetLineStartPosition(0);
            TextPointer p = RichTextControl.Document.ContentStart.GetLineStartPosition(0);
            int currentLineNumber = 1;

            // From the beginning of the RTF box content, counting continues forwards until the current cursor position is reached.
            while (true)
            {
                if (caretLineStart.CompareTo(p) < 0)
                {
                    break;
                }
                int result;
                p = p.GetLineStartPosition(1, out result);
                if (result == 0)
                {
                    break;
                }
                currentLineNumber++;
            }
            return currentLineNumber;
        }

        //
        // Returns the column number of the current cursor position
        private int ColumnNumber()
        {
            TextPointer caretPos = RichTextControl.CaretPosition;
            TextPointer p = RichTextControl.CaretPosition.GetLineStartPosition(0);
            int currentColumnNumber = Math.Max(p.GetOffsetToPosition(caretPos) - 1, 0);

            return currentColumnNumber;
        }

        #endregion private Funktionen

        #region public functions


        //
        // Delete all data
        //
        public void Clear()
        {            
            dataChanged = false;            
            RichTextControl.Document.Blocks.Clear();            
        }

        //
        // Set the content of the RichTextBox as RTF
        //
        public void SetRTF(string rtf)
        {
            TextRange range = new TextRange(RichTextControl.Document.ContentStart, RichTextControl.Document.ContentEnd);

            //  Catch Exception for StreamReader and MemoryStream
            try
            {
                using (MemoryStream rtfMemoryStream = new MemoryStream())
                {
                    using (StreamWriter rtfStreamWriter = new StreamWriter(rtfMemoryStream))
                    {
                        rtfStreamWriter.Write(rtf);
                        rtfStreamWriter.Flush();
                        rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                        range.Load(rtfMemoryStream, DataFormats.Rtf);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //
        // Get the content of the RichTextBox as RTF
        //
        public string GetRTF()
        {
            TextRange range = new TextRange(RichTextControl.Document.ContentStart, RichTextControl.Document.ContentEnd);

            //  Catch Exception for StreamReader and MemoryStream
            try
            {
                using (MemoryStream rtfMemoryStream = new MemoryStream())
                {
                    using (StreamWriter rtfStreamWriter = new StreamWriter(rtfMemoryStream))
                    {
                        range.Save(rtfMemoryStream, DataFormats.Rtf);

                        rtfMemoryStream.Flush();
                        rtfMemoryStream.Position = 0;
                        StreamReader sr = new StreamReader(rtfMemoryStream);
                        return sr.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;                
            }
        }

        #endregion

    }
}