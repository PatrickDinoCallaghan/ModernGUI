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
            this.navigtionMenu1 = new ModernGUI.Controls.NavigtionMenu();
            this.tabControl1 = new ModernGUI.Controls.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.wpfTextEditor1 = new ModernGUI.Controls.WPF.WPFTextEditor();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navigtionMenu1
            // 
            this.navigtionMenu1.AutoScroll = true;
            this.navigtionMenu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(61)))));
            this.navigtionMenu1.BackColor_Click = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(49)))), ((int)(((byte)(70)))));
            this.navigtionMenu1.BackColor_Hover = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(49)))), ((int)(((byte)(70)))));
            this.navigtionMenu1.BackColor_Selected = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(104)))), ((int)(((byte)(240)))));
            this.navigtionMenu1.BaseTabControl = this.tabControl1;
            this.navigtionMenu1.Depth = 0;
            this.navigtionMenu1.DisableToggling = new string[0];
            this.navigtionMenu1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.navigtionMenu1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.navigtionMenu1.ForeColor_Selected = System.Drawing.Color.Empty;
            this.navigtionMenu1.IsExpanded = false;
            this.navigtionMenu1.IsExpandedable = true;
            this.navigtionMenu1.ItemHeight = 50;
            this.navigtionMenu1.ItemImageSize = new System.Drawing.Size(20, 20);
            this.navigtionMenu1.ItemPadding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.navigtionMenu1.ItemRightImageMargin = 20;
            this.navigtionMenu1.ItemRightImageSize = new System.Drawing.Size(15, 15);
            this.navigtionMenu1.Items = new ModernGUI.Controls.ButtonItem[0];
            this.navigtionMenu1.ItemTextMargin = 8;
            this.navigtionMenu1.Location = new System.Drawing.Point(-1, 62);
            this.navigtionMenu1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.navigtionMenu1.MouseState = ModernGUI.MouseState.HOVER;
            this.navigtionMenu1.Name = "navigtionMenu1";
            this.navigtionMenu1.Size = new System.Drawing.Size(199, 421);
            this.navigtionMenu1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Depth = 0;
            this.tabControl1.Location = new System.Drawing.Point(204, 70);
            this.tabControl1.MouseState = ModernGUI.MouseState.HOVER;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(601, 413);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.wpfTextEditor1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(593, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(593, 385);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // wpfTextEditor1
            // 
            this.wpfTextEditor1.Location = new System.Drawing.Point(116, 91);
            this.wpfTextEditor1.Name = "wpfTextEditor1";
            this.wpfTextEditor1.Size = new System.Drawing.Size(382, 256);
            this.wpfTextEditor1.TabIndex = 0;
            this.wpfTextEditor1.Text = "wpfTextEditor1";
            // 
            // Example
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 483);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.navigtionMenu1);
            this.Name = "Example";
            this.Text = "Example";
            this.Load += new System.EventHandler(this.Example_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ModernGUI.Controls.NavigtionMenu navigtionMenu1;
        private ModernGUI.Controls.TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ModernGUI.Controls.WPF.WPFTextEditor wpfTextEditor1;
    }
}