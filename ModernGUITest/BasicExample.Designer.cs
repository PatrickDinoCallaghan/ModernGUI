using System.Drawing;
using System.Windows.Forms;
using ModernGUI;
using ModernGUI.Controls;

namespace ModernGUITest
{
    partial class BasicExample
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            ModernGUI.Controls.CalendarHighlightRange calendarHighlightRange1 = new ModernGUI.Controls.CalendarHighlightRange();
            ModernGUI.Controls.CalendarHighlightRange calendarHighlightRange2 = new ModernGUI.Controls.CalendarHighlightRange();
            ModernGUI.Controls.CalendarHighlightRange calendarHighlightRange3 = new ModernGUI.Controls.CalendarHighlightRange();
            ModernGUI.Controls.CalendarHighlightRange calendarHighlightRange4 = new ModernGUI.Controls.CalendarHighlightRange();
            ModernGUI.Controls.CalendarHighlightRange calendarHighlightRange5 = new ModernGUI.Controls.CalendarHighlightRange();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicExample));
            this.FlatButton2 = new ModernGUI.Controls.FlatButton();
            this.FlatButton1 = new ModernGUI.Controls.FlatButton();
            this.Divider1 = new ModernGUI.Controls.Divider();
            this.Checkbox4 = new ModernGUI.Controls.Checkbox();
            this.Checkbox3 = new ModernGUI.Controls.Checkbox();
            this.Checkbox2 = new ModernGUI.Controls.Checkbox();
            this.Checkbox1 = new ModernGUI.Controls.Checkbox();
            this.SingleLineTextField2 = new ModernGUI.Controls.SingleLineTextField();
            this.SingleLineTextField1 = new ModernGUI.Controls.SingleLineTextField();
            this.Button1 = new ModernGUI.Controls.RaisedButton();
            this.TabSelector1 = new ModernGUI.Controls.TabSelector();
            this.TabControl1 = new ModernGUI.Controls.TabControl();
            this.TextField_tabPage = new System.Windows.Forms.TabPage();
            this.SingleLineTextField3 = new ModernGUI.Controls.SingleLineTextField();
            this.RaisedButton1 = new ModernGUI.Controls.RaisedButton();
            this.SelectionBox_tabPage = new System.Windows.Forms.TabPage();
            this.RadioButton4 = new ModernGUI.Controls.RadioButton();
            this.RadioButton1 = new ModernGUI.Controls.RadioButton();
            this.RadioButton2 = new ModernGUI.Controls.RadioButton();
            this.RadioButton3 = new ModernGUI.Controls.RadioButton();
            this.CheckBox6 = new ModernGUI.Controls.Checkbox();
            this.CheckBox5 = new ModernGUI.Controls.Checkbox();
            this.gantt_tabPage = new System.Windows.Forms.TabPage();
            this.Progress_tabPage = new System.Windows.Forms.TabPage();
            this.Label2 = new ModernGUI.Controls.Label();
            this.FlatButton4 = new ModernGUI.Controls.FlatButton();
            this.RaisedButton2 = new ModernGUI.Controls.RaisedButton();
            this.ProgressBar1 = new ModernGUI.Controls.ProgressBar();
            this.DGV_tabPage = new System.Windows.Forms.TabPage();
            this.DGVListBoxStyle_raisedButton = new ModernGUI.Controls.RaisedButton();
            this.MoveDGV_raisedButton = new ModernGUI.Controls.RaisedButton();
            this.ReadonlyDGV_raisedButton = new ModernGUI.Controls.RaisedButton();
            this.dataGridView1 = new ModernGUI.Controls.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new ModernGUI.Controls.Columns.AddRemoveColumn();
            this.Calendar_tabPage = new System.Windows.Forms.TabPage();
            this.monthView1 = new ModernGUI.Controls.MonthView();
            this.calendar1 = new ModernGUI.Controls.Calendar();
            this.Graph_tabPage = new System.Windows.Forms.TabPage();
            this.label1 = new ModernGUI.Controls.Label();
            this.multiSelectTreeview1 = new ModernGUI.Controls.MultiSelectTreeview();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.ContextMenuStrip1 = new ModernGUI.Controls.ContextMenuStrip();
            this.item1ToolStripMenuItem = new ModernGUI.Controls.ToolStripMenuItem();
            this.subItem1ToolStripMenuItem = new ModernGUI.Controls.ToolStripMenuItem();
            this.subItem2ToolStripMenuItem = new ModernGUI.Controls.ToolStripMenuItem();
            this.disabledItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.item2ToolStripMenuItem = new ModernGUI.Controls.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.item3ToolStripMenuItem = new ModernGUI.Controls.ToolStripMenuItem();
            this.FlatButton3 = new ModernGUI.Controls.FlatButton();
            this.TabControl1.SuspendLayout();
            this.TextField_tabPage.SuspendLayout();
            this.SelectionBox_tabPage.SuspendLayout();
            this.Progress_tabPage.SuspendLayout();
            this.DGV_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Calendar_tabPage.SuspendLayout();
            this.Graph_tabPage.SuspendLayout();
            this.ContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FlatButton2
            // 
            this.FlatButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FlatButton2.AutoSize = true;
            this.FlatButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlatButton2.Depth = 0;
            this.FlatButton2.Icon = null;
            this.FlatButton2.Location = new System.Drawing.Point(575, 467);
            this.FlatButton2.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.FlatButton2.MouseState = ModernGUI.MouseState.HOVER;
            this.FlatButton2.Name = "FlatButton2";
            this.FlatButton2.Primary = false;
            this.FlatButton2.Size = new System.Drawing.Size(102, 36);
            this.FlatButton2.TabIndex = 13;
            this.FlatButton2.Text = "Secondary";
            this.FlatButton2.UseVisualStyleBackColor = true;
            // 
            // FlatButton1
            // 
            this.FlatButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FlatButton1.AutoSize = true;
            this.FlatButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlatButton1.Depth = 0;
            this.FlatButton1.Icon = null;
            this.FlatButton1.Location = new System.Drawing.Point(688, 467);
            this.FlatButton1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.FlatButton1.MouseState = ModernGUI.MouseState.HOVER;
            this.FlatButton1.Name = "FlatButton1";
            this.FlatButton1.Primary = true;
            this.FlatButton1.Size = new System.Drawing.Size(80, 36);
            this.FlatButton1.TabIndex = 1;
            this.FlatButton1.Text = "Primary";
            this.FlatButton1.UseVisualStyleBackColor = true;
            // 
            // Divider1
            // 
            this.Divider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Divider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Divider1.Depth = 0;
            this.Divider1.Location = new System.Drawing.Point(0, 453);
            this.Divider1.Margin = new System.Windows.Forms.Padding(0);
            this.Divider1.MouseState = ModernGUI.MouseState.HOVER;
            this.Divider1.Name = "Divider1";
            this.Divider1.Size = new System.Drawing.Size(789, 1);
            this.Divider1.TabIndex = 16;
            this.Divider1.Text = "Divider1";
            // 
            // Checkbox4
            // 
            this.Checkbox4.AutoSize = true;
            this.Checkbox4.Depth = 0;
            this.Checkbox4.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Checkbox4.Location = new System.Drawing.Point(0, 113);
            this.Checkbox4.Margin = new System.Windows.Forms.Padding(0);
            this.Checkbox4.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Checkbox4.MouseState = ModernGUI.MouseState.HOVER;
            this.Checkbox4.Name = "Checkbox4";
            this.Checkbox4.Ripple = true;
            this.Checkbox4.Size = new System.Drawing.Size(101, 30);
            this.Checkbox4.TabIndex = 7;
            this.Checkbox4.Text = "Checkbox4";
            this.Checkbox4.UseVisualStyleBackColor = true;
            // 
            // Checkbox3
            // 
            this.Checkbox3.AutoSize = true;
            this.Checkbox3.Depth = 0;
            this.Checkbox3.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Checkbox3.Location = new System.Drawing.Point(0, 78);
            this.Checkbox3.Margin = new System.Windows.Forms.Padding(0);
            this.Checkbox3.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Checkbox3.MouseState = ModernGUI.MouseState.HOVER;
            this.Checkbox3.Name = "Checkbox3";
            this.Checkbox3.Ripple = true;
            this.Checkbox3.Size = new System.Drawing.Size(101, 30);
            this.Checkbox3.TabIndex = 6;
            this.Checkbox3.Text = "Checkbox3";
            this.Checkbox3.UseVisualStyleBackColor = true;
            // 
            // Checkbox2
            // 
            this.Checkbox2.AutoSize = true;
            this.Checkbox2.Checked = true;
            this.Checkbox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Checkbox2.Depth = 0;
            this.Checkbox2.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Checkbox2.Location = new System.Drawing.Point(0, 44);
            this.Checkbox2.Margin = new System.Windows.Forms.Padding(0);
            this.Checkbox2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Checkbox2.MouseState = ModernGUI.MouseState.HOVER;
            this.Checkbox2.Name = "Checkbox2";
            this.Checkbox2.Ripple = true;
            this.Checkbox2.Size = new System.Drawing.Size(101, 30);
            this.Checkbox2.TabIndex = 5;
            this.Checkbox2.Text = "Checkbox2";
            this.Checkbox2.UseVisualStyleBackColor = true;
            // 
            // Checkbox1
            // 
            this.Checkbox1.AutoSize = true;
            this.Checkbox1.Checked = true;
            this.Checkbox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Checkbox1.Depth = 0;
            this.Checkbox1.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Checkbox1.Location = new System.Drawing.Point(0, 9);
            this.Checkbox1.Margin = new System.Windows.Forms.Padding(0);
            this.Checkbox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Checkbox1.MouseState = ModernGUI.MouseState.HOVER;
            this.Checkbox1.Name = "Checkbox1";
            this.Checkbox1.Ripple = true;
            this.Checkbox1.Size = new System.Drawing.Size(101, 30);
            this.Checkbox1.TabIndex = 4;
            this.Checkbox1.Text = "Checkbox1";
            this.Checkbox1.UseVisualStyleBackColor = true;
            // 
            // SingleLineTextField2
            // 
            this.SingleLineTextField2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SingleLineTextField2.Depth = 0;
            this.SingleLineTextField2.Hint = "Another example hint";
            this.SingleLineTextField2.Location = new System.Drawing.Point(0, 59);
            this.SingleLineTextField2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SingleLineTextField2.MaxLength = 32767;
            this.SingleLineTextField2.MouseState = ModernGUI.MouseState.HOVER;
            this.SingleLineTextField2.Name = "SingleLineTextField2";
            this.SingleLineTextField2.PasswordChar = '\0';
            this.SingleLineTextField2.SelectedText = "";
            this.SingleLineTextField2.SelectionLength = 0;
            this.SingleLineTextField2.SelectionStart = 0;
            this.SingleLineTextField2.Size = new System.Drawing.Size(700, 25);
            this.SingleLineTextField2.TabIndex = 3;
            this.SingleLineTextField2.TabStop = false;
            this.SingleLineTextField2.UseSystemPasswordChar = false;
            // 
            // SingleLineTextField1
            // 
            this.SingleLineTextField1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SingleLineTextField1.Depth = 0;
            this.SingleLineTextField1.Hint = "This is a hint";
            this.SingleLineTextField1.Location = new System.Drawing.Point(0, 16);
            this.SingleLineTextField1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SingleLineTextField1.MaxLength = 32767;
            this.SingleLineTextField1.MouseState = ModernGUI.MouseState.HOVER;
            this.SingleLineTextField1.Name = "SingleLineTextField1";
            this.SingleLineTextField1.PasswordChar = '\0';
            this.SingleLineTextField1.SelectedText = "";
            this.SingleLineTextField1.SelectionLength = 0;
            this.SingleLineTextField1.SelectionStart = 0;
            this.SingleLineTextField1.Size = new System.Drawing.Size(700, 25);
            this.SingleLineTextField1.TabIndex = 2;
            this.SingleLineTextField1.TabStop = false;
            this.SingleLineTextField1.UseSystemPasswordChar = false;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.AutoSize = true;
            this.Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Button1.Depth = 0;
            this.Button1.Icon = null;
            this.Button1.Location = new System.Drawing.Point(354, 277);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.MouseState = ModernGUI.MouseState.HOVER;
            this.Button1.Name = "Button1";
            this.Button1.Primary = true;
            this.Button1.Size = new System.Drawing.Size(126, 36);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Change Theme";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TabSelector1
            // 
            this.TabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabSelector1.BaseTabControl = this.TabControl1;
            this.TabSelector1.Depth = 0;
            this.TabSelector1.Location = new System.Drawing.Point(0, 25);
            this.TabSelector1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TabSelector1.MouseState = ModernGUI.MouseState.HOVER;
            this.TabSelector1.Name = "TabSelector1";
            this.TabSelector1.Size = new System.Drawing.Size(789, 48);
            this.TabSelector1.TabIndex = 17;
            this.TabSelector1.Text = "TabSelector1";
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TextField_tabPage);
            this.TabControl1.Controls.Add(this.SelectionBox_tabPage);
            this.TabControl1.Controls.Add(this.gantt_tabPage);
            this.TabControl1.Controls.Add(this.Progress_tabPage);
            this.TabControl1.Controls.Add(this.DGV_tabPage);
            this.TabControl1.Controls.Add(this.Calendar_tabPage);
            this.TabControl1.Controls.Add(this.Graph_tabPage);
            this.TabControl1.Depth = 0;
            this.TabControl1.ItemSize = new System.Drawing.Size(104, 20);
            this.TabControl1.Location = new System.Drawing.Point(16, 79);
            this.TabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TabControl1.MouseState = ModernGUI.MouseState.HOVER;
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(750, 360);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl1.TabIndex = 18;
            // 
            // TextField_tabPage
            // 
            this.TextField_tabPage.BackColor = System.Drawing.Color.White;
            this.TextField_tabPage.Controls.Add(this.SingleLineTextField3);
            this.TextField_tabPage.Controls.Add(this.RaisedButton1);
            this.TextField_tabPage.Controls.Add(this.SingleLineTextField1);
            this.TextField_tabPage.Controls.Add(this.SingleLineTextField2);
            this.TextField_tabPage.Controls.Add(this.Button1);
            this.TextField_tabPage.Location = new System.Drawing.Point(4, 24);
            this.TextField_tabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextField_tabPage.Name = "TextField_tabPage";
            this.TextField_tabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TextField_tabPage.Size = new System.Drawing.Size(742, 332);
            this.TextField_tabPage.TabIndex = 0;
            this.TextField_tabPage.Text = "TextField";
            // 
            // SingleLineTextField3
            // 
            this.SingleLineTextField3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SingleLineTextField3.Depth = 0;
            this.SingleLineTextField3.Hint = "This is a password";
            this.SingleLineTextField3.Location = new System.Drawing.Point(0, 102);
            this.SingleLineTextField3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SingleLineTextField3.MaxLength = 32767;
            this.SingleLineTextField3.MouseState = ModernGUI.MouseState.HOVER;
            this.SingleLineTextField3.Name = "SingleLineTextField3";
            this.SingleLineTextField3.PasswordChar = '\0';
            this.SingleLineTextField3.SelectedText = "";
            this.SingleLineTextField3.SelectionLength = 0;
            this.SingleLineTextField3.SelectionStart = 0;
            this.SingleLineTextField3.Size = new System.Drawing.Size(700, 25);
            this.SingleLineTextField3.TabIndex = 4;
            this.SingleLineTextField3.TabStop = false;
            this.SingleLineTextField3.UseSystemPasswordChar = true;
            // 
            // RaisedButton1
            // 
            this.RaisedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RaisedButton1.AutoSize = true;
            this.RaisedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RaisedButton1.Depth = 0;
            this.RaisedButton1.Icon = null;
            this.RaisedButton1.Location = new System.Drawing.Point(516, 277);
            this.RaisedButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RaisedButton1.MouseState = ModernGUI.MouseState.HOVER;
            this.RaisedButton1.Name = "RaisedButton1";
            this.RaisedButton1.Primary = true;
            this.RaisedButton1.Size = new System.Drawing.Size(184, 36);
            this.RaisedButton1.TabIndex = 21;
            this.RaisedButton1.Text = "Change color scheme";
            this.RaisedButton1.UseVisualStyleBackColor = true;
            this.RaisedButton1.Click += new System.EventHandler(this.RaisedButton1_Click);
            // 
            // SelectionBox_tabPage
            // 
            this.SelectionBox_tabPage.BackColor = System.Drawing.Color.White;
            this.SelectionBox_tabPage.Controls.Add(this.RadioButton4);
            this.SelectionBox_tabPage.Controls.Add(this.RadioButton1);
            this.SelectionBox_tabPage.Controls.Add(this.RadioButton2);
            this.SelectionBox_tabPage.Controls.Add(this.RadioButton3);
            this.SelectionBox_tabPage.Controls.Add(this.CheckBox6);
            this.SelectionBox_tabPage.Controls.Add(this.CheckBox5);
            this.SelectionBox_tabPage.Controls.Add(this.Checkbox3);
            this.SelectionBox_tabPage.Controls.Add(this.Checkbox1);
            this.SelectionBox_tabPage.Controls.Add(this.Checkbox2);
            this.SelectionBox_tabPage.Controls.Add(this.Checkbox4);
            this.SelectionBox_tabPage.Location = new System.Drawing.Point(4, 24);
            this.SelectionBox_tabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectionBox_tabPage.Name = "SelectionBox_tabPage";
            this.SelectionBox_tabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectionBox_tabPage.Size = new System.Drawing.Size(742, 332);
            this.SelectionBox_tabPage.TabIndex = 1;
            this.SelectionBox_tabPage.Text = "SelectionBox";
            // 
            // RadioButton4
            // 
            this.RadioButton4.AutoSize = true;
            this.RadioButton4.Checked = true;
            this.RadioButton4.Depth = 0;
            this.RadioButton4.Enabled = false;
            this.RadioButton4.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RadioButton4.Location = new System.Drawing.Point(129, 113);
            this.RadioButton4.Margin = new System.Windows.Forms.Padding(0);
            this.RadioButton4.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RadioButton4.MouseState = ModernGUI.MouseState.HOVER;
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Ripple = true;
            this.RadioButton4.Size = new System.Drawing.Size(118, 30);
            this.RadioButton4.TabIndex = 19;
            this.RadioButton4.TabStop = true;
            this.RadioButton4.Text = "RadioButton4";
            this.RadioButton4.UseVisualStyleBackColor = true;
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Depth = 0;
            this.RadioButton1.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RadioButton1.Location = new System.Drawing.Point(129, 9);
            this.RadioButton1.Margin = new System.Windows.Forms.Padding(0);
            this.RadioButton1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RadioButton1.MouseState = ModernGUI.MouseState.HOVER;
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Ripple = true;
            this.RadioButton1.Size = new System.Drawing.Size(118, 30);
            this.RadioButton1.TabIndex = 16;
            this.RadioButton1.Text = "RadioButton1";
            this.RadioButton1.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Depth = 0;
            this.RadioButton2.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RadioButton2.Location = new System.Drawing.Point(129, 44);
            this.RadioButton2.Margin = new System.Windows.Forms.Padding(0);
            this.RadioButton2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RadioButton2.MouseState = ModernGUI.MouseState.HOVER;
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Ripple = true;
            this.RadioButton2.Size = new System.Drawing.Size(118, 30);
            this.RadioButton2.TabIndex = 17;
            this.RadioButton2.Text = "RadioButton2";
            this.RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.Depth = 0;
            this.RadioButton3.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RadioButton3.Location = new System.Drawing.Point(129, 78);
            this.RadioButton3.Margin = new System.Windows.Forms.Padding(0);
            this.RadioButton3.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RadioButton3.MouseState = ModernGUI.MouseState.HOVER;
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Ripple = true;
            this.RadioButton3.Size = new System.Drawing.Size(118, 30);
            this.RadioButton3.TabIndex = 18;
            this.RadioButton3.Text = "RadioButton3";
            this.RadioButton3.UseVisualStyleBackColor = true;
            // 
            // CheckBox6
            // 
            this.CheckBox6.AutoSize = true;
            this.CheckBox6.Depth = 0;
            this.CheckBox6.Enabled = false;
            this.CheckBox6.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckBox6.Location = new System.Drawing.Point(0, 182);
            this.CheckBox6.Margin = new System.Windows.Forms.Padding(0);
            this.CheckBox6.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CheckBox6.MouseState = ModernGUI.MouseState.HOVER;
            this.CheckBox6.Name = "CheckBox6";
            this.CheckBox6.Ripple = true;
            this.CheckBox6.Size = new System.Drawing.Size(102, 30);
            this.CheckBox6.TabIndex = 9;
            this.CheckBox6.Text = "CheckBox6";
            this.CheckBox6.UseVisualStyleBackColor = true;
            // 
            // CheckBox5
            // 
            this.CheckBox5.AutoSize = true;
            this.CheckBox5.Checked = true;
            this.CheckBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox5.Depth = 0;
            this.CheckBox5.Enabled = false;
            this.CheckBox5.Font = new System.Drawing.Font("Open Sans Medium", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CheckBox5.Location = new System.Drawing.Point(0, 148);
            this.CheckBox5.Margin = new System.Windows.Forms.Padding(0);
            this.CheckBox5.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CheckBox5.MouseState = ModernGUI.MouseState.HOVER;
            this.CheckBox5.Name = "CheckBox5";
            this.CheckBox5.Ripple = true;
            this.CheckBox5.Size = new System.Drawing.Size(102, 30);
            this.CheckBox5.TabIndex = 8;
            this.CheckBox5.Text = "CheckBox5";
            this.CheckBox5.UseVisualStyleBackColor = true;
            // 
            // gantt_tabPage
            // 
            this.gantt_tabPage.Location = new System.Drawing.Point(4, 24);
            this.gantt_tabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gantt_tabPage.Name = "gantt_tabPage";
            this.gantt_tabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gantt_tabPage.Size = new System.Drawing.Size(742, 332);
            this.gantt_tabPage.TabIndex = 3;
            this.gantt_tabPage.Text = "gantt";
            this.gantt_tabPage.UseVisualStyleBackColor = true;
            // 
            // Progress_tabPage
            // 
            this.Progress_tabPage.Controls.Add(this.Label2);
            this.Progress_tabPage.Controls.Add(this.FlatButton4);
            this.Progress_tabPage.Controls.Add(this.RaisedButton2);
            this.Progress_tabPage.Controls.Add(this.ProgressBar1);
            this.Progress_tabPage.Location = new System.Drawing.Point(4, 24);
            this.Progress_tabPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Progress_tabPage.Name = "Progress_tabPage";
            this.Progress_tabPage.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Progress_tabPage.Size = new System.Drawing.Size(742, 332);
            this.Progress_tabPage.TabIndex = 4;
            this.Progress_tabPage.Text = "Progres";
            this.Progress_tabPage.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.Depth = 0;
            this.Label2.Font = new System.Drawing.Font("Open Sans Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Label2.Location = new System.Drawing.Point(4, 17);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.MouseState = ModernGUI.MouseState.HOVER;
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(677, 73);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Here we\'re showcasing the progressbar together with a FlatButton and a RaisedButt" +
    "on that have support for icons.";
            // 
            // FlatButton4
            // 
            this.FlatButton4.AutoSize = true;
            this.FlatButton4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlatButton4.Depth = 0;
            this.FlatButton4.Icon = null;
            this.FlatButton4.Location = new System.Drawing.Point(8, 110);
            this.FlatButton4.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.FlatButton4.MouseState = ModernGUI.MouseState.HOVER;
            this.FlatButton4.Name = "FlatButton4";
            this.FlatButton4.Primary = false;
            this.FlatButton4.Size = new System.Drawing.Size(89, 36);
            this.FlatButton4.TabIndex = 2;
            this.FlatButton4.Text = "Subtract";
            this.FlatButton4.UseVisualStyleBackColor = true;
            this.FlatButton4.Click += new System.EventHandler(this.FlatButton4_Click);
            // 
            // RaisedButton2
            // 
            this.RaisedButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RaisedButton2.AutoSize = true;
            this.RaisedButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RaisedButton2.Depth = 0;
            this.RaisedButton2.Icon = null;
            this.RaisedButton2.Location = new System.Drawing.Point(592, 110);
            this.RaisedButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RaisedButton2.MouseState = ModernGUI.MouseState.HOVER;
            this.RaisedButton2.Name = "RaisedButton2";
            this.RaisedButton2.Primary = true;
            this.RaisedButton2.Size = new System.Drawing.Size(50, 36);
            this.RaisedButton2.TabIndex = 1;
            this.RaisedButton2.Text = "Add";
            this.RaisedButton2.UseVisualStyleBackColor = true;
            this.RaisedButton2.Click += new System.EventHandler(this.RaisedButton2_Click);
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar1.Depth = 0;
            this.ProgressBar1.Location = new System.Drawing.Point(7, 93);
            this.ProgressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProgressBar1.MouseState = ModernGUI.MouseState.HOVER;
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(666, 5);
            this.ProgressBar1.TabIndex = 0;
            this.ProgressBar1.Value = 45;
            // 
            // DGV_tabPage
            // 
            this.DGV_tabPage.Controls.Add(this.DGVListBoxStyle_raisedButton);
            this.DGV_tabPage.Controls.Add(this.MoveDGV_raisedButton);
            this.DGV_tabPage.Controls.Add(this.ReadonlyDGV_raisedButton);
            this.DGV_tabPage.Controls.Add(this.dataGridView1);
            this.DGV_tabPage.Location = new System.Drawing.Point(4, 24);
            this.DGV_tabPage.Name = "DGV_tabPage";
            this.DGV_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DGV_tabPage.Size = new System.Drawing.Size(742, 332);
            this.DGV_tabPage.TabIndex = 5;
            this.DGV_tabPage.Text = "DGV";
            this.DGV_tabPage.UseVisualStyleBackColor = true;
            // 
            // DGVListBoxStyle_raisedButton
            // 
            this.DGVListBoxStyle_raisedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVListBoxStyle_raisedButton.AutoSize = true;
            this.DGVListBoxStyle_raisedButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DGVListBoxStyle_raisedButton.Depth = 0;
            this.DGVListBoxStyle_raisedButton.Icon = null;
            this.DGVListBoxStyle_raisedButton.Location = new System.Drawing.Point(585, 198);
            this.DGVListBoxStyle_raisedButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DGVListBoxStyle_raisedButton.MouseState = ModernGUI.MouseState.HOVER;
            this.DGVListBoxStyle_raisedButton.Name = "DGVListBoxStyle_raisedButton";
            this.DGVListBoxStyle_raisedButton.Primary = true;
            this.DGVListBoxStyle_raisedButton.Size = new System.Drawing.Size(113, 36);
            this.DGVListBoxStyle_raisedButton.TabIndex = 3;
            this.DGVListBoxStyle_raisedButton.Text = "ListBoxStyle";
            this.DGVListBoxStyle_raisedButton.UseVisualStyleBackColor = true;
            this.DGVListBoxStyle_raisedButton.Click += new System.EventHandler(this.DGVListBoxStyle_raisedButton_Click);
            // 
            // MoveDGV_raisedButton
            // 
            this.MoveDGV_raisedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveDGV_raisedButton.AutoSize = true;
            this.MoveDGV_raisedButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MoveDGV_raisedButton.Depth = 0;
            this.MoveDGV_raisedButton.Icon = null;
            this.MoveDGV_raisedButton.Location = new System.Drawing.Point(585, 156);
            this.MoveDGV_raisedButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MoveDGV_raisedButton.MouseState = ModernGUI.MouseState.HOVER;
            this.MoveDGV_raisedButton.Name = "MoveDGV_raisedButton";
            this.MoveDGV_raisedButton.Primary = true;
            this.MoveDGV_raisedButton.Size = new System.Drawing.Size(93, 36);
            this.MoveDGV_raisedButton.TabIndex = 2;
            this.MoveDGV_raisedButton.Text = "MOVEABLE";
            this.MoveDGV_raisedButton.UseVisualStyleBackColor = true;
            this.MoveDGV_raisedButton.Click += new System.EventHandler(this.MoveDGV_raisedButton_Click);
            // 
            // ReadonlyDGV_raisedButton
            // 
            this.ReadonlyDGV_raisedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadonlyDGV_raisedButton.AutoSize = true;
            this.ReadonlyDGV_raisedButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ReadonlyDGV_raisedButton.Depth = 0;
            this.ReadonlyDGV_raisedButton.Icon = null;
            this.ReadonlyDGV_raisedButton.Location = new System.Drawing.Point(585, 114);
            this.ReadonlyDGV_raisedButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ReadonlyDGV_raisedButton.MouseState = ModernGUI.MouseState.HOVER;
            this.ReadonlyDGV_raisedButton.Name = "ReadonlyDGV_raisedButton";
            this.ReadonlyDGV_raisedButton.Primary = true;
            this.ReadonlyDGV_raisedButton.Size = new System.Drawing.Size(93, 36);
            this.ReadonlyDGV_raisedButton.TabIndex = 1;
            this.ReadonlyDGV_raisedButton.Text = "ReadOnly";
            this.ReadonlyDGV_raisedButton.UseVisualStyleBackColor = true;
            this.ReadonlyDGV_raisedButton.Click += new System.EventHandler(this.raisedButton3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDragDrop = false;
            this.dataGridView1.AllowUserToResize = false;
            this.dataGridView1.ColumnHeadersHeight = 50;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Depth = 0;
            this.dataGridView1.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.dataGridView1.HeaderHeight = 50;
            this.dataGridView1.Location = new System.Drawing.Point(19, 15);
            this.dataGridView1.MouseState = ModernGUI.MouseState.HOVER;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeight = 40;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 46;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(504, 262);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 134.7586F;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 134.7586F;
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 61.28387F;
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Calendar_tabPage
            // 
            this.Calendar_tabPage.Controls.Add(this.monthView1);
            this.Calendar_tabPage.Controls.Add(this.calendar1);
            this.Calendar_tabPage.Location = new System.Drawing.Point(4, 24);
            this.Calendar_tabPage.Name = "Calendar_tabPage";
            this.Calendar_tabPage.Size = new System.Drawing.Size(742, 332);
            this.Calendar_tabPage.TabIndex = 6;
            this.Calendar_tabPage.Text = "Calendar";
            this.Calendar_tabPage.UseVisualStyleBackColor = true;
            // 
            // monthView1
            // 
            this.monthView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.monthView1.ArrowsColor = System.Drawing.SystemColors.Window;
            this.monthView1.ArrowsSelectedColor = System.Drawing.Color.Gold;
            this.monthView1.DayBackgroundColor = System.Drawing.Color.Empty;
            this.monthView1.DayGrayedText = System.Drawing.SystemColors.GrayText;
            this.monthView1.DaySelectedBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.monthView1.DaySelectedColor = System.Drawing.SystemColors.WindowText;
            this.monthView1.DaySelectedTextColor = System.Drawing.SystemColors.HighlightText;
            this.monthView1.ItemPadding = new System.Windows.Forms.Padding(2);
            this.monthView1.Location = new System.Drawing.Point(3, 3);
            this.monthView1.MonthTitleColor = System.Drawing.SystemColors.ActiveCaption;
            this.monthView1.MonthTitleColorInactive = System.Drawing.SystemColors.InactiveCaption;
            this.monthView1.MonthTitleTextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.monthView1.MonthTitleTextColorInactive = System.Drawing.SystemColors.InactiveCaptionText;
            this.monthView1.Name = "monthView1";
            this.monthView1.Size = new System.Drawing.Size(225, 329);
            this.monthView1.TabIndex = 1;
            this.monthView1.Text = "monthView1";
            this.monthView1.TodayBorderColor = System.Drawing.Color.Maroon;
            // 
            // calendar1
            // 
            this.calendar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calendar1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            calendarHighlightRange1.DayOfWeek = System.DayOfWeek.Monday;
            calendarHighlightRange1.EndTime = System.TimeSpan.Parse("17:00:00");
            calendarHighlightRange1.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange2.DayOfWeek = System.DayOfWeek.Tuesday;
            calendarHighlightRange2.EndTime = System.TimeSpan.Parse("17:00:00");
            calendarHighlightRange2.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange3.DayOfWeek = System.DayOfWeek.Wednesday;
            calendarHighlightRange3.EndTime = System.TimeSpan.Parse("17:00:00");
            calendarHighlightRange3.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange4.DayOfWeek = System.DayOfWeek.Thursday;
            calendarHighlightRange4.EndTime = System.TimeSpan.Parse("17:00:00");
            calendarHighlightRange4.StartTime = System.TimeSpan.Parse("08:00:00");
            calendarHighlightRange5.DayOfWeek = System.DayOfWeek.Friday;
            calendarHighlightRange5.EndTime = System.TimeSpan.Parse("17:00:00");
            calendarHighlightRange5.StartTime = System.TimeSpan.Parse("08:00:00");
            this.calendar1.HighlightRanges = new ModernGUI.Controls.CalendarHighlightRange[] {
        calendarHighlightRange1,
        calendarHighlightRange2,
        calendarHighlightRange3,
        calendarHighlightRange4,
        calendarHighlightRange5};
            this.calendar1.Location = new System.Drawing.Point(234, 3);
            this.calendar1.Name = "calendar1";
            this.calendar1.Size = new System.Drawing.Size(505, 326);
            this.calendar1.TabIndex = 0;
            this.calendar1.Text = "calendar1";
            // 
            // Graph_tabPage
            // 
            this.Graph_tabPage.Controls.Add(this.label1);
            this.Graph_tabPage.Controls.Add(this.multiSelectTreeview1);
            this.Graph_tabPage.Location = new System.Drawing.Point(4, 24);
            this.Graph_tabPage.Name = "Graph_tabPage";
            this.Graph_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.Graph_tabPage.Size = new System.Drawing.Size(742, 332);
            this.Graph_tabPage.TabIndex = 7;
            this.Graph_tabPage.Text = "Graph";
            this.Graph_tabPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Depth = 0;
            this.label1.Font = new System.Drawing.Font("Open Sans Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(25, 11);
            this.label1.MouseState = ModernGUI.MouseState.HOVER;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "MultiSelectTreeview";
            // 
            // multiSelectTreeview1
            // 
            this.multiSelectTreeview1.Location = new System.Drawing.Point(25, 37);
            this.multiSelectTreeview1.Name = "multiSelectTreeview1";
            this.multiSelectTreeview1.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("multiSelectTreeview1.SelectedNodes")));
            this.multiSelectTreeview1.Size = new System.Drawing.Size(254, 156);
            this.multiSelectTreeview1.TabIndex = 0;
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip1.Depth = 0;
            this.ContextMenuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item1ToolStripMenuItem,
            this.disabledItemToolStripMenuItem,
            this.item2ToolStripMenuItem,
            this.toolStripSeparator1,
            this.item3ToolStripMenuItem});
            this.ContextMenuStrip1.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.ContextMenuStrip1.MouseState = ModernGUI.MouseState.HOVER;
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(166, 130);
            // 
            // item1ToolStripMenuItem
            // 
            this.item1ToolStripMenuItem.AutoSize = false;
            this.item1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subItem1ToolStripMenuItem,
            this.subItem2ToolStripMenuItem});
            this.item1ToolStripMenuItem.Name = "item1ToolStripMenuItem";
            this.item1ToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.item1ToolStripMenuItem.Text = "Item 1";
            // 
            // subItem1ToolStripMenuItem
            // 
            this.subItem1ToolStripMenuItem.AutoSize = false;
            this.subItem1ToolStripMenuItem.Name = "subItem1ToolStripMenuItem";
            this.subItem1ToolStripMenuItem.Size = new System.Drawing.Size(152, 30);
            this.subItem1ToolStripMenuItem.Text = "SubItem 1";
            // 
            // subItem2ToolStripMenuItem
            // 
            this.subItem2ToolStripMenuItem.AutoSize = false;
            this.subItem2ToolStripMenuItem.Name = "subItem2ToolStripMenuItem";
            this.subItem2ToolStripMenuItem.Size = new System.Drawing.Size(152, 30);
            this.subItem2ToolStripMenuItem.Text = "SubItem 2";
            // 
            // disabledItemToolStripMenuItem
            // 
            this.disabledItemToolStripMenuItem.AutoSize = false;
            this.disabledItemToolStripMenuItem.Enabled = false;
            this.disabledItemToolStripMenuItem.Name = "disabledItemToolStripMenuItem";
            this.disabledItemToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.disabledItemToolStripMenuItem.Text = "Disabled item";
            // 
            // item2ToolStripMenuItem
            // 
            this.item2ToolStripMenuItem.AutoSize = false;
            this.item2ToolStripMenuItem.Name = "item2ToolStripMenuItem";
            this.item2ToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.item2ToolStripMenuItem.Text = "Item 2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // item3ToolStripMenuItem
            // 
            this.item3ToolStripMenuItem.AutoSize = false;
            this.item3ToolStripMenuItem.Name = "item3ToolStripMenuItem";
            this.item3ToolStripMenuItem.Size = new System.Drawing.Size(170, 30);
            this.item3ToolStripMenuItem.Text = "Item 3";
            // 
            // FlatButton3
            // 
            this.FlatButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FlatButton3.AutoSize = true;
            this.FlatButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlatButton3.Depth = 0;
            this.FlatButton3.Enabled = false;
            this.FlatButton3.Icon = null;
            this.FlatButton3.Location = new System.Drawing.Point(474, 467);
            this.FlatButton3.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.FlatButton3.MouseState = ModernGUI.MouseState.HOVER;
            this.FlatButton3.Name = "FlatButton3";
            this.FlatButton3.Primary = false;
            this.FlatButton3.Size = new System.Drawing.Size(85, 36);
            this.FlatButton3.TabIndex = 19;
            this.FlatButton3.Text = "DISABLED";
            this.FlatButton3.UseVisualStyleBackColor = true;
            // 
            // BasicExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(780, 511);
            this.ContextMenuStrip = this.ContextMenuStrip1;
            this.Controls.Add(this.FlatButton3);
            this.Controls.Add(this.FlatButton2);
            this.Controls.Add(this.TabSelector1);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.Divider1);
            this.Controls.Add(this.FlatButton1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "BasicExample";
            this.Text = "Example";
            this.TabControl1.ResumeLayout(false);
            this.TextField_tabPage.ResumeLayout(false);
            this.TextField_tabPage.PerformLayout();
            this.SelectionBox_tabPage.ResumeLayout(false);
            this.SelectionBox_tabPage.PerformLayout();
            this.Progress_tabPage.ResumeLayout(false);
            this.Progress_tabPage.PerformLayout();
            this.DGV_tabPage.ResumeLayout(false);
            this.DGV_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Calendar_tabPage.ResumeLayout(false);
            this.Graph_tabPage.ResumeLayout(false);
            this.Graph_tabPage.PerformLayout();
            this.ContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ModernGUI.Controls.RaisedButton Button1;
        private ModernGUI.Controls.FlatButton FlatButton1;
        private ModernGUI.Controls.SingleLineTextField SingleLineTextField1;
        private ModernGUI.Controls.SingleLineTextField SingleLineTextField2;
        private ModernGUI.Controls.Checkbox Checkbox1;
        private ModernGUI.Controls.Checkbox Checkbox2;
        private ModernGUI.Controls.Checkbox Checkbox3;
        private ModernGUI.Controls.Checkbox Checkbox4;
        private ModernGUI.Controls.FlatButton FlatButton2;
        private ModernGUI.Controls.Divider Divider1;
        private TabSelector TabSelector1;
        private ModernGUI.Controls.TabControl  TabControl1;
        private System.Windows.Forms.TabPage TextField_tabPage;
        private System.Windows.Forms.TabPage SelectionBox_tabPage;
        private ModernGUI.Controls.Checkbox CheckBox5;
        private ModernGUI.Controls.ContextMenuStrip ContextMenuStrip1;
        private ModernGUI.Controls.ToolStripMenuItem item1ToolStripMenuItem;
        private ModernGUI.Controls.ToolStripMenuItem subItem1ToolStripMenuItem;
        private ModernGUI.Controls.ToolStripMenuItem subItem2ToolStripMenuItem;
        private ModernGUI.Controls.ToolStripMenuItem item2ToolStripMenuItem;
        private ModernGUI.Controls.ToolStripMenuItem item3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem disabledItemToolStripMenuItem;
		private ModernGUI.Controls.Checkbox CheckBox6;
		private ModernGUI.Controls.RaisedButton RaisedButton1;
        private ModernGUI.Controls.SingleLineTextField SingleLineTextField3;
		private TabPage gantt_tabPage;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private ColumnHeader columnHeader4;
        private TabPage Progress_tabPage;
        private ModernGUI.Controls.ProgressBar ProgressBar1;
        private ModernGUI.Controls.RaisedButton RaisedButton2;
        private ModernGUI.Controls.FlatButton FlatButton3;
        private ModernGUI.Controls.FlatButton FlatButton4;
        private ModernGUI.Controls.Label Label2;
        private TabPage DGV_tabPage;
        private ModernGUI.Controls.DataGridView dataGridView1;
        private TabPage Calendar_tabPage;
        private Calendar calendar1;
        private TabPage tabPage3;
        private MonthView monthView1;
        private ModernGUI.Controls.Columns.AddRemoveColumn AddRemove;
        private ModernGUI.Controls.Columns.AddRemoveColumn addRemoveColumn1;
        private ModernGUI.Controls.RadioButton RadioButton4;
        private ModernGUI.Controls.RadioButton RadioButton1;
        private ModernGUI.Controls.RadioButton RadioButton2;
        private ModernGUI.Controls.RadioButton RadioButton3;
        private TabPage Graph_tabPage;
        private MultiSelectTreeview multiSelectTreeview1;
        private ModernGUI.Controls.Label label1;
        private RaisedButton ReadonlyDGV_raisedButton;
        private RaisedButton MoveDGV_raisedButton;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private ModernGUI.Controls.Columns.AddRemoveColumn Column3;
        private RaisedButton DGVListBoxStyle_raisedButton;
    }
}