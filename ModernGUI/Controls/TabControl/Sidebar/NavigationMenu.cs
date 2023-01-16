using ModernGUI.Animations;
using ModernGUI.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms.Layout;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Text;

namespace ModernGUI.Controls
{
    [DefaultEvent("OnItemSelected")]
    [DefaultProperty("Items")]
    [DebuggerStepThrough]
    public partial class NavigtionMenu : IControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        #region Private fields

        private bool IsLoaded;
        private int _buttonHeight;

        private Size _itemImageSize;
        private int _itemTextPadding;
        private Padding _itemPading;
        private bool _isExpanded;
        private int origW;
        private int _itemRightIMGMargin;
        private Size _itemRightImageSize;
        private Color _backColor_Selected;
        private bool _isExpandedable;
        private Color _foreColor_Selected;

        private TabControl _baseTabControl;
        private ButtonItem[] _items;
        #endregion

        #region Public properties
        public Dictionary<string, Action<CButton>> OnItemClick;
        public ButtonItem[] Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
                this.RedrawButtons();
            }
        }
        public int ItemHeight
        {
            get
            {
                return this._buttonHeight;
            }
            set
            {
                this._buttonHeight = value;
                this.RedrawButtons();
            }
        }
        public string[] DisableToggling { get; set; } = new string[0];
        public Color BackColor_Hover { get; set; } = Color.FromArgb(39, 49, 70);
        public Color BackColor_Click { get; set; } = Color.FromArgb(39, 49, 70);
        public Color BackColor_Selected
        {
            get => this._backColor_Selected;
            set
            {
                this._backColor_Selected = value;

                this.RedrawButtons();
            }
        }
        public bool IsExpanded
        {
            get
            {
                return this._isExpanded;
            }
            set
            {
                if (this._isExpanded != value)
                {

                    this._isExpanded = value;
                    this.RedrawButtons();

                    OnExpanderChanged?.Invoke(this, value, this.Size);
                }

            }
        }
        public Size ItemImageSize
        {
            get => this._itemImageSize;
            set
            {
                this._itemImageSize = value;
                this.RedrawButtons();
            }
        }
        public Size ItemRightImageSize
        {
            get => this._itemRightImageSize;
            set => this._itemRightImageSize = value;
        }
        public int ItemTextMargin
        {
            get => this._itemTextPadding;
            set
            {
                this._itemTextPadding = value;
                this.RedrawButtons();
            }
        }
        public int ItemRightImageMargin
        {
            get
            {
                return this._itemRightIMGMargin;
            }
            set
            {
                this._itemRightIMGMargin = value;
                this.RedrawButtons();
            }
        }
        public Padding ItemPadding
        {
            get
            {
                return this._itemPading;
            }
            set
            {
                this._itemPading = value;
                this.RedrawButtons();
            }
        }
        public bool IsExpandedable
        {
            get
            {
                return this._isExpandedable;
            }
            set
            {
                this._isExpandedable = value;
                this.HederPanel.Visible = value;
                this.RedrawButtons();
            }
        }
        public Color ForeColor_Selected
        {
            get
            {
                return this._foreColor_Selected;

            }
            set
            {
                this._foreColor_Selected = value;
                this.RedrawButtons();
            }
        }
        #endregion

        public event NavigtionMenu.OnSelectEventHandler OnItemSelected;
        public delegate void OnSelectEventHandler(object sender, string path, EventArgs e);

        public delegate void OnExpanderVisibilityChanged(object sender, bool visibilty, Size size);
        public event NavigtionMenu.OnExpanderVisibilityChanged OnExpanderChanged;

        private readonly AnimationManager _animationManager;
        int _previousSelectedTabIndex;
        int _SelectedTabIndex;

        public TabControl BaseTabControl
        {
            get { return _baseTabControl; }
            set
            {
                _baseTabControl = value;

                if (_baseTabControl == null) return;

                _previousSelectedTabIndex = _baseTabControl.SelectedIndex;
                _baseTabControl.Deselected += (sender, args) =>
                {
                    _previousSelectedTabIndex = _baseTabControl.SelectedIndex;
                };
                _baseTabControl.SelectedIndexChanged += (sender, args) =>
                {
                    _SelectedTabIndex = _baseTabControl.SelectedIndex;
                };
            }
        }

        public NavigtionMenu()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            _buttonHeight = 50;
            _items = new ButtonItem[0];
            _itemImageSize = new Size(20, 20);
            _itemTextPadding = 8;
            _itemPading = new Padding() { Left = 8 };
            _itemRightIMGMargin = 20;
            _itemRightImageSize = new Size(15, 15);
            _backColor_Selected = Color.FromArgb(88, 104, 240);
            _isExpandedable = true;
            _foreColor_Selected = Color.Empty;

            _animationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseOut,
                Increment = 0.04
            };

            _animationManager.OnAnimationProgress += sender => Invalidate();

            InitializeComponent();
        }

        #region Redraw Control

        private void RedrawButtons()
        {
            if (!this.IsLoaded)
            {
                return;
            }

            this.expandables.Clear();
            this.pnlContainer.Controls.Clear();
            this.ImgExpand_PicBox.Size = this.ItemImageSize;
            this.ImgExpand_PicBox.Left = this.ItemPadding.Left + 7;
            int num = 0;

            foreach (ButtonItem buttonItem in this != null ? this.Items : (ButtonItem[])null)
            {
                num++;
                DrawMainCategory(buttonItem, ref num);
            }
        }

        private void DrawMainCategory(ButtonItem buttonItem, ref int num)
        {
            ButtonItem btnItem = buttonItem;

            string str = "";

            for (int index = 0; index < this.ItemTextMargin; ++index)
            {
                str += " ";
            }

            CButton button = new CButton();

            button.Name = num.ToString();
            button.Text = this.IsExpanded ? str + btnItem.Text : string.Empty;
            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = this.BackColor;
            button.Height = this.ItemHeight;
            button.Dock = DockStyle.Top;
            button.TextAlign = ContentAlignment.MiddleLeft;

            if (this.BackColor_Hover != Color.Empty)
            {
                button.FlatAppearance.MouseOverBackColor = this.BackColor_Hover;
            }
            if (this.BackColor_Click != Color.Empty)
            {
                button.FlatAppearance.MouseDownBackColor = this.BackColor_Click;
            }
            if (btnItem.Icon == null)
            {
                btnItem.Icon = this.ImgExpand_PicBox.Image;
            }
            this.imageList1.Images.Clear();
            this.imageList1.ImageSize = this.ItemImageSize;
            this.imageList1.Images.Add(btnItem.Icon);

            button.Image = this.imageList1.Images[0];
            btnItem.ImageIdle = (Image)ModernGUI.Shared.Drawing.ChangeColor((Bitmap)this.imageList1.Images[0], this.ForeColor);
            btnItem.ImageActive = (Image)ModernGUI.Shared.Drawing.ChangeColor((Bitmap)this.imageList1.Images[0], this.ForeColor_Selected);
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.ButtonItem = btnItem;

            if (!this.IsExpanded)
            {
                this.Width = this.ItemHeight + this.ItemPadding.Left;
            }
            else if (this.origW > 0)
            {
                this.Width = this.origW;
            }

            button.Padding = this.ItemPadding;
            this.pnlContainer.Controls.Add((Control)button);

            button.BringToFront();

            button.Click += (s, e) => Button_Click(s, e, btnItem, button);

            List<CButton> cbuttonList = new List<CButton>();

            if (btnItem.SubItems.Length != 0)
            {
                PictureBox pic = new PictureBox();
                Image pic1 = (Image)ModernGUI.Shared.Drawing.ChangeColor(new Bitmap(this.MenuHide_PicBox.Image), this.ForeColor);
                Image pic2 = (Image)ModernGUI.Shared.Drawing.ChangeColor(new Bitmap(this.MenuDropdown_PicBox.Image), this.ForeColor);

                pic.Image = btnItem.IsExpanded ? pic2 : pic1;
                pic.Size = this.ItemRightImageSize;
                pic.Left = button.Width - pic.Width - this.ItemRightImageMargin;
                pic.Top = this.ItemHeight / 2 - pic.Height / 2;
                pic.Anchor = AnchorStyles.Right;
                pic.BackColor = button.BackColor;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Enabled = false;

                button.Controls.Add((Control)pic);

                button.MouseEnter += (EventHandler)((s, e) => pic.BackColor = button.FlatAppearance.MouseOverBackColor);
                button.MouseDown += (MouseEventHandler)((s, e) => pic.BackColor = button.FlatAppearance.MouseDownBackColor);
                button.MouseUp += (MouseEventHandler)((s, e) => pic.BackColor = button.FlatAppearance.MouseOverBackColor);
                button.MouseLeave += (EventHandler)((s, e) => pic.BackColor = button.BackColor);
                button.Click += (EventHandler)((s, e) => pic.Image = pic.Image == pic1 ? pic2 : pic1);

                this.expandables.Add(pic);

                foreach (string subItem in btnItem.SubItems)
                {
                    num++;
                    DrawSubCategory(str, subItem, ref num, btnItem, cbuttonList, button);
                }
            }
            else if (this.selectedBtn == null)
            {
                this.selectedBtn = button;
            }

            button.Tag = (object)cbuttonList;
            button.ButtonItem = btnItem;
        }

        private void DrawSubCategory(string str, string subItem, ref int num, ButtonItem btnItem, List<CButton> cbuttonList, CButton button)
        {
            CButton sbutton = new CButton();

            if (this.selectedBtn == null)
            {
                this.selectedBtn = sbutton;
            }

            sbutton.Name = num.ToString();
            sbutton.Text = str + subItem;
            sbutton.Cursor = Cursors.Hand;
            sbutton.FlatStyle = FlatStyle.Flat;
            sbutton.FlatAppearance.BorderColor = this.BackColor;
            sbutton.Height = this.ItemHeight;
            sbutton.Dock = DockStyle.Top;
            sbutton.Padding = this.ItemPadding;
            sbutton.TextAlign = ContentAlignment.MiddleLeft;
            this.pnlContainer.Controls.Add((Control)sbutton);
            sbutton.BringToFront();
            sbutton.Visible = btnItem.IsExpanded && this.IsExpanded;

            if (this.BackColor_Hover != Color.Empty)
            {
                sbutton.FlatAppearance.MouseOverBackColor = this.BackColor_Hover;
            }
            if (this.BackColor_Click != Color.Empty)
            {
                sbutton.FlatAppearance.MouseDownBackColor = this.BackColor_Click;
            }
            sbutton.Tag = (object)(btnItem.Text + "." + subItem);
            sbutton.Group = button;
            sbutton.Click += SButton_Click;

            cbuttonList.Add(sbutton);
        }

        #endregion

        private void SetSelection(CButton selecteBtn, ButtonItem btnItem = null)
        {
            foreach (CButton control1 in (ArrangedElementCollection)this.pnlContainer.Controls)
            {
                if (selecteBtn.Name == control1.Name)
                {
                    if (selecteBtn.Group != null && selecteBtn.Group.Tag.GetType() == typeof(List<CButton>))
                    {
                        foreach (Control control2 in (List<CButton>)selecteBtn.Group.Tag)
                        {
                            control2.Visible = true;
                        }
                    }
                    FlatButtonAppearance flatAppearance1 = control1.FlatAppearance;
                    FlatButtonAppearance flatAppearance2 = control1.FlatAppearance;
                    Color color1;
                    control1.BackColor = color1 = this.BackColor_Selected == Color.Empty ? control1.FlatAppearance.MouseDownBackColor : this.BackColor_Selected;
                    Color color2;
                    Color color3 = color2 = color1;
                    flatAppearance2.BorderColor = color2;
                    Color color4 = color3;
                    flatAppearance1.MouseOverBackColor = color4;

                    if (btnItem != null)
                    {
                        control1.Image = control1.ButtonItem.ImageActive;
                    }
                    control1.ForeColor = this.ForeColor_Selected;
                }
                else
                {
                    control1.FlatAppearance.BorderColor = control1.BackColor = this.BackColor;
                    control1.FlatAppearance.MouseOverBackColor = this.BackColor_Hover;
                    control1.ForeColor = this.ForeColor;
                    control1.Image = control1.ButtonItem?.ImageIdle;
                }
            }
            this.selectedBtn = selecteBtn;
            SelectTab(selecteBtn);
        }

        // Selected a tab from the linked base tab control by its label
        private void SelectTab(CButton selecteBtn)
        {

            if (_baseTabControl != null)
            {
                TabPage tabPage = ReturnTabPageByText(selecteBtn.Text.Trim());

                if (tabPage != null)
                {
                    _baseTabControl.SelectedIndex = _baseTabControl.TabPages.IndexOf(tabPage);
                }
                else
                {
                    MessageBox.Show(" Linked basetabcontrol doesnt have the " + selecteBtn.Text.Trim() + " tab", "Tab doesnt exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private TabPage ReturnTabPageByText(string TabText)
        {
            return _baseTabControl.TabPages.OfType<TabPage>().FirstOrDefault(tab => tab.Text.Trim() == TabText);
        }

        public void Collapse()
        {
            if (this.origW == 0)
            {
                this.origW = this.Width;
            }
            this.IsExpanded = false;
            this.ImgCollapse_PicBox.Visible = false;
            this.ImgExpand_PicBox.Visible = true;

            this.SetExpanderVisibility(false);
        }

        private void SetExpanderVisibility(bool Visible)
        {
            foreach (Control expandable in this.expandables)
            {
                if (expandable.Visible != Visible)
                {
                    expandable.Visible = Visible;
                }
            }
        }

        private void Expand(CButton selected = null)
        {
            this.IsExpanded = true;
            this.ImgCollapse_PicBox.Visible = true;
            this.ImgExpand_PicBox.Visible = false;
            this.SetExpanderVisibility(true);
        }

        #region Events
        private void Button_Click(object sender, EventArgs e, ButtonItem btnItem, CButton button)
        {
            if (btnItem.SubItems.Length == 0)
            {
                if (!this.IsExpanded)
                {
                    this.Expand();
                }
                if (!((IEnumerable<string>)this.DisableToggling).Contains<string>(button.ButtonItem.Text))
                {
                    this.SetSelection(button, btnItem);
                }
                NavigtionMenu.OnSelectEventHandler onItemSelected = this.OnItemSelected;
                if (onItemSelected != null)
                {
                    onItemSelected((object)this, btnItem.Text, new EventArgs());
                }
                if (!this.OnItemClick.ContainsKey(button.ButtonItem.Text))
                {
                    return;
                }
                Action<CButton> action = this.OnItemClick[button.ButtonItem.Text];
                if (action == null)
                {
                    return;
                }
                action(button);
            }
            else
            {
                CButton cbutton1 = (CButton)sender;
                if (!this.IsExpanded)
                {
                    this.IsExpanded = true;
                    this.ImgCollapse_PicBox.Visible = true;
                    this.ImgExpand_PicBox.Visible = false;
                    this.SetExpanderVisibility(true);
                    this.SetSelection(this.selectedBtn);
                }
                else
                {
                    foreach (CButton cbutton2 in (List<CButton>)cbutton1.Tag)
                    {
                        cbutton2.Visible = !cbutton2.Visible;
                    }
                    button.ButtonItem.IsExpanded = !button.ButtonItem.IsExpanded;
                }
            }
        }
        private void SButton_Click(object sender, EventArgs e)
        {
            CButton sbutton = (CButton)sender;

            if (!((IEnumerable<string>)this.DisableToggling).Contains<string>(sbutton.Tag.ToString()))
            {
                this.SetSelection(sbutton);
            }
            NavigtionMenu.OnSelectEventHandler onItemSelected = this.OnItemSelected;
            if (onItemSelected != null)
            {
                onItemSelected((object)this, sbutton.Tag.ToString(), new EventArgs());
            }
            if (!this.OnItemClick.ContainsKey(sbutton.Tag.ToString()))
            {
                return;
            }
            Action<CButton> action = this.OnItemClick[sbutton.Tag.ToString()];
            if (action == null)
            {
                return;
            }
            action(sbutton);
        }
        private void NavigtionMenu_ForeColorChanged(object sender, EventArgs e)
        {
            if (this.ImgExpand_PicBox.Tag == null)
            {
                this.ImgExpand_PicBox.Tag = (object)this.ImgExpand_PicBox.Image;
            }
            this.ImgExpand_PicBox.Image = (Image)ModernGUI.Shared.Drawing.ChangeColor(new Bitmap((Image)this.ImgExpand_PicBox.Tag), this.ForeColor);

            if (this.ImgCollapse_PicBox.Tag == null)
            {
                this.ImgCollapse_PicBox.Tag = (object)this.ImgCollapse_PicBox.Image;
            }
            this.ImgCollapse_PicBox.Image = (Image)ModernGUI.Shared.Drawing.ChangeColor(new Bitmap((Image)this.ImgCollapse_PicBox.Tag), this.ForeColor);
            this.RedrawButtons();
        }
        private void ImgCollapse_Click(object sender, EventArgs e)
        {
            this.Collapse();
        }
        private void ImgExpand_Click(object sender, EventArgs e)
        {
            this.Expand();
        }
        private void NavigtionMenu_Load(object sender, EventArgs e)
        {
            this.origW = this.Width;
            this.IsLoaded = true;
            this.ImgCollapse_PicBox.Visible = this.IsExpandedable;


            this.RedrawButtons();
            this.Width = this.origW;
            this.Expand(null);
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
           // var g = e.Graphics;
          //  g.TextRenderingHint = TextRenderingHint.AntiAlias;

          //  g.Clear(SkinManager.ColorScheme.PrimaryColor);

            /*
            if (_baseTabControl == null)
            {
                return;
            }
            if (!_animationManager.IsAnimating() || _tabRects == null || _tabRects.Count != _baseTabControl.TabCount)
            {
                UpdateTabRects();
            }

            var animationProgress = _animationManager.GetProgress();

            //Click feedback
            if (_animationManager.IsAnimating())
            {
                var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationProgress * 50)), Color.White));
                var rippleSize = (int)(animationProgress * _tabRects[_baseTabControl.SelectedIndex].Width * 1.75);

                g.SetClip(_tabRects[_baseTabControl.SelectedIndex]);
                g.FillEllipse(rippleBrush, new Rectangle(_animationSource.X - rippleSize / 2, _animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                g.ResetClip();
                rippleBrush.Dispose();
            }

            //Draw tab headers
            foreach (TabPage tabPage in _baseTabControl.TabPages)
            {
                var currentTabIndex = _baseTabControl.TabPages.IndexOf(tabPage);
                Brush textBrush = new SolidBrush(Color.FromArgb(CalculateTextAlpha(currentTabIndex, animationProgress), SkinManager.ColorScheme.TextColor));

                g.DrawString(tabPage.Text.ToUpper(), SkinManager.openSans[10, OpenSans.Weight.Medium], textBrush, _tabRects[currentTabIndex], new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                textBrush.Dispose();
            }

            //Animate tab indicator
            var previousSelectedTabIndexIfHasOne = _previousSelectedTabIndex == -1 ? _baseTabControl.SelectedIndex : _previousSelectedTabIndex;
            var previousActiveTabRect = _tabRects[previousSelectedTabIndexIfHasOne];
            var activeTabPageRect = _tabRects[_baseTabControl.SelectedIndex];

            var y = activeTabPageRect.Bottom - 2;
            var x = previousActiveTabRect.X + (int)((activeTabPageRect.X - previousActiveTabRect.X) * animationProgress);
            var width = previousActiveTabRect.Width + (int)((activeTabPageRect.Width - previousActiveTabRect.Width) * animationProgress);

            g.FillRectangle(SkinManager.ColorScheme.AccentBrush, x, y, width, TAB_INDICATOR_HEIGHT);   */
        }
     
    }
    #region Design Code

    public partial class NavigtionMenu : UserControl
    {
        private IContainer components;
        private ImageList imageList1;
        private Panel pnlContainer;
        private CButton selectedBtn;
        private List<PictureBox> expandables;
        private PictureBox ImgExpand_PicBox;
        private PictureBox ImgCollapse_PicBox;
        private PictureBox MenuDropdown_PicBox;
        private PictureBox MenuHide_PicBox;
        private System.Windows.Forms.ToolTip toolTip1;
        public Panel HederPanel;

        private void InitializeComponent()
        {
            this.components = (IContainer)new System.ComponentModel.Container();
            this.imageList1 = new ImageList(this.components);
            this.HederPanel = new Panel();
            this.MenuDropdown_PicBox = new PictureBox();
            this.MenuHide_PicBox = new PictureBox();
            this.ImgCollapse_PicBox = new PictureBox();
            this.ImgExpand_PicBox = new PictureBox();
            this.pnlContainer = new Panel();
            this.expandables = new List<PictureBox>();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.HederPanel.SuspendLayout();

            ((ISupportInitialize)this.MenuDropdown_PicBox).BeginInit();
            ((ISupportInitialize)this.MenuHide_PicBox).BeginInit();
            ((ISupportInitialize)this.ImgCollapse_PicBox).BeginInit();
            ((ISupportInitialize)this.ImgExpand_PicBox).BeginInit();

            this.SuspendLayout();

            OnItemClick = new Dictionary<string, Action<CButton>>();

            //
            // imageList1
            //
            this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new Size(16, 16);
            this.imageList1.TransparentColor = Color.Transparent;

            //
            // pnlContainer
            //
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Dock = DockStyle.Fill;
            this.pnlContainer.Location = new Point(0, 55);
            this.pnlContainer.Margin = new Padding(2, 4, 2, 4);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new Size(200, 500);
            this.pnlContainer.TabIndex = 1;

            //
            // HeaderPanel 
            //
            this.HederPanel.Controls.Add((Control)this.MenuHide_PicBox);
            this.HederPanel.Controls.Add((Control)this.MenuDropdown_PicBox);
            this.HederPanel.Controls.Add((Control)this.ImgCollapse_PicBox);
            this.HederPanel.Controls.Add((Control)this.ImgExpand_PicBox);
            this.HederPanel.Dock = DockStyle.Top;
            this.HederPanel.Location = new Point(0, 0);
            this.HederPanel.Margin = new Padding(2, 4, 2, 4);
            this.HederPanel.Name = "HederPanel";
            this.HederPanel.Size = new Size(200, 55);
            this.HederPanel.TabIndex = 0;
            this.HederPanel.ResumeLayout(false);

            //
            //  MenuDropdown_PicBox
            //
            this.MenuDropdown_PicBox.Cursor = Cursors.Hand;
            this.MenuDropdown_PicBox.Image = (Image)Resources.ImgMenuDropdown;
            this.MenuDropdown_PicBox.Location = new Point(97, 11);
            this.MenuDropdown_PicBox.Margin = new Padding(2, 4, 2, 4);
            this.MenuDropdown_PicBox.Name = "pictureBox2";
            this.MenuDropdown_PicBox.Size = new Size(25, 27);
            this.MenuDropdown_PicBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.MenuDropdown_PicBox.TabIndex = 3;
            this.MenuDropdown_PicBox.TabStop = false;
            this.MenuDropdown_PicBox.Visible = false;

            //
            //  MenuHide_PicBox
            //
            this.MenuHide_PicBox.Cursor = Cursors.Hand;
            this.MenuHide_PicBox.Image = (Image)Resources.ImgMenuHide;
            this.MenuHide_PicBox.Location = new Point(64, 11);
            this.MenuHide_PicBox.Margin = new Padding(2, 4, 2, 4);
            this.MenuHide_PicBox.Name = "pictureBox1";
            this.MenuHide_PicBox.Size = new Size(25, 27);
            this.MenuHide_PicBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.MenuHide_PicBox.TabIndex = 2;
            this.MenuHide_PicBox.TabStop = false;
            this.MenuHide_PicBox.Visible = false;

            //
            //  ImgCollapse_PicBox
            //
            this.ImgCollapse_PicBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ImgCollapse_PicBox.Cursor = Cursors.Hand;
            this.ImgCollapse_PicBox.Image = (Image)Resources.ImgCollapse;
            this.ImgCollapse_PicBox.Location = new Point(162, 7);
            this.ImgCollapse_PicBox.Margin = new Padding(2, 4, 2, 4);
            this.ImgCollapse_PicBox.Name = "ImgCollapse";
            this.ImgCollapse_PicBox.Size = new Size(26, 31);
            this.ImgCollapse_PicBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.ImgCollapse_PicBox.TabIndex = 1;
            this.ImgCollapse_PicBox.TabStop = false;
            this.ImgCollapse_PicBox.Click += new EventHandler(this.ImgCollapse_Click);

            //
            //  ImgExpand_PicBox
            //
            this.ImgExpand_PicBox.Cursor = Cursors.Hand;
            this.ImgExpand_PicBox.Image = (Image)Resources.ImgExpand;
            this.ImgExpand_PicBox.Location = new Point(15, 11);
            this.ImgExpand_PicBox.Margin = new Padding(2, 4, 2, 4);
            this.ImgExpand_PicBox.Name = "ImgExpand";
            this.ImgExpand_PicBox.Size = new Size(33, 31);
            this.ImgExpand_PicBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.ImgExpand_PicBox.TabIndex = 0;
            this.ImgExpand_PicBox.TabStop = false;
            this.ImgExpand_PicBox.Visible = false;
            this.ImgExpand_PicBox.Click += new EventHandler(this.ImgExpand_Click);

            this.toolTip1.IsBalloon = true;
            this.AutoScaleDimensions = new SizeF(9f, 20f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add((Control)this.pnlContainer);
            this.Controls.Add((Control)this.HederPanel);
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI Semibold", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.ForeColor = Color.FromArgb(224, 224, 224);
            this.BackColor = SkinManager.ColorScheme.PrimaryColor; 
            this.Margin = new Padding(3, 5, 3, 5);
            this.Name = nameof(NavigtionMenu);
            this.Size = new Size(250, 540);

            this.Load += new EventHandler(this.NavigtionMenu_Load);
            this.BackColorChanged += new EventHandler(delegate (object sender, EventArgs e) { this.RedrawButtons(); });
            this.ForeColorChanged += new EventHandler(this.NavigtionMenu_ForeColorChanged);

            ((ISupportInitialize)this.MenuDropdown_PicBox).EndInit();
            ((ISupportInitialize)this.MenuHide_PicBox).EndInit();
            ((ISupportInitialize)this.ImgCollapse_PicBox).EndInit();
            ((ISupportInitialize)this.ImgExpand_PicBox).EndInit();

            this.ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    #endregion
}
