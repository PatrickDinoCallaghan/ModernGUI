namespace ModernGUI.Controls
{
    partial class CustomContextMenuStripPanel
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
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // label1
            // 
            this.label.AutoSize = false;
            this.label.Font = SkinManager.openSans[10, OpenSans.Weight.Medium];
            this.label.ForeColor = Color.Red;
            this.label.Location = new System.Drawing.Point(3, 0);
            this.label.Name = "label1";
            this.label.TabIndex = 0;
            this.label.Text = "";
            this.label.Visible = false;

            // 
            // CustomContextMenuStripPanel
            //
            this.Controls.Add(this.label, 0, 0);            
            this.AutoSize = true;
            this.ColumnCount = 2;
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "tableLayoutPanel1";
            this.RowCount = 1;
            this.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Size = new System.Drawing.Size(65, 23);
            this.Padding = new Padding(0, 0, 0, 0);
            this.Margin = new Padding(0, 0, 0, 0);
            this.TabIndex = 0;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label;
    }
}
