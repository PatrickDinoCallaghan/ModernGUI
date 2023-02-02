using ModernGUI.Shared;
using System.ComponentModel;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace ModernGUI.Controls
{
    public class Form : System.Windows.Forms.Form, IControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public new FormBorderStyle FormBorderStyle { get { return base.FormBorderStyle; } set { base.FormBorderStyle = value; } }
        public bool Sizable { get; set; }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX info);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTBOTTOM = 15;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int BORDER_WIDTH = 7;
        private ResizeDirection _resizeDir;
        private ButtonState _buttonState = ButtonState.None;

        private const int WMSZ_TOP = 3;
        private const int WMSZ_TOPLEFT = 4;
        private const int WMSZ_TOPRIGHT = 5;
        private const int WMSZ_LEFT = 1;
        private const int WMSZ_RIGHT = 2;
        private const int WMSZ_BOTTOM = 6;
        private const int WMSZ_BOTTOMLEFT = 7;
        private const int WMSZ_BOTTOMRIGHT = 8;

        private readonly Dictionary<int, int> _resizingLocationsToCmd = new Dictionary<int, int>
        {
            {HTTOP,         WMSZ_TOP},
            {HTTOPLEFT,     WMSZ_TOPLEFT},
            {HTTOPRIGHT,    WMSZ_TOPRIGHT},
            {HTLEFT,        WMSZ_LEFT},
            {HTRIGHT,       WMSZ_RIGHT},
            {HTBOTTOM,      WMSZ_BOTTOM},
            {HTBOTTOMLEFT,  WMSZ_BOTTOMLEFT},
            {HTBOTTOMRIGHT, WMSZ_BOTTOMRIGHT}
        };

        private const int STATUS_BAR_BUTTON_WIDTH = STATUS_BAR_HEIGHT;
        private const int STATUS_BAR_HEIGHT = 24;
        private const int ACTION_BAR_HEIGHT = 40;

        private const uint TPM_LEFTALIGN = 0x0000;
        private const uint TPM_RETURNCMD = 0x0100;

        private const int WM_SYSCOMMAND = 0x0112;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int WS_SYSMENU = 0x00080000;

        private const int MONITOR_DEFAULTTONEAREST = 2;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public class MONITORINFOEX
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szDevice = new char[32];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public int Width()
            {
                return right - left;
            }

            public int Height()
            {
                return bottom - top;
            }
        }

        private enum ResizeDirection
        {
            BottomLeft,
            Left,
            Right,
            BottomRight,
            Bottom,
            None
        }

        private enum ButtonState
        {
            XOver,
            MaxOver,
            MinOver,
            XDown,
            MaxDown,
            MinDown,
            None
        }

        private readonly Cursor[] _resizeCursors = { Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNS };

        private Rectangle _minButtonBounds;
        private Rectangle _maxButtonBounds;
        private Rectangle _xButtonBounds;
        private Rectangle _actionBarBounds;
        private Rectangle _statusBarBounds;

        private bool _maximized;
        private Size _previousSize;
        private Point _previousLocation;
        private bool _headerMouseDown;

        public Form()
        {
            FormBorderStyle = FormBorderStyle.None;
            Sizable = true;
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            // This enables the form to trigger the MouseMove event even when mouse is over another control
            Application.AddMessageFilter(new MouseMessageFilter());
            MouseMessageFilter.MouseMove += OnGlobalMouseMove;

            Initialize();
        }

        #region Esthetic style and animation

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (DesignMode || IsDisposed) return;

            if (m.Msg == WM_LBUTTONDBLCLK)
            {
                MaximizeWindow(!_maximized);
            }
            else if (m.Msg == WM_MOUSEMOVE && _maximized &&
                (_statusBarBounds.Contains(PointToClient(Cursor.Position)) || _actionBarBounds.Contains(PointToClient(Cursor.Position))) &&
                !(_minButtonBounds.Contains(PointToClient(Cursor.Position)) || _maxButtonBounds.Contains(PointToClient(Cursor.Position)) || _xButtonBounds.Contains(PointToClient(Cursor.Position))))
            {
                if (_headerMouseDown)
                {
                    _maximized = false;
                    _headerMouseDown = false;

                    var mousePoint = PointToClient(Cursor.Position);
                    if (mousePoint.X < Width / 2)
                        Location = mousePoint.X < _previousSize.Width / 2 ?
                            new Point(Cursor.Position.X - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
                            new Point(Cursor.Position.X - _previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);
                    else
                        Location = Width - mousePoint.X < _previousSize.Width / 2 ?
                            new Point(Cursor.Position.X - _previousSize.Width + Width - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
                            new Point(Cursor.Position.X - _previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);

                    Size = _previousSize;
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            else if (m.Msg == WM_LBUTTONDOWN &&
                (_statusBarBounds.Contains(PointToClient(Cursor.Position)) || _actionBarBounds.Contains(PointToClient(Cursor.Position))) &&
                !(_minButtonBounds.Contains(PointToClient(Cursor.Position)) || _maxButtonBounds.Contains(PointToClient(Cursor.Position)) || _xButtonBounds.Contains(PointToClient(Cursor.Position))))
            {
                if (!_maximized)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
                else
                {
                    _headerMouseDown = true;
                }
            }
            else if (m.Msg == WM_RBUTTONDOWN)
            {
                Point cursorPos = PointToClient(Cursor.Position);

                if (_statusBarBounds.Contains(cursorPos) && !_minButtonBounds.Contains(cursorPos) &&
                    !_maxButtonBounds.Contains(cursorPos) && !_xButtonBounds.Contains(cursorPos))
                {
                    // Show default system menu when right clicking titlebar
                    var id = TrackPopupMenuEx(GetSystemMenu(Handle, false), TPM_LEFTALIGN | TPM_RETURNCMD, Cursor.Position.X, Cursor.Position.Y, Handle, IntPtr.Zero);

                    // Pass the command as a WM_SYSCOMMAND message
                    SendMessage(Handle, WM_SYSCOMMAND, id, 0);
                }
            }
            else if (m.Msg == WM_NCLBUTTONDOWN)
            {
                // This re-enables resizing by letting the application know when the
                // user is trying to resize a side. This is disabled by default when using WS_SYSMENU.
                if (!Sizable) return;

                byte bFlag = 0;

                // Get which side to resize from
                if (_resizingLocationsToCmd.ContainsKey((int)m.WParam))
                    bFlag = (byte)_resizingLocationsToCmd[(int)m.WParam];

                if (bFlag != 0)
                    SendMessage(Handle, WM_SYSCOMMAND, 0xF000 | bFlag, (int)m.LParam);
            }
            else if (m.Msg == WM_LBUTTONUP)
            {
                _headerMouseDown = false;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var par = base.CreateParams;
                // WS_SYSMENU: Trigger the creation of the system menu
                // WS_MINIMIZEBOX: Allow minimizing from taskbar
                par.Style = par.Style | WS_MINIMIZEBOX | WS_SYSMENU; // Turn on the WS_MINIMIZEBOX style flag
                return par;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (DesignMode) return;
            UpdateButtons(e);

            if (e.Button == MouseButtons.Left && !_maximized)
                ResizeForm(_resizeDir);
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (DesignMode) return;
            _buttonState = ButtonState.None;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode) return;

            if (Sizable)
            {
                //True if the mouse is hovering over a child control
                var isChildUnderMouse = GetChildAtPoint(e.Location) != null;

                if (e.Location.X < BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.BottomLeft;
                    Cursor = Cursors.SizeNESW;
                }
                else if (e.Location.X < BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.Left;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Location.X > Width - BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.BottomRight;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (e.Location.X > Width - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.Right;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.Bottom;
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    _resizeDir = ResizeDirection.None;

                    //Only reset the cursor when needed, this prevents it from flickering when a child control changes the cursor to its own needs
                    if (_resizeCursors.Contains(Cursor))
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }

            UpdateButtons(e);
        }

        protected void OnGlobalMouseMove(object sender, MouseEventArgs e)
        {
            if (IsDisposed) return;
            // Convert to client position and pass to Form.MouseMove
            var clientCursorPos = PointToClient(e.Location);
            var newE = new MouseEventArgs(MouseButtons.None, 0, clientCursorPos.X, clientCursorPos.Y, 0);
            OnMouseMove(newE);
        }

        private void UpdateButtons(MouseEventArgs e, bool up = false)
        {
            if (DesignMode) return;
            var oldState = _buttonState;
            bool showMin = MinimizeBox && ControlBox;
            bool showMax = MaximizeBox && ControlBox;

            if (e.Button == MouseButtons.Left && !up)
            {
                if (showMin && !showMax && _maxButtonBounds.Contains(e.Location))
                    _buttonState = ButtonState.MinDown;
                else if (showMin && showMax && _minButtonBounds.Contains(e.Location))
                    _buttonState = ButtonState.MinDown;
                else if (showMax && _maxButtonBounds.Contains(e.Location))
                    _buttonState = ButtonState.MaxDown;
                else if (ControlBox && _xButtonBounds.Contains(e.Location))
                    _buttonState = ButtonState.XDown;
                else
                    _buttonState = ButtonState.None;
            }
            else
            {
                if (showMin && !showMax && _maxButtonBounds.Contains(e.Location))
                {
                    _buttonState = ButtonState.MinOver;

                    if (oldState == ButtonState.MinDown && up)
                        WindowState = FormWindowState.Minimized;
                }
                else if (showMin && showMax && _minButtonBounds.Contains(e.Location))
                {
                    _buttonState = ButtonState.MinOver;

                    if (oldState == ButtonState.MinDown && up)
                        WindowState = FormWindowState.Minimized;
                }
                else if (MaximizeBox && ControlBox && _maxButtonBounds.Contains(e.Location))
                {
                    _buttonState = ButtonState.MaxOver;

                    if (oldState == ButtonState.MaxDown && up)
                        MaximizeWindow(!_maximized);

                }
                else if (ControlBox && _xButtonBounds.Contains(e.Location))
                {
                    _buttonState = ButtonState.XOver;

                    if (oldState == ButtonState.XDown && up)
                        Close();
                }
                else _buttonState = ButtonState.None;
            }

            if (oldState != _buttonState) Invalidate();
        }

        private void MaximizeWindow(bool maximize)
        {
            if (!MaximizeBox || !ControlBox) return;

            _maximized = maximize;

            if (maximize)
            {
                var monitorHandle = MonitorFromWindow(Handle, MONITOR_DEFAULTTONEAREST);
                var monitorInfo = new MONITORINFOEX();
                GetMonitorInfo(new HandleRef(null, monitorHandle), monitorInfo);
                _previousSize = Size;
                _previousLocation = Location;
                Size = new Size(monitorInfo.rcWork.Width(), monitorInfo.rcWork.Height());
                Location = new Point(monitorInfo.rcWork.left, monitorInfo.rcWork.top);
            }
            else
            {
                Size = _previousSize;
                Location = _previousLocation;
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DesignMode) return;
            UpdateButtons(e, true);

            base.OnMouseUp(e);
            ReleaseCapture();
        }

        private void ResizeForm(ResizeDirection direction)
        {
            if (DesignMode) return;
            var dir = -1;
            switch (direction)
            {
                case ResizeDirection.BottomLeft:
                    dir = HTBOTTOMLEFT;
                    break;
                case ResizeDirection.Left:
                    dir = HTLEFT;
                    break;
                case ResizeDirection.Right:
                    dir = HTRIGHT;
                    break;
                case ResizeDirection.BottomRight:
                    dir = HTBOTTOMRIGHT;
                    break;
                case ResizeDirection.Bottom:
                    dir = HTBOTTOM;
                    break;
            }

            ReleaseCapture();
            if (dir != -1)
            {
                SendMessage(Handle, WM_NCLBUTTONDOWN, dir, 0);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _settingsMenu.Location = new Point(this.Size.Width - _settingsMenu.Width, STATUS_BAR_HEIGHT);
            _RedoButton.Location = new Point(this.Size.Width - _settingsMenu.Width - _RedoButton.Width, STATUS_BAR_HEIGHT);
            _UndoButton.Location = new Point(this.Size.Width - _settingsMenu.Width - _RedoButton.Width - _UndoButton.Width, STATUS_BAR_HEIGHT);
            _minButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - 3 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _maxButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - 2 * STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _xButtonBounds = new Rectangle((Width - SkinManager.FORM_PADDING / 2) - STATUS_BAR_BUTTON_WIDTH, 0, STATUS_BAR_BUTTON_WIDTH, STATUS_BAR_HEIGHT);
            _statusBarBounds = new Rectangle(0, 0, Width, STATUS_BAR_HEIGHT);
            _actionBarBounds = new Rectangle(0, STATUS_BAR_HEIGHT, Width, ACTION_BAR_HEIGHT);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(SkinManager.GetApplicationBackgroundColor());
            g.FillRectangle(SkinManager.ColorScheme.DarkPrimaryBrush, _statusBarBounds);
            g.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, _actionBarBounds);

            //Draw border
            using (var borderPen = new Pen(SkinManager.GetDividersColor(), 1))
            {
                g.DrawLine(borderPen, new Point(0, _actionBarBounds.Bottom), new Point(0, Height - 2));
                g.DrawLine(borderPen, new Point(Width - 1, _actionBarBounds.Bottom), new Point(Width - 1, Height - 2));
                g.DrawLine(borderPen, new Point(0, Height - 1), new Point(Width - 1, Height - 1));
            }

            // Determine whether or not we even should be drawing the buttons.
            bool showMin = MinimizeBox && ControlBox;
            bool showMax = MaximizeBox && ControlBox;
            var hoverBrush = SkinManager.GetFlatButtonHoverBackgroundBrush();
            var downBrush = SkinManager.GetFlatButtonPressedBackgroundBrush();

            // When MaximizeButton == false, the minimize button will be painted in its place
            if (_buttonState == ButtonState.MinOver && showMin)
                g.FillRectangle(hoverBrush, showMax ? _minButtonBounds : _maxButtonBounds);

            if (_buttonState == ButtonState.MinDown && showMin)
                g.FillRectangle(downBrush, showMax ? _minButtonBounds : _maxButtonBounds);

            if (_buttonState == ButtonState.MaxOver && showMax)
                g.FillRectangle(hoverBrush, _maxButtonBounds);

            if (_buttonState == ButtonState.MaxDown && showMax)
                g.FillRectangle(downBrush, _maxButtonBounds);

            if (_buttonState == ButtonState.XOver && ControlBox)
                g.FillRectangle(hoverBrush, _xButtonBounds);

            if (_buttonState == ButtonState.XDown && ControlBox)
                g.FillRectangle(downBrush, _xButtonBounds);

            using (var formButtonsPen = new Pen(SkinManager.ACTION_BAR_TEXT_SECONDARY, 2))
            {
                // Minimize button.
                if (showMin)
                {
                    int x = showMax ? _minButtonBounds.X : _maxButtonBounds.X;
                    int y = showMax ? _minButtonBounds.Y : _maxButtonBounds.Y;

                    g.DrawLine(
                        formButtonsPen,
                        x + (int)(_minButtonBounds.Width * 0.33),
                        y + (int)(_minButtonBounds.Height * 0.66),
                        x + (int)(_minButtonBounds.Width * 0.66),
                        y + (int)(_minButtonBounds.Height * 0.66)
                   );
                }

                // Maximize button
                if (showMax)
                {
                    g.DrawRectangle(
                        formButtonsPen,
                        _maxButtonBounds.X + (int)(_maxButtonBounds.Width * 0.33),
                        _maxButtonBounds.Y + (int)(_maxButtonBounds.Height * 0.36),
                        (int)(_maxButtonBounds.Width * 0.39),
                        (int)(_maxButtonBounds.Height * 0.31)
                   );
                }

                // Close button
                if (ControlBox)
                {
                    g.DrawLine(
                        formButtonsPen,
                        _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.33),
                        _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.33),
                        _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.66),
                        _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.66)
                   );

                    g.DrawLine(
                        formButtonsPen,
                        _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.66),
                        _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.33),
                        _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.33),
                        _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.66));
                }
            }

            //Form title
            g.DrawString(Text, SkinManager.openSans[12, OpenSans.Weight.Medium], SkinManager.ColorScheme.TextBrush, new Rectangle(SkinManager.FORM_PADDING, STATUS_BAR_HEIGHT, Width, ACTION_BAR_HEIGHT), new StringFormat { LineAlignment = StringAlignment.Center });

        }


        #endregion

        #region Form Menu Icons(Top Left)

        private SettingsMenu _settingsMenu;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowSettingsMenu
        {
            get { return _settingsMenu.Visible; }
            set { _settingsMenu.Visible = value; }
        }
        RedoButton _RedoButton;

        UndoButton _UndoButton;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool EnableUndoRedo
        {
            get { return _UndoButton.Visible; }
            set
            {
                _UndoButton.Visible = value;
                _RedoButton.Visible = value;
            }
        }

        #region Form Menu Events

        public delegate void OnUndoClicked();
        public OnUndoClicked UndoClicked;

        public delegate void OnRedoClicked();
        public OnRedoClicked RedoClicked;
        #endregion

        #endregion


        #region Blur and unblur

        private PictureBox Blur_pb;
        public Control _BlurControl;
        public void Blur(Control blurControl)
        {
            DrawingControl.SuspendDrawing(this);
            // On blur control initialization
     
            // If blur control has changed
            if (blurControl != _BlurControl)
            {       
                if (_BlurControl == null)
                {
                    Blur_pb = new PictureBox();
                    Blur_pb.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
                    this.Controls.Add(Blur_pb);
                    Blur_pb.BringToFront();
                }
               
                if (blurControl == this)
                {
                    _BlurControl = this;
                    Blur_pb.Location = new Point(0, 0);
                    Blur_pb.Size = this.Size;
                }
                else
                {
                    _BlurControl = blurControl;
                    Blur_pb.Location = _BlurControl.Location;
                    Blur_pb.Size = _BlurControl.Size;
                }
            }

            Bitmap bmp = ModernGUI.Controls.BlurButmapFilter.TakeSnapshot(_BlurControl);
            ModernGUI.Controls.BlurButmapFilter.GaussianBlur(bmp, 4);

            Blur_pb.Image = bmp;
            Blur_pb.Visible = true;
            Blur_pb.BringToFront();

            DrawingControl.ResumeDrawing(this);
        }

        public void UnBlur()
        {
            Blur_pb.Image = null;
            Blur_pb.Visible = false;
            Blur_pb.SendToBack();
        }

        #endregion

        private void Initialize()
        {
            _settingsMenu = new SettingsMenu();
            Blur_pb = new PictureBox();

            _RedoButton = new RedoButton();
            _UndoButton = new UndoButton();

            _RedoButton.Click += (e, s) => RedoClicked?.Invoke();
            _UndoButton.Click += (e, s) => UndoClicked?.Invoke();

            this.SuspendLayout();
            // 
            // Form
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form";


            //
            // Settings Menu
            //
            _settingsMenu.Size = new Size(ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            _settingsMenu.Location = new Point(this.Size.Width - _settingsMenu.Width, STATUS_BAR_HEIGHT);
            _settingsMenu.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //
            // Redo Button
            //
            _RedoButton.Size = new Size(ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            _RedoButton.Location = new Point(this.Size.Width - _settingsMenu.Width - _RedoButton.Width, STATUS_BAR_HEIGHT);
            _RedoButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            //
            // Undo Button
            //
            _UndoButton.Size = new Size(ACTION_BAR_HEIGHT, ACTION_BAR_HEIGHT);
            _UndoButton.Location = new Point(this.Size.Width - _settingsMenu.Width - _RedoButton.Width - _UndoButton.Width, STATUS_BAR_HEIGHT);
            _UndoButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);




            this.Controls.Add(_settingsMenu);
            this.Controls.Add(_RedoButton);
            this.Controls.Add(_UndoButton);


            this.ResumeLayout(false);

        }

       
        public bool PauseUserControlUpdatedEvent { get; set; } = false;
        public delegate void UserChangedValueOfControl();

        public UserChangedValueOfControl OnValueOfControlChangedByUser;

        #region Fire Controllupdated() method when users change value of a control

        protected override void OnControlAdded(ControlEventArgs e)
        {
            SubscribeEvents(e.Control);
            base.OnControlAdded(e);
        }

        private void SubscribeEvents(System.Windows.Forms.Control control)
        {
            foreach (System.Windows.Forms.Control innerControl in control.Controls)
            {
                SubscribeEvents(innerControl);
            }

            control.Enter += ControlEntered;
            control.Leave += ControlLeft;
        }

        object SelectedControl = null;
        private bool SelectedControlContentChanged;

        private void ControlEntered(object sender, EventArgs e)
        {
            if (SelectedControl != sender)
            {
                SelectedControl = sender;
                SelectedControlContentChanged = false;
                if (sender.GetType() == typeof(System.Windows.Forms.TextBox))
                {
                    ((System.Windows.Forms.TextBox)sender).TextChanged += new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(System.Windows.Forms.ComboBox))
                {
                    ((System.Windows.Forms.ComboBox)sender).SelectedIndexChanged += new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(System.Windows.Forms.CheckBox))
                {
                    ((System.Windows.Forms.CheckBox)sender).CheckedChanged += new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(ModernGUI.Controls.SingleLineTextField))
                {
                    ((ModernGUI.Controls.SingleLineTextField)sender).TextChanged += new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(System.Windows.Forms.DataGridView))
                {
                    ((System.Windows.Forms.DataGridView)sender).CellValueChanged += new DataGridViewCellEventHandler(OnContentChanged);
                    ((System.Windows.Forms.DataGridView)sender).CurrentCellDirtyStateChanged += new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(ModernGUI.Controls.DataGridView))
                {
                    ((ModernGUI.Controls.DataGridView)sender).CellValueChanged += new DataGridViewCellEventHandler(OnContentChanged);
                    ((ModernGUI.Controls.DataGridView)sender).CurrentCellDirtyStateChanged += new EventHandler(OnContentChanged);
                }

                if (sender.GetType() == typeof(System.Windows.Forms.MonthCalendar))
                {
                    ((MonthCalendar)sender).DateChanged += new DateRangeEventHandler(OnContentChanged);
                }

                if (sender.GetType() == typeof(ModernGUI.Controls.RadioButton))
                {
                    ((RadioButton)sender).CheckedChanged += new EventHandler(OnContentChanged);
                }

                if (sender.GetType() == typeof(ModernGUI.Controls.ToggleSwitch))
                {
                    ((ToggleSwitch)sender).CheckedChanged += new ToggleSwitch.CheckedChangedDelegate(OnContentChanged);
                }
                if (sender.GetType() == typeof(ModernGUI.Controls.Calendar))
                {
                    // This needs to be added when any object on the calendar changes
                }

            }
        }

        private void OnContentChanged(object? sender, EventArgs e)
        {
            SelectedControlContentChanged = true;
        }

        private void ControlLeft(object sender, EventArgs e)
        {
            if (SelectedControl == sender)
            {
                if (sender.GetType() == typeof(System.Windows.Forms.TextBox))
                {
                    ((System.Windows.Forms.TextBox)sender).TextChanged -= new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(System.Windows.Forms.ComboBox))
                {
                    ((System.Windows.Forms.ComboBox)sender).SelectedIndexChanged -= new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(System.Windows.Forms.CheckBox))
                {
                    ((System.Windows.Forms.CheckBox)sender).CheckedChanged -= new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(ModernGUI.Controls.SingleLineTextField))
                {
                    ((ModernGUI.Controls.SingleLineTextField)sender).TextChanged -= new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(System.Windows.Forms.DataGridView))
                {
                    ((DataGridView)sender).CellValueChanged -= new DataGridViewCellEventHandler(OnContentChanged);
                    ((DataGridView)sender).CurrentCellDirtyStateChanged -= new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(ModernGUI.Controls.DataGridView))
                {
                    ((ModernGUI.Controls.DataGridView)sender).CellValueChanged -= new DataGridViewCellEventHandler(OnContentChanged);
                    ((ModernGUI.Controls.DataGridView)sender).CurrentCellDirtyStateChanged -= new EventHandler(OnContentChanged);
                }
                if (sender.GetType() == typeof(System.Windows.Forms.MonthCalendar))
                {
                    ((MonthCalendar)sender).DateChanged -= new DateRangeEventHandler(OnContentChanged);
                }

                if (sender.GetType() == typeof(ModernGUI.Controls.RadioButton))
                {
                    ((RadioButton)sender).CheckedChanged -= new EventHandler(OnContentChanged);
                }

                if (sender.GetType() == typeof(ModernGUI.Controls.ToggleSwitch))
                {
                    ((ToggleSwitch)sender).CheckedChanged -= new ToggleSwitch.CheckedChangedDelegate(OnContentChanged);
                }
                if (sender.GetType() == typeof(ModernGUI.Controls.Calendar))
                {
                    // This needs to be added when any object on the calendar changes
                }
            }

            if (SelectedControlContentChanged == true && PauseUserControlUpdatedEvent == false)
            {
                ControlUpdated();
                SelectedControlContentChanged = false;
            }
        }

        /// <summary>
        /// This is called when a model might have to be updated.
        /// </summary>
        private void ControlUpdated()
        {
            OnValueOfControlChangedByUser?.Invoke();

        }
        #endregion
    }


}