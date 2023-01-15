using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace ModernGUI.Shared
{
    public static partial class Drawing
    {
        public static class CommonShapes
        {
            public static Point[] ReturnDownArrow(int X, int Y)
            {
                return new Point[] { new Point(0 + X, 0 + Y), new Point(14 + X, 0 + Y), new Point(7 + X, 7 + Y) };
            }
            public static Point[] ReturnUpArrow(int X, int Y)
            {
                return new Point[] { new Point(0 + X, 8 + Y), new Point(16 + X, 8 + Y), new Point(8 + X, 0 + Y) };
            }
            public static PointF[] DownArrow(float X, float Y, float Height, float Width)
            {
                return new PointF[] { new PointF(X, Y), new PointF(Width + X, Y), new PointF((Width / 2) + X, Width + Y) };
            }
            public static Point[] UpArrow(int X, int Y, int Height, int Width)
            {
                return new Point[] { new Point(0 + X, Height / 2 + Y), new Point(Width, Height / 2 + Y), new Point(Width / 2 + X, 0 + Y) };
            }
        }

        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius)
        {
            var gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y);
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(x, y + height - (radius * 2), x, y + radius);
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            return gp;
        }

        public static GraphicsPath CreateEllipse(float x, float y, float width, float height, float radius)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(x, y, width, height);
            grPath.CloseFigure();

            return grPath;

        }

        public static GraphicsPath CreateRoundRect(Rectangle rect, float radius)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }


    }


}
