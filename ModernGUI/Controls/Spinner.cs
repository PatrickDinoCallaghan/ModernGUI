using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernGUI.Controls
{
    public class Spinner : UserControl
    {
        public Spinner()
        {
            InitializeComponent();
        }

        public enum ButtonClicked
        {
            Down,
            Up
        }

        [Browsable(true)]
        public event OnButtonClick ButtonClick;
        public delegate void OnButtonClick (object sender, ButtonClicked e);
        
        #region Design Code
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size =   new Size(16, this.Height);
        }
        Control Updown()
        {
            DomainUpDown domainUpDown1 = new DomainUpDown();

            foreach (Control item in domainUpDown1.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.UpDownBase+UpDownButtons")
                {
                    this.Controls.Remove(item);


                    item.MouseClick += Item_MouseDown;
                    item.LocationChanged += Item_LocationChanged;
                    return item;
                }
                else
                {
                    item.Visible = false;
                }
            }
            return null;
        }
        private void Item_LocationChanged(object? sender, EventArgs e)
        {
            this.UpDownbase.Location = new Point(0, 0);
           // this.Size = new Size(16, UpDownbase.Size.Height);
            UpDownbase.Dock = DockStyle.Fill;
        }

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
            this.SuspendLayout();
            // 
            // Spinner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Spinner";
            this.Size = new System.Drawing.Size(16, 16);

            //
            // Up Down Base
            //
            this.UpDownbase = this.Updown();
            this.UpDownbase.Location = new Point(0,0);
            this.Controls.Add(UpDownbase);

            this.ResumeLayout(false);

        }

        Control UpDownbase;
        #endregion

        private void Item_MouseDown(object? sender, MouseEventArgs e)
        {
    
            if (e.Y < ((Control)sender).Height / 2)
            {
                ButtonClick?.Invoke(this, ButtonClicked.Up);
            }

            if (e.Y > ((Control)sender).Height / 2)
            {
                ButtonClick?.Invoke(this, ButtonClicked.Down);
            }

        }

        #endregion
    }
}
