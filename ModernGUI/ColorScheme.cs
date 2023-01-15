namespace ModernGUI
{
    public enum ColorSchemes
    {
        BlueGrey,
        Indigo,
        Green,
        Red
    }
    //Color constantes
    public enum TextShade
    {
        WHITE = 0xFFFFFF,
        BLACK = 0x212121
    }

    public class ColorScheme
    {
        public readonly Color PrimaryColor, DarkPrimaryColor, LightPrimaryColor, AccentColor, TextColor;
        public readonly Pen PrimaryPen, DarkPrimaryPen, LightPrimaryPen, AccentPen, TextPen;
        public readonly Brush PrimaryBrush, DarkPrimaryBrush, LightPrimaryBrush, AccentBrush, TextBrush;

        public ColorSchemes CurrentScheme;

        /// <summary>
        /// Defines the Color Scheme to be used for all forms.
        /// </summary>
        /// <param name="ColorSchemes">ColorScheme enum.</param>
        /// 
        public ColorScheme(ColorSchemes scheme)
        {
            int primary;
            int darkPrimary;
            int lightPrimary;
            int accent;
            TextShade textShade;

            switch (scheme)
            {
                case ColorSchemes.Indigo:
                    primary = 0x3F51B5;
                    darkPrimary = 0x303F9F;
                    lightPrimary = 0xC5CAE9;
                    accent = 0xFF4081;
                    textShade = TextShade.WHITE;
                    break;
                case ColorSchemes.Green:
                    primary = 0x43A047;
                    darkPrimary = 0x388E3C;
                    lightPrimary = 0xA5D6A7;
                    accent = 0xFFCDD2;
                    textShade = TextShade.WHITE;
                    break;
                case ColorSchemes.Red:
                    primary = 0xED3419;
                    darkPrimary = 0xDF2C14;
                    lightPrimary = 0xFF4122;
                    accent = 0xFFCDD2;
                    textShade = TextShade.WHITE;
                    break;
                default:
                    primary = 0x37474F;
                    darkPrimary = 0x263238;
                    lightPrimary = 0x607D8B;
                    accent = 0x81D4FA;
                    textShade = TextShade.WHITE;
                    break;
            }

            //Color
            PrimaryColor = (primary).ToColor();
            DarkPrimaryColor = (darkPrimary).ToColor();
            LightPrimaryColor = (lightPrimary).ToColor();
            AccentColor = (accent).ToColor();
            TextColor = ((int)textShade).ToColor();

            //Pen
            PrimaryPen = new Pen(PrimaryColor);
            DarkPrimaryPen = new Pen(DarkPrimaryColor);
            LightPrimaryPen = new Pen(LightPrimaryColor);
            AccentPen = new Pen(AccentColor);
            TextPen = new Pen(TextColor);

            //Brush
            PrimaryBrush = new SolidBrush(PrimaryColor);
            DarkPrimaryBrush = new SolidBrush(DarkPrimaryColor);
            LightPrimaryBrush = new SolidBrush(LightPrimaryColor);
            AccentBrush = new SolidBrush(AccentColor);
            TextBrush = new SolidBrush(TextColor);

            CurrentScheme = scheme;
        }
    }

    public static class ColorExtension
    {
        /// <summary>
        /// Convert an integer number to a Color.
        /// </summary>
        /// <returns></returns>
        public static Color ToColor(this int argb)
        {
            return Color.FromArgb(
                (argb & 0xff0000) >> 16,
                (argb & 0xff00) >> 8,
                 argb & 0xff);
        }

        /// <summary>
        /// Removes the alpha component of a color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color RemoveAlpha(this Color color)
        {
            return Color.FromArgb(color.R, color.G, color.B);
        }

        /// <summary>
        /// Converts a 0-100 integer to a 0-255 color component.
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public static int PercentageToColorComponent(this int percentage)
        {
            return (int)((percentage / 100d) * 255d);
        }
    }

}
