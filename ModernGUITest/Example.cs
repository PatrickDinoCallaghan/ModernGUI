﻿using ModernGUI;
using ModernGUI.Controls;
using System.Drawing.Drawing2D;

namespace ModernGUITest
{
    public partial class Example : ModernGUI.Controls.Form
    {
        private readonly SkinManager _SkinManager;
        public Example()
        {
            InitializeComponent();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Example));

            // Initialize MaterialSkinManager
            _SkinManager = SkinManager.Instance;
            _SkinManager.AddFormToManage(this);
            _SkinManager.Theme = SkinManager.Themes.LIGHT;
            _SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Indigo);
        }

        public Bitmap ResizeImage(double scaleFactor, Image image)
        {
            var newWidth = 15;
            var newHeight = 15;
            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);


            //   thumbnailBitmap.Save(toStream, image.RawFormat);

            return null;
            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
        }
        private void Example_Load(object sender, EventArgs e)
        {
        }

        private void spinner1_ButtonClick(object sender, Spinner.ButtonClicked e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}
