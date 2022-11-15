namespace ModernGUITest
{
    partial class Example
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
            this.spinner1 = new ModernGUITest.Spinner();
            this.SuspendLayout();
            // 
            // spinner1
            // 
            this.spinner1.Location = new System.Drawing.Point(646, 205);
            this.spinner1.Name = "spinner1";
            this.spinner1.Size = new System.Drawing.Size(16, 28);
            this.spinner1.TabIndex = 0;
            this.spinner1.ButtonClick += new ModernGUITest.Spinner.OnButtonClick(this.spinner1_ButtonClick);
            // 
            // Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 483);
            this.Controls.Add(this.spinner1);
            this.Name = "Example";
            this.Text = "Example";
            this.Load += new System.EventHandler(this.Example_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Spinner spinner1;
    }
}