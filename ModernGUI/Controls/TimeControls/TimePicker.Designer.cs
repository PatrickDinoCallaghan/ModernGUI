namespace ModernGUI.Controls.TimeControls
{
    partial class TimePicker
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spinner1 = new ModernGUI.Controls.Spinner();
            this.SuspendLayout();
            // 
            // spinner1
            // 
            this.spinner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spinner1.Location = new System.Drawing.Point(96, 0);
            this.spinner1.Name = "spinner1";
            this.spinner1.Size = new System.Drawing.Size(16, 22);
            this.spinner1.TabIndex = 0;
            // 
            // TimePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spinner1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TimePicker";
            this.Size = new System.Drawing.Size(112, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private BaseTextBox _baseTextBox;
        private Spinner spinner1;
    }
}
