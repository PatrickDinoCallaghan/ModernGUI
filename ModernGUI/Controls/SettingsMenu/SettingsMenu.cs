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
    internal partial class SettingsMenu : RoundButton
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private readonly AnimationManager _animationManager;

        private ModernGUI.Controls.ContextMenuStrip cms;
        private ToggleSwitch Dark_ToggleSwitch;

        private ModernGUI.Controls.ToolStripMenuItem ThemeToolStripMenuItem;
        private CustomContextMenuStripItem DarkModeToolStripMenuItem;


        public SettingsMenu()
        {
            this.Text = "";
            this.BackColor = SkinManager.ColorScheme.PrimaryColor;

            _animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            this.Icon = global::ModernGUI.Properties.Resources.Settings;

            InitializeComponent();
            SetupMenu();
        }

        private void SetupMenu()
        {
            cms = new ContextMenuStrip();
            Dark_ToggleSwitch = new ToggleSwitch();

            Dark_ToggleSwitch.OffText = "Off";
            Dark_ToggleSwitch.OnText = "On";
            ThemeToolStripMenuItem = new ModernGUI.Controls.ToolStripMenuItem();
            DarkModeToolStripMenuItem = new CustomContextMenuStripItem(Dark_ToggleSwitch);
            //This ensures the cms doesn't hide when an item is clicked.
            cms.Closing += (o, e) =>
            {
                if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                    e.Cancel = true;
            };

            cms.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            cms.MouseState = ModernGUI.MouseState.HOVER;
            cms.Name = "ContextMenuStrip1";
            cms.Size = new System.Drawing.Size(130, 130);
            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ThemeToolStripMenuItem,
            DarkModeToolStripMenuItem});
            // 
            // ThemeToolStripMenuItem
            // 
            ThemeToolStripMenuItem.AutoSize = true;
            ThemeToolStripMenuItem.Name = "Theme";
            ThemeToolStripMenuItem.Text = "Change Color Scheme";
            ThemeToolStripMenuItem.Click += ThemeToolStripMenuItem_Click;
            // 
            // DarkModeToolStripMenuItem
            // 
            DarkModeToolStripMenuItem.AutoSize = true;
            DarkModeToolStripMenuItem.Name = "DarkModeToolStripMenuItem";
            DarkModeToolStripMenuItem.Text = "Dark Mode";
            DarkModeToolStripMenuItem.Size = new System.Drawing.Size(130, 130);
            Dark_ToggleSwitch.CheckedChanged += Dark_ToggleSwitch_CheckedChanged;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            _animationManager.StartNewAnimation(ModernGUI.Animations.AnimationDirection.In, mevent.Location);
       
            if (SkinManager.Theme == SkinManager.Themes.LIGHT)
            { Dark_ToggleSwitch.Checked = false; }
            else { Dark_ToggleSwitch.Checked = true;}

            Point point = new Point(this.Location.X, this.Location.Y + this.Height);
            cms.Show(this.PointToScreen(new Point(0, this.Height)));
        }

        private void Dark_ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (Dark_ToggleSwitch.Checked)
            {
                SkinManager.Theme = SkinManager.Themes.DARK;
            }
            else
            {

                SkinManager.Theme = SkinManager.Themes.LIGHT;
            }
         //   SkinManager.Theme = SkinManager.Theme == SkinManager.Themes.DARK ? SkinManager.Themes.LIGHT : SkinManager.Themes.DARK;
            cms.Invalidate();
        }

        private void ThemeToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            ModernGUI.Shared.DrawingControl.SuspendDrawing(this);
            ModernGUI.Shared.DrawingControl.SuspendDrawing(this.Parent);
            int colorSchemeIndex = (int)SkinManager.ColorScheme.CurrentScheme + 1;

            if (colorSchemeIndex > 3) colorSchemeIndex = 0;

            SkinManager.ColorScheme = new ColorScheme((ModernGUI.ColorSchemes)(colorSchemeIndex));

            //These are just example color scheme
            switch (colorSchemeIndex)
            {
                case 0:
                    SkinManager.ColorScheme = new ColorScheme(ColorSchemes.BlueGrey);
                    break;
                case 1:
                    SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Indigo);
                    break;
                case 2:
                    SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Green);
                    break;
                case 3:
                    SkinManager.ColorScheme = new ColorScheme(ColorSchemes.Red);
                    break;
            }

            ModernGUI.Shared.DrawingControl.ResumeDrawing(this);
            ModernGUI.Shared.DrawingControl.ResumeDrawing(this.Parent);
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
