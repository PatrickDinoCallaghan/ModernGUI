using Microsoft.Office.Interop.Outlook;

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

        /// <summary>
        /// This returns a highlighted version of color
        /// </summary>
        /// <param name="color">Original color to highlight</param>
        /// <returns></returns>
        public static Color HighlightColor(Color color, float Highlight_Factor) 
        {
            HSLColor hSLColor = HSLColor.FromColor(color);
            return hSLColor.HighlighedColor(Highlight_Factor);
        }

    }
    public class HSLColor
    {
        public float H;
        public float S;
        public float L;

        /// <summary>
        /// Hue, saturation, lightness When 0 ≤ H < 360, 0 ≤ S ≤ 1 and 0 ≤ L ≤ 1:
        /// </summary>
        public HSLColor(float _H, float _S, float _L)
        {
            H = _H;
            S = _S;
            L = _L;
        }

        public static HSLColor FromColor(Color color)
        {
            return FromRGB(color.R, color.G, color.B);
        }

        /// <summary>
        /// Red, Green, Blue.
        /// </summary>
        /// <param name="R">Red</param>
        /// <param name="G">green</param>
        /// <param name="B">Blue</param>
        /// <returns></returns>
        public static HSLColor FromRGB(Byte R, Byte G, Byte B)
        {
            float _R = (R / 255f);
            float _G = (G / 255f);
            float _B = (B / 255f);

            float _Min = Math.Min(Math.Min(_R, _G), _B);
            float _Max = Math.Max(Math.Max(_R, _G), _B);
            float _Delta = _Max - _Min;

            float H = 0;
            float S = 0;
            float L = (float)((_Max + _Min) / 2.0f);

            if (_Delta != 0)
            {
                if (L < 0.5f)
                {
                    S = (float)(_Delta / (_Max + _Min));
                }
                else
                {
                    S = (float)(_Delta / (2.0f - _Max - _Min));
                }
                if (_R == _Max)
                {
                    H = (_G - _B) / _Delta;
                }
                else if (_G == _Max)
                {
                    H = 2f + (_B - _R) / _Delta;
                }
                else if (_B == _Max)
                {
                    H = 4f + (_R - _G) / _Delta;
                }
            }

            return new HSLColor(H, S, L);
        }

        public Color ToColor()
        {
            byte r, g, b;
            if (S == 0)
            {
                r = (byte)Math.Round(L * 255d);
                g = (byte)Math.Round(L * 255d);
                b = (byte)Math.Round(L * 255d);
            }
            else
            {
                double t1, t2;
                double th = H / 6.0d;

                if (L < 0.5d)
                {
                    t2 = L * (1d + S);
                }
                else
                {
                    t2 = (L + S) - (L * S);
                }
                t1 = 2d * L - t2;

                double tr, tg, tb;
                tr = th + (1.0d / 3.0d);
                tg = th;
                tb = th - (1.0d / 3.0d);

                tr = ColorCalc(tr, t1, t2);
                tg = ColorCalc(tg, t1, t2);
                tb = ColorCalc(tb, t1, t2);
                r = (byte)Math.Round(tr * 255d);
                g = (byte)Math.Round(tg * 255d);
                b = (byte)Math.Round(tb * 255d);
            }
            return Color.FromArgb(r, g, b);
        }

        private double ColorCalc(double c, double t1, double t2)
        {

            if (c < 0) c += 1d;
            if (c > 1) c -= 1d;
            if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
            if (2.0d * c < 1.0d) return t2;
            if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
            return t1;
        }

        public Color HighlighedColor(float Highlight_Factor)
        {
            HSLColor Highlight = new HSLColor(H, S, L);

            Highlight.L = this.L * Highlight_Factor;

            return Highlight.ToColor();
        }
    }
}
