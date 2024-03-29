﻿using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ModernGUI.Controls
{
    public class MenuStrip : System.Windows.Forms.MenuStrip, IControl
    {
        public int Depth { get; set; }
        public SkinManager SkinManager { get { return SkinManager.Instance; } }
        public MouseState MouseState { get; set; }

        public MenuStrip()
        {
            Renderer = new MenuStripRender();

            if (DesignMode)
            {
                Dock = DockStyle.None;
                Anchor |= AnchorStyles.Right;
                AutoSize = false;
                Location = new Point(0, 28);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = SkinManager.openSans[10, OpenSans.Weight.Medium];
            BackColorChanged += (sender, args) => ForeColor = SkinManager.GetPrimaryTextColor();
        }
    }

    internal class MenuStripRender : ToolStripProfessionalRenderer, IControl
    {
        //Properties for managing the design properties
        public int Depth { get; set; }
        public SkinManager SkinManager { get { return SkinManager.Instance; } }
        public MouseState MouseState { get; set; }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            if (e.Item.IsOnDropDown)
            {
                var itemRect = GetItemRect(e.Item);
                var textRect = new Rectangle(24, itemRect.Y, itemRect.Width - (24 + 16), itemRect.Height);
                g.DrawString(e.Text, SkinManager.openSans[10, OpenSans.Weight.Medium], e.Item.Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush(), textRect, new StringFormat() { LineAlignment = StringAlignment.Center });
            }
            else
            {
                g.DrawString(e.Text, SkinManager.openSans[10, OpenSans.Weight.Medium], Brushes.White, e.TextRectangle, new StringFormat() { LineAlignment = StringAlignment.Center });
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(SkinManager.GetApplicationBackgroundColor());


            //Draw background
            var itemRect = GetItemRect(e.Item);
            g.FillRectangle(e.Item.Selected && e.Item.Enabled ? SkinManager.GetCmsSelectedItemBrush() : new SolidBrush(SkinManager.GetApplicationBackgroundColor()), itemRect);

            //Ripple animation
            var toolStrip = (ModernGUI.Controls.ContextMenuStrip)e.ToolStrip;
            if (toolStrip != null)
            {
                var animationManager = toolStrip.AnimationManager;
                var animationSource = toolStrip.AnimationSource;
                if (toolStrip.AnimationManager.IsAnimating() && e.Item.Bounds.Contains(animationSource))
                {
                    for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                    {
                        var animationValue = animationManager.GetProgress(i);
                        var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationValue * 50)), Color.Black));
                        var rippleSize = (int)(animationValue * itemRect.Width * 2.5);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, itemRect.Y - itemRect.Height, rippleSize, itemRect.Height * 3));
                    }
                }
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            //base.OnRenderImageMargin(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            var g = e.Graphics;

            g.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), e.Item.Bounds);
            g.DrawLine(new Pen(SkinManager.GetDividersColor()), new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2), new Point(e.Item.Bounds.Right, e.Item.Bounds.Height / 2));
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            //var g = e.Graphics;

            //g.DrawRectangle(new Pen(SkinManager.GetDividersColor()), new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var g = e.Graphics;
            const int ARROW_SIZE = 4;

            var arrowMiddle = new Point(e.ArrowRectangle.X + e.ArrowRectangle.Width / 2, e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2);
            var arrowBrush = e.Item.Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush();
            using (var arrowPath = new GraphicsPath())
            {
                arrowPath.AddLines(new[] { new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y - ARROW_SIZE), new Point(arrowMiddle.X, arrowMiddle.Y), new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y + ARROW_SIZE) });
                arrowPath.CloseFigure();

                g.FillPath(arrowBrush, arrowPath);
            }
        }

        private Rectangle GetItemRect(ToolStripItem item)
        {
            return new Rectangle(0, item.ContentRectangle.Y, item.ContentRectangle.Width + 4, item.ContentRectangle.Height);
        }
    }
}
