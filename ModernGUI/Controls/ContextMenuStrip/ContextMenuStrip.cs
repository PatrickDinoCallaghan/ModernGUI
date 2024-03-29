﻿using ModernGUI.Animations;
using System.CodeDom;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ModernGUI.Controls
{
    public class ContextMenuStrip : System.Windows.Forms.ContextMenuStrip, IControl
    {
        //Properties for managing the design properties
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }


        internal AnimationManager AnimationManager;
        internal Point AnimationSource;

        public delegate void ItemClickStart(object sender, ToolStripItemClickedEventArgs e);
        public event ItemClickStart OnItemClickStart;

        public ContextMenuStrip()
        {
            Renderer = new ToolStripRender();

            AnimationManager = new AnimationManager(false)
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };
            AnimationManager.OnAnimationProgress += sender => Invalidate();
            AnimationManager.OnAnimationFinished += sender => OnItemClicked(_delayesArgs);

            BackColor = SkinManager.GetApplicationBackgroundColor();
        }

        protected override void OnMouseUp(MouseEventArgs mea)
        {
            base.OnMouseUp(mea);

            AnimationSource = mea.Location;
        }

        private ToolStripItemClickedEventArgs _delayesArgs;
        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem != null && !(e.ClickedItem is ToolStripSeparator))
            {
                if (e == _delayesArgs)
                {
                    //The event has been fired manualy because the args are the ones we saved for delay
                    base.OnItemClicked(e);
                }
                else
                {
                    //Interrupt the default on click, saving the args for the delay which is needed to display the animaton
                    _delayesArgs = e;

                    //Fire custom event to trigger actions directly but keep cms open
                    OnItemClickStart?.Invoke(this, e);

                    //Start animation
                    AnimationManager.StartNewAnimation(AnimationDirection.In);
                }
            }
        }
    }

    public class ToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem
    {
        public ToolStripMenuItem()
        {
            AutoSize = false;
            Size = new Size(120, 30);
        }

        protected override ToolStripDropDown CreateDefaultDropDown()
        {
            var baseDropDown = base.CreateDefaultDropDown();
            if (DesignMode) return baseDropDown;

            var defaultDropDown = new ContextMenuStrip();
            defaultDropDown.Items.AddRange(baseDropDown.Items);

            return defaultDropDown;
        }
    }

    internal class ToolStripRender : ToolStripProfessionalRenderer, IControl
    {
        //Properties for managing the design properties
        public int Depth { get; set; }
        public SkinManager SkinManager => SkinManager.Instance;
        public MouseState MouseState { get; set; }


        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {

            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            var itemRect = GetItemRect(e.Item);
            var textRect = new Rectangle(30, itemRect.Y, itemRect.Width - (24 + 16), itemRect.Height);
            g.DrawString(
               e.Text,
                 SkinManager.openSans[10, OpenSans.Weight.Medium],
                e.Item.Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush(),
                textRect,
                new StringFormat { LineAlignment = StringAlignment.Center });
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(SkinManager.GetApplicationBackgroundColor());

            //Draw background
            var itemRect = GetItemRect(e.Item);
            g.FillRectangle(e.Item.Selected && e.Item.Enabled ? SkinManager.GetCmsSelectedItemBrush() : new SolidBrush(SkinManager.GetApplicationBackgroundColor()), itemRect);

            //Ripple animation
            var toolStrip = (ContextMenuStrip)e.ToolStrip;
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
            g.DrawLine(
                new Pen(SkinManager.GetDividersColor()),
                new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2),
                new Point(e.Item.Bounds.Right, e.Item.Bounds.Height / 2));
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            var g = e.Graphics;

            g.DrawRectangle(
                new Pen(SkinManager.GetDividersColor()),
                new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var g = e.Graphics;
            const int ARROW_SIZE = 4;

            var arrowMiddle = new Point(e.ArrowRectangle.X + e.ArrowRectangle.Width / 2, e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2);
            var arrowBrush = e.Item.Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush();
            using (var arrowPath = new GraphicsPath())
            {
                arrowPath.AddLines(
                    new[] {
                        new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y - ARROW_SIZE),
                        new Point(arrowMiddle.X, arrowMiddle.Y),
                        new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y + ARROW_SIZE) });
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
