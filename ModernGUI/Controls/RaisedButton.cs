using ModernGUI.Animations;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ModernGUI.Controls
{
    public class RaisedButton : Button, ModernGUI.IControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public bool Primary { get; set; }

        private readonly AnimationManager _animationManager;

        private SizeF _textSize;

        private Image _icon;
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                if (AutoSize)
                {
                    Size = GetPreferredSize();
                }
                Invalidate();
            }
        }

        public RaisedButton()
        {
            Primary = true;

            _animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                _textSize = CreateGraphics().MeasureString(value.ToUpper(), SkinManager.openSans[10, OpenSans.Weight.Medium]);
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            _animationManager.StartNewAnimation(ModernGUI.Animations.AnimationDirection.In, mevent.Location);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(Parent.BackColor);

            using (var backgroundPath = ModernGUI.Shared.Drawing.CreateRoundRect(ClientRectangle.X,
                ClientRectangle.Y,
                ClientRectangle.Width - 1,
                ClientRectangle.Height - 1,
                1f))
            {
                if (this.Enabled)
                {
                    g.FillPath(Primary ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetRaisedButtonBackgroundBrush(), backgroundPath);
                }
                else
                {
                    g.FillPath(Primary ? new SolidBrush(Shared.Drawing.HighlightColor(((SolidBrush)SkinManager.ColorScheme.PrimaryBrush).Color, 1.5f)) : new SolidBrush(Shared.Drawing.HighlightColor(((SolidBrush)SkinManager.GetRaisedButtonBackgroundBrush()).Color, 0.5f)), backgroundPath);

                }


            }

            if (_animationManager.IsAnimating()&& Enabled == true)
            {
                for (int i = 0; i < _animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = _animationManager.GetProgress(i);
                    var animationSource = _animationManager.GetSource(i);
                    var rippleBrush = new SolidBrush(System.Drawing.Color.FromArgb((int)(51 - (animationValue * 50)), Color.White));
                    var rippleSize = (int)(animationValue * Width * 2);
                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                }
            }

            //Icon
            var iconRect = new Rectangle(8, 6, 24, 24);

            if (string.IsNullOrEmpty(Text))
                // Center Icon
                iconRect.X += 2;

            if (Icon != null)
                g.DrawImage(Icon, iconRect);

            //Text
            var textRect = ClientRectangle;

            if (Icon != null)
            {
                //
                // Resize and move Text container
                //

                // First 8: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                // Third 8: right padding
                textRect.Width -= 8 + 24 + 4 + 8;

                // First 8: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                textRect.X += 8 + 24 + 4;
            }

            g.DrawString(
                Text.ToUpper(),
                 SkinManager.openSans[10, OpenSans.Weight.Medium],
                SkinManager.GetRaisedButtonTextBrush(Primary),
                textRect,
                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private Size GetPreferredSize()
        {
            return GetPreferredSize(new Size(0, 0));
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            // Provides extra space for proper padding for content
            var extra = 16;

            if (Icon != null)
                // 24 is for icon size
                // 4 is for the space between icon & text
                extra += 24 + 4;

            return new Size((int)Math.Ceiling(_textSize.Width) + extra, 36);
        }
    }
}
