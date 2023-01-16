﻿using ModernGUI;
using ModernGUI.Controls;
using System.Drawing.Drawing2D;

namespace ModernGUITest
{
    public partial class Example : ModernGUI.Controls.Form
    {
        private readonly SkinManager _SkinManager;

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Example));

        public Example()
        {
            InitializeComponent();
 
            // Initialize SkinManager
            _SkinManager = SkinManager.Instance;
            _SkinManager.AddFormToManage(this);
            _SkinManager.Theme = SkinManager.Themes.LIGHT;
            _SkinManager.ColorScheme = new ColorScheme(ColorSchemes.BlueGrey);
        }

        private void Blur_flatButton_Click(object sender, EventArgs e)
        {
            this.Blur();
            UnBlur_flatButton.BringToFront();
        }

        private void UnBlur_flatButton_Click(object sender, EventArgs e)
        {
            this.UnBlur();
        }
    }
}
