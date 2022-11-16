using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModernGUI.Animations;
using System.Runtime.InteropServices;

namespace ModernGUI.Controls.TimeControls
{
    [DefaultEvent("OnValueChanged")]
    public partial class TimePicker : UserControl, IControl
    {
        //Properties for managing the design properties
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private readonly AnimationManager _animationManager;

        [Browsable(true)] 
        public event EventHandler OnValueChanged;

        public int Hours
        {
            get
            {
                string Part = _baseTextBox.Text.Split(":".ToCharArray())[0];
                int Value = 0;
                int.TryParse(Part, out Value);
                return Value;
            }
            set
            {
                string[] Parts = _baseTextBox.Text.Split(":".ToCharArray());
                string[] SecondParts = Parts[2].Split(".".ToCharArray());

                Parts[0] = value.ToString("D2");

                SetText(Parts[0], Parts[1], SecondParts[0], SecondParts[1]);
            }
        }
        public int Minutes
        {
            get
            {
                string Part = _baseTextBox.Text.Split(":".ToCharArray())[1];
                int Value = 0;
                int.TryParse(Part, out Value);
                return Value;
            }
            set
            {
                string[] Parts = _baseTextBox.Text.Split(":".ToCharArray());
                string[] SecondParts = Parts[2].Split(".".ToCharArray());

                Parts[1] = value.ToString("D2");

                SetText(Parts[0], Parts[1], SecondParts[0], SecondParts[1]);
            }
        }
        public int Seconds
        {
            get
            {
                string Part = _baseTextBox.Text.Split(":".ToCharArray())[2].Split(".".ToCharArray())[0];
                int Value = 0;
                int.TryParse(Part, out Value);
                return Value;
            }
            set
            {
                string[] Parts = _baseTextBox.Text.Split(":".ToCharArray());
                string[] SecondParts = Parts[2].Split(".".ToCharArray());

                SecondParts[0] = value.ToString("D2");

                SetText(Parts[0], Parts[1], SecondParts[0], SecondParts[1]);
            }
        }
        public int Milliseconds
        {
            get
            {
                string Part = _baseTextBox.Text.Split(":".ToCharArray())[2].Split(".".ToCharArray())[1];
                int Value = 0;
                int.TryParse(Part, out Value);
                return Value;
            }
            set
            {
                string[] Parts = _baseTextBox.Text.Split(":".ToCharArray());
                string[] SecondParts = Parts[2].Split(".".ToCharArray());

                SecondParts[1] = value.ToString("D3");

                SetText(Parts[0], Parts[1], SecondParts[0], SecondParts[1]);
            }
        }
        public TimeSpan Value
        {
            get
            {
                return new TimeSpan(0, Hours, Minutes, Seconds, Milliseconds);
            }
            set
            {
                Hours = value.Hours;
                Minutes = value.Minutes;
                Seconds = value.Seconds;
                Milliseconds = value.Milliseconds;
            }
        }

        public TimePicker()
        {
            InitializeComponent();

            this.LostFocus += new EventHandler(_baseTextBox_LostFocus);

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);

            _animationManager = new AnimationManager
            {
                Increment = 0.06,
                AnimationType = AnimationType.EaseInOut,
                InterruptAnimation = false
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            _baseTextBox = new BaseTextBox
            {
                BorderStyle = BorderStyle.None,
                Font = SkinManager.openSans[11, OpenSans.Weight.Regular],
                ForeColor = SkinManager.GetPrimaryTextColor(),
                Location = new Point(0, 0),
                Width = this.Width-16,
                Height = Height - 2
            };

            if (!Controls.Contains(_baseTextBox) && !DesignMode)
            {
                Controls.Add(_baseTextBox);
            }

            _baseTextBox.GotFocus += (sender, args) => _animationManager.StartNewAnimation(AnimationDirection.In);
            _baseTextBox.LostFocus += (sender, args) => _animationManager.StartNewAnimation(AnimationDirection.Out);
            BackColorChanged += (sender, args) =>
            {
                _baseTextBox.BackColor = BackColor;
                _baseTextBox.ForeColor = SkinManager.GetPrimaryTextColor();
            };

            //Fix for tabstop

            _baseTextBox.TabStop = true;
            this.TabStop = false;

            this._baseTextBox.Text = "00:00:00.000";

            _baseTextBox.LostFocus += new EventHandler(_baseTextBox_LostFocus);
            _baseTextBox.KeyDown += _baseTextBox_KeyDown;
            _baseTextBox.Click += _baseTextBox_Click;
            _baseTextBox.TextChanged += _baseTextBox_TextChanged;

            this.Height = _baseTextBox.Height + 3;

            spinner1.ButtonClick += Spinner1_ButtonClick;
        }

        private void Spinner1_ButtonClick(object sender, Spinner.ButtonClicked e)
        {
            int increment = 0;
            if (e == Spinner.ButtonClicked.Up)
            {
                increment = 1;
            }
            else if (e == Spinner.ButtonClicked.Down)
            {
                increment = -1;
            }

            if (_baseTextBox.SelectionStart <= 2)
            {

                Value = Value.Add(new TimeSpan(increment, 0, 0));
                _baseTextBox.Select(0, 2);

            }
            else if (_baseTextBox.SelectionStart <= 5)
            {
                Value = Value.Add(new TimeSpan(0, increment, 0));
                _baseTextBox.Select(3, 2);
            }
            else if (_baseTextBox.SelectionStart <= 8)
            {
                Value = Value.Add(new TimeSpan(0, 0, increment));
                _baseTextBox.Select(6, 2);
            }
            else
            {
                Value = Value.Add(new TimeSpan(0, 0, 0, 0, increment));
                _baseTextBox.Select(10, 3);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_baseTextBox != null)
            {
                _baseTextBox.Width = this.Width - 16;
                this.Height = _baseTextBox.Height +3;

            }
            base.OnSizeChanged(e);
        }
        private void _baseTextBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                FormatText();
            }
            catch
            {
            }
        }
    
        private void FormatText()
        {
            string[] Parts = _baseTextBox.Text.Trim().Split(":".ToCharArray());
            
            int Hours = 0;
            if (!int.TryParse(Parts[0], out Hours))
            {
                Hours = 0;
            }
            if (Hours >= 24)
            {
                Hours = 0;
            }

            int Minutes = 0;
            if (!int.TryParse(Parts[1], out Minutes))
            {
                Minutes = 0;
            }
            if (Minutes >= 60)
            {
                Minutes = 0;
            }

            string[] SecondParts = Parts[2].Split(".".ToCharArray());
            
            int Seconds = 0;
            if (!int.TryParse(SecondParts[0], out Seconds))
            {
                Seconds = 0;
            }
            if (Seconds >= 60)
            {
                Seconds = 0;
            }

            int Milliseconds = 0;
            if (!int.TryParse(SecondParts[1], out Milliseconds))
            {
                Milliseconds = 0;
            }
            if (Milliseconds >= 1000)
            {
                Milliseconds = 0;
            }

            SetText(Hours.ToString("D2"),Minutes.ToString("D2"), Seconds.ToString("D2"), Milliseconds.ToString("D3"));
            if (OnValueChanged != null)
            {
                OnValueChanged.Invoke(null, new EventArgs());
            }
        }
        bool DoingFormatting = false;

        private void SetText(string Hour, string Minute, string Second, string Millisecond)
        {
            int SelectedIndex = _baseTextBox.SelectionStart;
            _baseTextBox.Text = Hour + ":" + Minute + ":" + Second + "." + Millisecond;
            SelectedIndex = SelectedIndex > _baseTextBox.Text.Length ? _baseTextBox.Text.Length : SelectedIndex;
            SelectCorrectText(SelectedIndex);
            if (!DoingFormatting)
            {
                DoingFormatting = true;
                FormatText();
                DoingFormatting = false;
            }
        }

        private void _baseTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                string[] Parts = _baseTextBox.Text.Split(":".ToCharArray());
                string[] SecondParts = Parts[2].Split(".".ToCharArray());

                if (_baseTextBox.SelectionStart <= 2)
                {
                    int PartNum = 0;
                    if (int.TryParse(Parts[0], out PartNum))
                    {
                        PartNum++;
                        if (PartNum >= 100)
                        {
                            PartNum = 0;
                        }
                        Parts[0] = PartNum.ToString("D2");
                    }
                }
                else if (_baseTextBox.SelectionStart <= 5)
                {
                    int PartNum = 0;
                    if (int.TryParse(Parts[1], out PartNum))
                    {
                        PartNum++;
                        if (PartNum >= 60)
                        {
                            PartNum = 0;
                        }
                        Parts[1] = PartNum.ToString("D2");
                    }
                }
                else if (_baseTextBox.SelectionStart <= 8)
                {
                    int PartNum = 0;
                    if (int.TryParse(SecondParts[0], out PartNum))
                    {
                        PartNum++;
                        if (PartNum >= 60)
                        {
                            PartNum = 0;
                        }
                        SecondParts[0] = PartNum.ToString("D2");
                    }
                }
                else
                {
                    int PartNum = 0;
                    if (int.TryParse(SecondParts[1], out PartNum))
                    {
                        PartNum++;
                        if (PartNum >= 1000)
                        {
                            PartNum = 0;
                        }
                        SecondParts[1] = PartNum.ToString("D3");
                    }
                }
                SetText(Parts[0], Parts[1], SecondParts[0], SecondParts[1]);
                FormatText();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                string[] Parts = _baseTextBox.Text.Split(":".ToCharArray());
                string[] SecondParts = Parts[2].Split(".".ToCharArray());

                if (_baseTextBox.SelectionStart <= 2)
                {
                    int PartNum = 0;
                    if (int.TryParse(Parts[0], out PartNum))
                    {
                        PartNum--;
                        if (PartNum < 0)
                        {
                            PartNum = 23;
                        }
                        Parts[0] = PartNum.ToString("D2");
                    }
                }
                else if (_baseTextBox.SelectionStart <= 5)
                {
                    int PartNum = 0;
                    if (int.TryParse(Parts[1], out PartNum))
                    {
                        PartNum--;
                        if (PartNum < 0)
                        {
                            PartNum = 59;
                        }
                        Parts[1] = PartNum.ToString("D2");
                    }
                }
                else if (_baseTextBox.SelectionStart <= 8)
                {
                    int PartNum = 0;
                    if (int.TryParse(SecondParts[0], out PartNum))
                    {
                        PartNum--;
                        if (PartNum < 0)
                        {
                            PartNum = 59;
                        }
                        SecondParts[0] = PartNum.ToString("D2");
                    }
                }
                else
                {
                    int PartNum = 0;
                    if (int.TryParse(SecondParts[1], out PartNum))
                    {
                        PartNum--;
                        if (PartNum < 0)
                        {
                            PartNum = 999;
                        }
                        SecondParts[1] = PartNum.ToString("D3");
                    }
                }
                SetText(Parts[0], Parts[1], SecondParts[0], SecondParts[1]);
                FormatText();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                FormatText();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                FormatText();
                if (_baseTextBox.SelectionStart <= 2)
                {
                    _baseTextBox.Select(9, 3);
                }
                else if (_baseTextBox.SelectionStart <= 5)
                {
                    _baseTextBox.Select(0, 2);
                }
                else if (_baseTextBox.SelectionStart <= 8)
                {
                    _baseTextBox.Select(3, 2);
                }
                else
                {
                    _baseTextBox.Select(6, 2);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                FormatText();
                if (_baseTextBox.SelectionStart <= 2)
                {
                    _baseTextBox.Select(3, 2);
                }
                else if (_baseTextBox.SelectionStart <= 5)
                {
                    _baseTextBox.Select(6, 2);
                }
                else if (_baseTextBox.SelectionStart <= 8)
                {
                    _baseTextBox.Select(9, 3);
                }
                else
                {
                    _baseTextBox.Select(0, 2);
                }
                e.Handled = true;
            }
            else if (!((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)))
            {
                NonNumKeyPressed = true;
                e.Handled = true;
            }
        }
        bool NonNumKeyPressed = false;
        private void SelectCorrectText(int SelectedIndex)
        {
            if (SelectedIndex <= 2)
            {
                _baseTextBox.Select(0, 2);
            }
            else if (SelectedIndex <= 5)
            {
                _baseTextBox.Select(3, 2);
            }
            else if (SelectedIndex <= 8)
            {
                _baseTextBox.Select(6, 2);
            }
            else
            {
                _baseTextBox.Select(9, 3);
            }
        }
        private void _baseTextBox_Click(object sender, EventArgs e)
        {
            FormatText();
        }

        private void _baseTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NonNumKeyPressed)
            {
                NonNumKeyPressed = false;
                FormatText();
            }
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.Clear(Parent.BackColor);

            var lineY = this.Height -3;

            if (!_animationManager.IsAnimating())
            {
                //No animation
                g.FillRectangle(this.Focused || _baseTextBox.Focused ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetDividersBrush(), _baseTextBox.Location.X, lineY, this.Width, this.Focused ? 2 : 1);
            }
            else
            {
                //Animate
                int animationWidth = (int)(this.Width * _animationManager.GetProgress());
                int halfAnimationWidth = animationWidth / 2;
                int animationStart = _baseTextBox.Location.X + this.Width / 2;

                //Unfocused background
                g.FillRectangle(SkinManager.GetDividersBrush(), _baseTextBox.Location.X, lineY, this.Width, 1);

                //Animated focus transition
                g.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, animationStart - halfAnimationWidth, lineY, animationWidth, 2);
            }
        }
        private class BaseTextBox : TextBox
        {
            public new void SelectAll()
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    base.Focus();
                    base.SelectAll();
                });
            }

            public new void Focus()
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    base.Focus();
                });
            }

            public BaseTextBox()
            {
                ModernGUI.Controls.ContextMenuStrip cms = new ModernGUI.Controls.ContextMenuStrip();
                cms.Opening += ContextMenuStripOnOpening;
                cms.OnItemClickStart += ContextMenuStripOnItemClickStart;

                ContextMenuStrip = cms;
            }

            private void ContextMenuStripOnItemClickStart(object sender, ToolStripItemClickedEventArgs toolStripItemClickedEventArgs)
            {
                switch (toolStripItemClickedEventArgs.ClickedItem.Text)
                {
                    case "Undo":
                        Undo();
                        break;
                    case "Cut":
                        Cut();
                        break;
                    case "Copy":
                        Copy();
                        break;
                    case "Paste":
                        Paste();
                        break;
                    case "Delete":
                        SelectedText = string.Empty;
                        break;
                    case "Select All":
                        SelectAll();
                        break;
                }
            }

            private void ContextMenuStripOnOpening(object sender, CancelEventArgs cancelEventArgs)
            {
                var strip = sender as TextBoxContextMenuStrip;
                if (strip != null)
                {
                    strip.Undo.Enabled = CanUndo;
                    strip.Cut.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.Copy.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.Paste.Enabled = Clipboard.ContainsText();
                    strip.Delete.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.SelectAll.Enabled = !string.IsNullOrEmpty(Text);
                }
            }
            private class TextBoxContextMenuStrip : ContextMenuStrip
            {
                public readonly ToolStripItem Undo = new ModernGUI.Controls.ToolStripMenuItem { Text = "Undo" };
                public readonly ToolStripItem Seperator1 = new ToolStripSeparator();
                public readonly ToolStripItem Cut = new ModernGUI.Controls.ToolStripMenuItem { Text = "Cut" };
                public readonly ToolStripItem Copy = new ModernGUI.Controls.ToolStripMenuItem { Text = "Copy" };
                public readonly ToolStripItem Paste = new ModernGUI.Controls.ToolStripMenuItem { Text = "Paste" };
                public readonly ToolStripItem Delete = new ModernGUI.Controls.ToolStripMenuItem { Text = "Delete" };
                public readonly ToolStripItem Seperator2 = new ToolStripSeparator();
                public readonly ToolStripItem SelectAll = new ModernGUI.Controls.ToolStripMenuItem { Text = "Select All" };

                public TextBoxContextMenuStrip()
                {
                    Items.AddRange(new[]
                    {
                    Undo,
                    Seperator1,
                    Cut,
                    Copy,
                    Paste,
                    Delete,
                    Seperator2,
                    SelectAll
                });
                }
            }
        }

    }
}
