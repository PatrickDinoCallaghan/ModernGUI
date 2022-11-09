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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Example));
            this.multiSelectTreeview1 = new ModernGUI.Controls.MultiSelectTreeview();
            this.SuspendLayout();
            // 
            // multiSelectTreeview1
            // 
            this.multiSelectTreeview1.Location = new System.Drawing.Point(86, 155);
            this.multiSelectTreeview1.Name = "multiSelectTreeview1";
            this.multiSelectTreeview1.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("multiSelectTreeview1.SelectedNodes")));
            this.multiSelectTreeview1.Size = new System.Drawing.Size(326, 180);
            this.multiSelectTreeview1.TabIndex = 0;
            // 
            // Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 483);
            this.Controls.Add(this.multiSelectTreeview1);
            this.Name = "Example";
            this.Text = "Example";
            this.Load += new System.EventHandler(this.Example_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ModernGUI.Controls.MultiSelectTreeview multiSelectTreeview1;
    }
}