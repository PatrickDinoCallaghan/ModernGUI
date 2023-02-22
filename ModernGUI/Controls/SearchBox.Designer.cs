namespace ModernGUI.Controls
{
    partial class SearchBox
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
            this.Search_btn = new ModernGUI.Controls.RaisedButton();
            this.SearchText_txtbx = new ModernGUI.Controls.SingleLineTextField();
            this.Clear_criteria_btn = new ModernGUI.Controls.FlatButton();
            this.Search_criteria_lbl = new System.Windows.Forms.Label();
            this.criteria_pnl = new System.Windows.Forms.Panel();
            this.criteria_pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Search_btn
            // 
            this.Search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_btn.AutoSize = true;
            this.Search_btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Search_btn.Depth = 0;
            this.Search_btn.Icon = null;
            this.Search_btn.Location = new System.Drawing.Point(164, 0);
            this.Search_btn.MouseState = ModernGUI.MouseState.HOVER;
            this.Search_btn.Name = "Search_btn";
            this.Search_btn.Primary = true;
            this.Search_btn.Size = new System.Drawing.Size(73, 36);
            this.Search_btn.TabIndex = 10;
            this.Search_btn.Text = "Search";
            this.Search_btn.UseVisualStyleBackColor = true;
            this.Search_btn.Click += new System.EventHandler(this.Search_btn_Click);
            // 
            // SearchText_txtbx
            // 
            this.SearchText_txtbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchText_txtbx.Depth = 0;
            this.SearchText_txtbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SearchText_txtbx.Hint = "Search";
            this.SearchText_txtbx.Location = new System.Drawing.Point(3, 6);
            this.SearchText_txtbx.MaxLength = 50;
            this.SearchText_txtbx.MouseState = ModernGUI.MouseState.HOVER;
            this.SearchText_txtbx.Name = "SearchText_txtbx";
            this.SearchText_txtbx.PasswordChar = '\0';
            this.SearchText_txtbx.ReadOnly = false;
            this.SearchText_txtbx.SelectedText = "";
            this.SearchText_txtbx.SelectionLength = 0;
            this.SearchText_txtbx.SelectionStart = 0;
            this.SearchText_txtbx.Size = new System.Drawing.Size(156, 25);
            this.SearchText_txtbx.TabIndex = 9;
            this.SearchText_txtbx.TabStop = false;
            this.SearchText_txtbx.UseSystemPasswordChar = false;
            // 
            // Clear_criteria_btn
            // 
            this.Clear_criteria_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Clear_criteria_btn.AutoSize = true;
            this.Clear_criteria_btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Clear_criteria_btn.Depth = 0;
            this.Clear_criteria_btn.Icon = null;
            this.Clear_criteria_btn.Location = new System.Drawing.Point(175, 29);
            this.Clear_criteria_btn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Clear_criteria_btn.MouseState = ModernGUI.MouseState.HOVER;
            this.Clear_criteria_btn.Name = "Clear_criteria_btn";
            this.Clear_criteria_btn.Primary = false;
            this.Clear_criteria_btn.Size = new System.Drawing.Size(62, 36);
            this.Clear_criteria_btn.TabIndex = 11;
            this.Clear_criteria_btn.Text = "Clear";
            this.Clear_criteria_btn.UseVisualStyleBackColor = true;
            this.Clear_criteria_btn.WarningBorder = false;
            this.Clear_criteria_btn.Click += new System.EventHandler(this.Clear_criteria_btn_Click);
            // 
            // Search_criteria_lbl
            // 
            this.Search_criteria_lbl.AutoSize = true;
            this.Search_criteria_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.Search_criteria_lbl.Location = new System.Drawing.Point(3, 3);
            this.Search_criteria_lbl.Name = "Search_criteria_lbl";
            this.Search_criteria_lbl.Size = new System.Drawing.Size(0, 15);
            this.Search_criteria_lbl.TabIndex = 13;
            // 
            // criteria_pnl
            // 
            this.criteria_pnl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.criteria_pnl.AutoScroll = true;
            this.criteria_pnl.Controls.Add(this.Search_criteria_lbl);
            this.criteria_pnl.Location = new System.Drawing.Point(3, 29);
            this.criteria_pnl.Name = "criteria_pnl";
            this.criteria_pnl.Size = new System.Drawing.Size(156, 36);
            this.criteria_pnl.TabIndex = 14;
            // 
            // SearchBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.Search_btn);
            this.Controls.Add(this.criteria_pnl);
            this.Controls.Add(this.SearchText_txtbx);
            this.Controls.Add(this.Clear_criteria_btn);
            this.Name = "SearchBox";
            this.Size = new System.Drawing.Size(240, 65);
            this.criteria_pnl.ResumeLayout(false);
            this.criteria_pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RaisedButton Search_btn;
        private SingleLineTextField SearchText_txtbx;
        private FlatButton Clear_criteria_btn;
        private System.Windows.Forms.Label Search_criteria_lbl;
        private System.Windows.Forms.Panel criteria_pnl;
    }
}
