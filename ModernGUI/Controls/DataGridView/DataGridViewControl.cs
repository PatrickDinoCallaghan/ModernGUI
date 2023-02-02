using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ModernGUI.Controls
{
    /// <summary>
    /// Represents a datagridviewcontrol control.
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.DataGridView)), ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Displays data in a customizable grid."), DesignTimeVisible(true)]
    public class DataGridView : System.Windows.Forms.DataGridView, IComponent, IControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public SkinManager SkinManager => SkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        #region Feilds
        private int _RowHeight = 30;
        private int _HeaderHeight = 25;
        private bool _ReadOnly = false;
        private Color _HeaderColor = Color.FromArgb(109, 122, 224);

        //   private Font _HeaderFont = new System.Drawing.Font("Segoe UI Semibold", 13.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        // private Font _Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        private Font _HeaderFont;

        private Font _Font;
        private bool _AllowUserToDragDrop = false;
        private VScrollBar ScrollBar = null;
        private bool _AllowUserToResize = false;
        private System.Windows.Forms.DataGridViewCellBorderStyle _CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

        private bool _ListBoxStyle = false;
        private System.Windows.Forms.BorderStyle _BorderStyle = System.Windows.Forms.BorderStyle.None;
        #endregion

        #region Properties
        // This is hidden from the user as it cannot be changed
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(DataGridViewColumnHeadersHeightSizeMode.DisableResizing)]
        public new System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
        {
            get { return base.ColumnHeadersHeightSizeMode; }

        }

        // This is hidden from the user as it cannot be changed
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(DataGridViewRowHeadersWidthSizeMode.DisableResizing)]
        public new System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode
        {
            get { return base.RowHeadersWidthSizeMode; }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int HeaderHeight { get { return _HeaderHeight; } set { _HeaderHeight = value; this.SetCellDimensions(); this.Invalidate(); } }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int RowHeight { get { return _RowHeight; } set { _RowHeight = value; this.SetCellDimensions(); this.Invalidate(); } }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new bool ReadOnly { get { return base.ReadOnly; } set { _ReadOnly = value; if (_ReadOnly != ReadOnly) { ReadOnlyChanged(); } base.ReadOnly = value; } }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color HeaderColor { get { return _HeaderColor; } set { _HeaderColor = value; this.Invalidate(); } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font HeaderFont { get { return _HeaderFont; } set { _HeaderFont = value; this.Invalidate(); } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Font Font { get { return _HeaderFont; } set { _HeaderFont = value; this.Invalidate(); } }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowUserToDragDrop { get { return _AllowUserToDragDrop; } set { _AllowUserToDragDrop = value; } }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowUserToResize { get { return _AllowUserToResize; } set { _AllowUserToResize = value; this.Invalidate(); } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Windows.Forms.DataGridViewCellBorderStyle CellBorderStyle { get { return _CellBorderStyle; } set { base.CellBorderStyle = value; _CellBorderStyle = value; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Windows.Forms.BorderStyle BorderStyle
        {
            get { return _BorderStyle; }
            set { _BorderStyle = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackgroundColor
        {
            get { return base.BackgroundColor; }
            set { base.BackgroundColor = value; }
        }

        /// <summary>
        /// Hides the in theme header of the datagridview and sets  it to read only. This gives the control a more conventional listbox style look
        /// </summary>
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ListBoxStyle
        {
            get
            {
                return _ListBoxStyle;
            }

            set
            {
                if (value == true)
                {
                    ReadOnly = true;
                }
                _ListBoxStyle = value;

                this.Invalidate();
            }
        }

        #endregion

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor
        {
            get
            {
                return this.BackgroundColor;
            }
            set
            {
                this.BackgroundColor = value;

                Set_CellDefaultStyle(value, Shared.Drawing.ColorNegative(value));
            }
        }

        public DataGridView()
        {
            base.BackgroundColor = Color.White;

            InitializeComponent();

            Set_CellDefaultStyle(Color.White, Shared.Drawing.ColorNegative(Color.White));
        }

        /// <summary>
        /// This sets a new default cell style for both readonly and editing modes
        /// </summary>
        private void Set_CellDefaultStyle(Color BackGroundColor, Color CellFontColor)
        {
            if (_ReadOnly == true)
            {
                DataGridViewCellStyle dataGridViewcellStlye_ReadOnly = new DataGridViewCellStyle();

                dataGridViewcellStlye_ReadOnly.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
                dataGridViewcellStlye_ReadOnly.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                dataGridViewcellStlye_ReadOnly.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                dataGridViewcellStlye_ReadOnly.BackColor = BackGroundColor;

                dataGridViewcellStlye_ReadOnly.Font = _Font;
                dataGridViewcellStlye_ReadOnly.ForeColor = CellFontColor;
                dataGridViewcellStlye_ReadOnly.WrapMode = System.Windows.Forms.DataGridViewTriState.False;

                this.DefaultCellStyle = dataGridViewcellStlye_ReadOnly;
                this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;


                foreach (var item in this.Columns)
                {
                    if (item.GetType() == typeof(ModernGUI.Controls.Columns.AddRemoveColumn))
                    {
                        ((ModernGUI.Controls.Columns.AddRemoveColumn)item).Visible = false;
                    }
                }

            }
            else
            {
                DataGridViewCellStyle dataGridViewcellStlye = new DataGridViewCellStyle();

                dataGridViewcellStlye.SelectionBackColor = SystemColors.Highlight;
                dataGridViewcellStlye.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                dataGridViewcellStlye.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                dataGridViewcellStlye.BackColor = BackGroundColor;
                dataGridViewcellStlye.Font = _Font;
                dataGridViewcellStlye.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
                dataGridViewcellStlye.ForeColor = CellFontColor;

                this.DefaultCellStyle = dataGridViewcellStlye;
                this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;

                foreach (var item in this.Columns)
                {
                    if (item.GetType() == typeof(ModernGUI.Controls.Columns.AddRemoveColumn))
                    {
                        ((ModernGUI.Controls.Columns.AddRemoveColumn)item).Visible = true;
                    }
                }

            }
        }

        #region Set Dimensions

        /// <summary>
        /// This sets the default cell dimensions
        /// </summary>
        private void SetCellDimensions()
        {
            base.RowHeadersVisible = false;
            base.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            base.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            base.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;

            base.ColumnHeadersHeight = _HeaderHeight;
            base.RowTemplate.MinimumHeight = _RowHeight;
            base.RowTemplate.Height = _RowHeight;
        }

        private new void ReadOnlyChanged()
        {
            Set_CellDefaultStyle(BackColor, ModernGUI.Shared.Drawing.ColorNegative(BackColor));
            this.Invalidate();
        }
        #endregion

        #region DesignerCode

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (Parent != null)
                {
                    if (Parent.Controls.Contains(ScrollBar))
                    {
                        Parent.Controls.Remove(ScrollBar);
                    }
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, this, new object[] { true });

            this.Rows.Clear();

            DataGridViewCellStyle dataGridViewCellStyleCells = new DataGridViewCellStyle();

            dataGridViewCellStyleCells.Font = _Font;
            this.RowsDefaultCellStyle = dataGridViewCellStyleCells;

            this.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.ScrollBars = ScrollBars.None;

            this.Invalidate();

            SetCellDimensions();

        }
        bool OnBackgroundColorChanged_FirstRun = false;
        private void SetColorSchemeAndFont(Color BackGroundColor)
        {

            if (this.BackgroundColor == BackgroundColor && OnBackgroundColorChanged_FirstRun == false)
            {
                this.OnBackgroundColorChanged(new EventArgs());
                OnBackgroundColorChanged_FirstRun = true;
            }
            else
            {

                this.BackgroundColor = BackGroundColor;

            }

            _Font = SkinManager.openSans[10, OpenSans.Weight.Regular];
            _HeaderFont = SkinManager.openSans[11, OpenSans.Weight.Regular];
            _HeaderColor = SkinManager.ColorScheme.PrimaryColor;
        }
        #endregion

        #region Rounded Edges
        /// <summary>
        /// This allows me to give the control nice rounded edges
        /// </summary>
        /// <param name="nLeftRect"></param>
        /// <param name="nTopRect"></param>
        /// <param name="nRightRect"></param>
        /// <param name="nBottomRect"></param>
        /// <param name="nWidthEllipse"></param>
        /// <param name="nHeightEllipse"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        #endregion

        #region Drawing Control
        protected override void OnPaint(PaintEventArgs e)
        {

            SetColorSchemeAndFont(BackColor);

            base.OnPaint(e);
            if (_AllowUserToResize == true)
            {
                var rc = new Rectangle(this.ClientSize.Width - grab, this.ClientSize.Height - grab, grab, grab);
                ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            }
            ResizeDGVToFitColumns();

            Color myRgbColor_Dark = new Color();

            myRgbColor_Dark = ChangeColorBrightness(_HeaderColor, 0.20F);

            RectangleF Entiretop = new RectangleF(0, 0, this.Width, _HeaderHeight);

            base.ColumnHeadersHeight = _HeaderHeight;

            if (this.Columns.Count > 0)
            {

                if (_ListBoxStyle == true)
                {

                    e.Graphics.FillRectangle(new SolidBrush(Parent.BackColor), Entiretop);
                }
                else
                {
                    using (System.Drawing.Drawing2D.LinearGradientBrush gradient = new System.Drawing.Drawing2D.LinearGradientBrush(Entiretop, _HeaderColor, myRgbColor_Dark, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(gradient, Entiretop);
                    }
                }

                int CurrentPostion = 0;

                for (int i = 0; i < this.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        CurrentPostion += this.Columns[i - 1].Width;
                    }

                    Rectangle rectangle = new Rectangle(1 + CurrentPostion, 0, this.Columns[i].Width, _HeaderHeight);

                    if (this.Columns[i].GetType() != typeof(ModernGUI.Controls.Columns.AddRemoveColumn))
                    {
                        if (this.SortedColumn?.Index == i)
                        {
                            if (this.SortOrder == SortOrder.Ascending)
                            {
                                e.Graphics.FillPolygon(new SolidBrush(Color.Gray), ModernGUI.Shared.Drawing.CommonShapes.ReturnDownArrow(CurrentPostion + this.Columns[i].Width - 20, (_HeaderHeight / 2) - 3));
                            }
                            if (this.SortOrder == SortOrder.Descending)
                            {
                                e.Graphics.FillPolygon(new SolidBrush(Color.Gray), ModernGUI.Shared.Drawing.CommonShapes.ReturnUpArrow(CurrentPostion + this.Columns[i].Width - 20, (_HeaderHeight / 2) - 3));
                            }
                        }

                        if (_ListBoxStyle == true)
                        {
                            TextRenderer.DrawText(e.Graphics, this.Columns[i].HeaderText, _HeaderFont, rectangle, Color.FromArgb(146, 146, 146), TextFormatFlags.NoPrefix | TextFormatFlags.VerticalCenter);
                        }
                        else
                        {
                            TextRenderer.DrawText(e.Graphics, this.Columns[i].HeaderText, _HeaderFont, rectangle, Color.White, TextFormatFlags.NoPrefix | TextFormatFlags.VerticalCenter);
                        }
                    }
                }
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 7, 7));
            }

        }
        protected override void OnRowHeightChanged(DataGridViewRowEventArgs e)
        {
            _AllrowHeight = -1;
            base.OnRowHeightChanged(e);
        }
        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);

            SetScrollBar();
            this.Invalidate();

        }
        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);

            _AllrowHeight += this.Rows[e.RowIndex].Height;
            SetScrollBar();
            this.Invalidate();
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);
            if (this.RowCount > 0)
            {
                try
                {

                    _AllrowHeight -= this.Rows[e.RowIndex].Height;
                }
                catch (Exception)
                {
                    _AllrowHeight = -1;
                }
            }
            else
            {
                _AllrowHeight = -1;
            }
            SetScrollBar();
            this.Invalidate();
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);


            SetScrollBar();
            this.Invalidate();

        }

        private void ResizeDGVToFitColumns()
        {
            foreach (DataGridViewColumn column in this.Columns)
            {
                if (column.GetType() == typeof(ModernGUI.Controls.Columns.AddRemoveColumn))
                {
                    column.Width = 93;
                }
                if (column.Visible == true)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);


            ResizeDGVToFitColumns();

            SetScrollBar();
            this.Invalidate();
        }

        #endregion

        #region Custom ScrollBar

        private int _AllrowHeight = -1; //This stops pointless recalculation
        private int AllRowsHeight
        {
            get
            {

                int returnRowHeight = 0;
                foreach (DataGridViewRow row in this.Rows)
                {
                    if (row.Visible == true)
                    {
                        returnRowHeight += _RowHeight;
                    }
                }
                return returnRowHeight;
            }
        }


        private void SetScrollBar()
        {
            if (this.Parent == null) { return; }

            if (AllRowsHeight + HeaderHeight <= this.Height)
            {
                if (ScrollBar != null)
                {
                    ScrollBar.Scroll -= ScrollBar_Scroll;
                    this.Parent.Controls.Remove(ScrollBar);
                    ScrollBar = null;
                }
            }
            else
            {
                if (ScrollBar == null)
                {
                    ScrollBar = new VScrollBar();
                    this.Parent.Controls.Add(ScrollBar);
                    ScrollBar.Scroll += ScrollBar_Scroll;
                }

                if (AllRowsHeight + HeaderHeight > this.Height)
                {
                    ScrollBar.Height = this.Height - _HeaderHeight;
                    ScrollBar.Minimum = 0;
                    ScrollBar.SmallChange = 5;

                    ScrollBar.Maximum = this.RowCount *5;
                    ScrollBar.Location = new Point(this.Location.X + this.Width, this.Location.Y + _HeaderHeight);

                    ScrollBar.BringToFront();
                }
            }
        }

        private void DataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollBar.Value = e.NewValue;
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.FirstDisplayedScrollingRowIndex = e.NewValue / 5;
            this.Invalidate();
        }

        protected override void OnParentChanged(EventArgs e)
        {

            base.OnParentChanged(e);

            if (this.Parent != null && ScrollBar == null)
            {
                SetScrollBar();
            }


        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            SetScrollBar();
        }

        #endregion

        #region Allow user to increase height at runtime

        private const int grab = 16;
        protected override void WndProc(ref Message m)
        {

            base.WndProc(ref m);

            if (_AllowUserToResize == true)
            {

                if (m.Msg == 0x84)
                {  // Trap WM_NCHITTEST
                    Point DragToPoint = new Point(m.LParam.ToInt32());

                    var pos = this.PointToClient(DragToPoint);


                    if (pos.X >= this.ClientSize.Width - grab && pos.Y >= this.ClientSize.Height - grab)
                    {
                        m.Result = new IntPtr(17);  // Only allows for runtime increase of height
                    }

                }

            }

        }

        #endregion

        #region DragDrop
        /// <summary>
        /// Gives the user the abillity to drag and drop the control at runtime
        /// </summary>
        private Control activeControl;
        private Point previousLocation;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_AllowUserToDragDrop == true)
            {
                if (Cursor.Current == Cursors.Default)
                {
                    activeControl = this;
                    previousLocation = e.Location;
                    Cursor = Cursors.Hand;
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_AllowUserToDragDrop == true)
            {
                if (activeControl == null)
                {
                    return;
                }

                var location = activeControl.Location;
                location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
                activeControl.Location = location;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_AllowUserToDragDrop == true)
            {
                activeControl = null;
                Cursor = Cursors.Default;
            }
        }

        #endregion

    }
}
