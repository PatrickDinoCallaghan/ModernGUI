using System.Drawing.Drawing2D;


#nullable enable
namespace ModernGUI.Controls
{
    public class StarRatingControl : Control
    {
        protected int m_leftMargin = 2;
        protected int m_rightMargin = 2;
        protected int m_topMargin = 2;
        protected int m_bottomMargin = 2;
        protected int m_starSpacing = 8;
        protected int m_starCount = 5;
        protected Rectangle[] m_starAreas;
        protected bool m_hovering;
        protected int m_hoverStar;
        protected int m_selectedStar;
        protected Color m_outlineColor = Color.DarkGray;
        protected Color m_hoverColor = Color.Yellow;
        protected Color m_selectedColor = Color.RoyalBlue;
        protected int m_outlineThickness = 1;

        public StarRatingControl()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Width = 120;
            this.Height = 18;
            this.m_starAreas = new Rectangle[this.StarCount];
        }

        public int LeftMargin
        {
            get => this.m_leftMargin;
            set
            {
                if (this.m_leftMargin == value)
                    return;
                this.m_leftMargin = value;
                this.Invalidate();
            }
        }

        public int RightMargin
        {
            get => this.m_rightMargin;
            set
            {
                if (this.m_rightMargin == value)
                    return;
                this.m_rightMargin = value;
                this.Invalidate();
            }
        }

        public int TopMargin
        {
            get => this.m_topMargin;
            set
            {
                if (this.m_topMargin == value)
                    return;
                this.m_topMargin = value;
                this.Invalidate();
            }
        }

        public int BottomMargin
        {
            get => this.m_bottomMargin;
            set
            {
                if (this.m_bottomMargin == value)
                    return;
                this.m_bottomMargin = value;
                this.Invalidate();
            }
        }

        public int StarSpacing
        {
            get => this.m_starSpacing;
            set
            {
                if (this.m_starSpacing == value)
                    return;
                this.m_starSpacing = value;
                this.Invalidate();
            }
        }

        public int StarCount
        {
            get => this.m_starCount;
            set
            {
                if (this.m_starCount == value)
                    return;
                this.m_starCount = value;
                this.m_starAreas = new Rectangle[this.m_starCount];
                this.Invalidate();
            }
        }

        public bool IsHovering => this.m_hovering;

        public Color OutlineColor
        {
            get => this.m_outlineColor;
            set
            {
                if (!(this.m_outlineColor != value))
                    return;
                this.m_outlineColor = value;
                this.Invalidate();
            }
        }

        public Color HoverColor
        {
            get => this.m_hoverColor;
            set
            {
                if (!(this.m_hoverColor != value))
                    return;
                this.m_hoverColor = value;
                this.Invalidate();
            }
        }

        public Color SelectedColor
        {
            get => this.m_selectedColor;
            set
            {
                if (!(this.m_selectedColor != value))
                    return;
                this.m_selectedColor = value;
                this.Invalidate();
            }
        }

        public int OutlineThickness
        {
            get => this.m_outlineThickness;
            set
            {
                if (this.m_outlineThickness == value)
                    return;
                this.m_outlineThickness = value;
                this.Invalidate();
            }
        }

        public int HoverStar => this.m_hoverStar;

        public int SelectedStar => this.m_selectedStar;

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.Clear(this.BackColor);
            Rectangle rect = new Rectangle(this.LeftMargin, this.TopMargin, (this.Width - (this.LeftMargin + this.RightMargin + this.StarSpacing * (this.StarCount - 1))) / this.StarCount, this.Height - (this.TopMargin + this.BottomMargin));
            for (int starAreaIndex = 0; starAreaIndex < this.StarCount; ++starAreaIndex)
            {
                this.m_starAreas[starAreaIndex].X = rect.X - this.StarSpacing / 2;
                this.m_starAreas[starAreaIndex].Y = rect.Y;
                this.m_starAreas[starAreaIndex].Width = rect.Width + this.StarSpacing / 2;
                this.m_starAreas[starAreaIndex].Height = rect.Height;
                this.DrawStar(pe.Graphics, rect, starAreaIndex);
                rect.X += rect.Width + this.StarSpacing;
            }
            base.OnPaint(pe);
        }

        protected void DrawStar(Graphics g, Rectangle rect, int starAreaIndex)
        {
            Pen pen = new Pen(this.OutlineColor, (float)this.OutlineThickness);
            Brush brush = !this.m_hovering || this.m_hoverStar <= starAreaIndex ? (this.m_hovering || this.m_selectedStar <= starAreaIndex ? (Brush)new SolidBrush(this.BackColor) : (Brush)new LinearGradientBrush(rect, this.SelectedColor, this.BackColor, LinearGradientMode.ForwardDiagonal)) : (Brush)new LinearGradientBrush(rect, this.HoverColor, this.BackColor, LinearGradientMode.ForwardDiagonal);
            PointF[] points = new PointF[10];
            points[0].X = (float)(rect.X + rect.Width / 2);
            points[0].Y = (float)rect.Y;
            points[1].X = (float)(rect.X + 42 * rect.Width / 64);
            points[1].Y = (float)(rect.Y + 19 * rect.Height / 64);
            points[2].X = (float)(rect.X + rect.Width);
            points[2].Y = (float)(rect.Y + 22 * rect.Height / 64);
            points[3].X = (float)(rect.X + 48 * rect.Width / 64);
            points[3].Y = (float)(rect.Y + 38 * rect.Height / 64);
            points[4].X = (float)(rect.X + 52 * rect.Width / 64);
            points[4].Y = (float)(rect.Y + rect.Height);
            points[5].X = (float)(rect.X + rect.Width / 2);
            points[5].Y = (float)(rect.Y + 52 * rect.Height / 64);
            points[6].X = (float)(rect.X + 12 * rect.Width / 64);
            points[6].Y = (float)(rect.Y + rect.Height);
            points[7].X = (float)(rect.X + rect.Width / 4);
            points[7].Y = (float)(rect.Y + 38 * rect.Height / 64);
            points[8].X = (float)rect.X;
            points[8].Y = (float)(rect.Y + 22 * rect.Height / 64);
            points[9].X = (float)(rect.X + 22 * rect.Width / 64);
            points[9].Y = (float)(rect.Y + 19 * rect.Height / 64);
            g.FillPolygon(brush, points);
            g.DrawPolygon(pen, points);
        }

        protected override void OnMouseEnter(EventArgs ea)
        {
            this.m_hovering = true;
            this.Invalidate();
            base.OnMouseEnter(ea);
        }

        protected override void OnMouseLeave(EventArgs ea)
        {
            this.m_hovering = false;
            this.Invalidate();
            base.OnMouseLeave(ea);
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            for (int index = 0; index < this.StarCount; ++index)
            {
                if (this.m_starAreas[index].Contains(args.X, args.Y))
                {
                    this.m_hoverStar = index + 1;
                    this.Invalidate();
                    break;
                }
            }
            base.OnMouseMove(args);
        }

        protected override void OnClick(EventArgs args)
        {
            Point client = this.PointToClient(Control.MousePosition);
            for (int index = 0; index < this.StarCount; ++index)
            {
                if (this.m_starAreas[index].Contains(client))
                {
                    this.m_hoverStar = index + 1;
                    this.m_selectedStar = index + 1;
                    this.Invalidate();
                    break;
                }
            }
            base.OnClick(args);
        }
    }
}
