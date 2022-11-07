using System.ComponentModel;

namespace ModernGUI.Controls
{
    [ToolboxItem(false)]
    public partial class AddRemove : UserControl
    {
        private System.Windows.Forms.Button Remove_Btn;
        private System.Windows.Forms.Button Add_Btn;

        public RemoveClicked OnRemoveClicked;

        public AddClicked OnAddClicked;

        public int RowIndex { get; set; }
        public AddRemove()
        {
            InitializeComponent();
        }

        #region Each Button Events

        private void Remove_Btn_Click(object sender, EventArgs e)
        {
            OnRemoveClicked?.Invoke(RowIndex);
        }

        private void Add_Btn_Click(object sender, EventArgs e)
        {
            OnAddClicked?.Invoke(RowIndex);
        }

        #endregion

        #region Component Designer

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AddRemove));
            Remove_Btn = new System.Windows.Forms.Button();
            Add_Btn = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // Remove_Btn
            // 
            Remove_Btn.BackgroundImage = (Image)resources.GetObject("Remove_Btn.BackgroundImage");
            Remove_Btn.BackgroundImageLayout = ImageLayout.Center;
            Remove_Btn.FlatAppearance.BorderColor = SystemColors.Control;
            Remove_Btn.FlatAppearance.BorderSize = 0;
            Remove_Btn.FlatAppearance.MouseDownBackColor = SystemColors.InactiveCaption;
            Remove_Btn.FlatStyle = FlatStyle.Flat;
            Remove_Btn.Location = new Point(41, 0);
            Remove_Btn.Name = "Remove_Btn";
            Remove_Btn.Size = new Size(40, 39);
            Remove_Btn.TabIndex = 4;
            Remove_Btn.UseVisualStyleBackColor = true;
            Remove_Btn.Click += new EventHandler(Remove_Btn_Click);
            // 
            // Add_Btn
            // 
            Add_Btn.BackgroundImage = (Image)resources.GetObject("Add_Btn.BackgroundImage");
            Add_Btn.BackgroundImageLayout = ImageLayout.Center;
            Add_Btn.FlatAppearance.BorderColor = SystemColors.Control;
            Add_Btn.FlatAppearance.BorderSize = 0;
            Add_Btn.FlatAppearance.MouseDownBackColor = SystemColors.InactiveCaption;
            Add_Btn.FlatStyle = FlatStyle.Flat;
            Add_Btn.Location = new Point(0, 0);
            Add_Btn.Name = "Add_Btn";
            Add_Btn.Size = new Size(40, 39);
            Add_Btn.TabIndex = 3;
            Add_Btn.UseVisualStyleBackColor = true;
            Add_Btn.Click += new EventHandler(Add_Btn_Click);
            // 
            // AddRemove
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Remove_Btn);
            Controls.Add(Add_Btn);
            Name = "AddRemove";
            Size = new Size(80, 39);
            ResumeLayout(false);

        }

        #endregion

        public delegate void RemoveClicked(int RowIndex);

        public delegate void AddClicked(int RowIndex);

    }

}
