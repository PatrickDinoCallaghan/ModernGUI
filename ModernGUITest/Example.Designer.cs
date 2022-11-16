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
            this.timePicker1 = new ModernGUI.Controls.TimeControls.TimePicker();
            this.SuspendLayout();
            // 
            // timePicker1
            // 
            this.timePicker1.Depth = 0;
            this.timePicker1.Hours = 0;
            this.timePicker1.Location = new System.Drawing.Point(274, 200);
            this.timePicker1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.timePicker1.Milliseconds = 0;
            this.timePicker1.Minutes = 0;
            this.timePicker1.MouseState = ModernGUI.MouseState.HOVER;
            this.timePicker1.Name = "timePicker1";
            this.timePicker1.Seconds = 0;
            this.timePicker1.Size = new System.Drawing.Size(112, 22);
            this.timePicker1.TabIndex = 0;
            this.timePicker1.TabStop = false;
            this.timePicker1.Value = System.TimeSpan.Parse("00:00:00");
            // 
            // Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 483);
            this.Controls.Add(this.timePicker1);
            this.Name = "Example";
            this.Text = "Example";
            this.Load += new System.EventHandler(this.Example_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ModernGUI.Controls.TimeControls.TimePicker timePicker1;
    }
}