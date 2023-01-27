namespace ModernGUITest
{
    partial class SingleControlTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.autoCompleteTextBox1 = new ModernGUI.Controls.AutoCompleteTextBox();
            this.SuspendLayout();
            // 
            // autoCompleteTextBox1
            // 
            this.autoCompleteTextBox1.Location = new System.Drawing.Point(134, 117);
            this.autoCompleteTextBox1.Name = "autoCompleteTextBox1";
            this.autoCompleteTextBox1.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.autoCompleteTextBox1.PopupOffset = new System.Drawing.Point(12, 0);
            this.autoCompleteTextBox1.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.autoCompleteTextBox1.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.autoCompleteTextBox1.PopupWidth = 300;
            this.autoCompleteTextBox1.Size = new System.Drawing.Size(237, 23);
            this.autoCompleteTextBox1.TabIndex = 0;
            // 
            // SingleControlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.autoCompleteTextBox1);
            this.Name = "SingleControlTest";
            this.Text = "SingleControlTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private ModernGUI.Controls.AutoCompleteTextBox autoCompleteTextBox1;
    }
}