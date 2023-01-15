using ModernGUI.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace ModernGUI.Controls
{
    public partial class FormHeaderButton : RoundButton
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private readonly AnimationManager _animationManager;

        public FormHeaderButton(Image IconImage)
        {
            this.Text = "";
            this.BackColor = SkinManager.ColorScheme.PrimaryColor;

            _animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            this.Icon = IconImage;

            InitializeComponent();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            _animationManager.StartNewAnimation(ModernGUI.Animations.AnimationDirection.In, mevent.Location);
       
            Point point = new Point(this.Location.X, this.Location.Y + this.Height);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            this.BackColor = SkinManager.ColorScheme.PrimaryColor;

            g.Clear(SkinManager.ColorScheme.PrimaryColor);

            using (var backgroundPath = ModernGUI.Shared.Drawing.CreateEllipse(ClientRectangle.X,
                ClientRectangle.Y,
                ClientRectangle.Width - 1,
                ClientRectangle.Height - 1,
                1f))
            {
                g.FillPath(Primary ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetRaisedButtonBackgroundBrush(), backgroundPath);
            }

            if (_animationManager.IsAnimating())
            {
                for (int i = 0; i < _animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = _animationManager.GetProgress(i);
                    var animationSource = _animationManager.GetSource(i);
                    var rippleBrush = new SolidBrush(System.Drawing.Color.FromArgb((int)(51 - (animationValue * 50)), System.Drawing.Color.White));
                    var rippleSize = (int)(animationValue * Width * 2);
                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                }
            }

            var iconRect = new Rectangle(new Point(0,0), this.Size);

            if (Icon != null)
            {
                g.DrawImage(Icon, iconRect);
            }
        }

    }
}
