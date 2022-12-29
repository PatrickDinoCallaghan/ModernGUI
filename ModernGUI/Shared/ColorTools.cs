namespace ModernGUI.Shared
{
    public static partial class Drawing
    {
        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend)
        {
            var ratio = blend / 255d;
            var invRatio = 1d - ratio;
            var r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            var g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            var b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(r, g, b);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor)
        {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }

        public static Color ColorNegative(Color ColourToInvert)
        {
            int RGBMAX = 255;
            return Color.FromArgb(RGBMAX - ColourToInvert.R,
              RGBMAX - ColourToInvert.G, RGBMAX - ColourToInvert.B);
        }

        public static Bitmap ChangeColor(Bitmap bitmap, Color newColor)
        {
            Bitmap bitmap1 = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)

                {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (pixel.A > (byte)150)
                    {
                        bitmap1.SetPixel(x, y, newColor);
                    }
                    else
                    {
                        bitmap1.SetPixel(x, y, pixel);
                    }

                }
            }
            return bitmap1;
        }
    }
}
