using System.ComponentModel;
using System.Reflection;

namespace ModernGUI.Controls
{
    #region Add remove Row 

    [Browsable(true), ToolboxItem(true), DesignTimeVisible(true), DataGridViewColumnDesignTimeVisible(true)]
    public class AddRemoveColumn : AlwaysVisibleHostedControlColumn
    {
        /// <summary>
        /// I always want this to be blank without text for the column header.
        /// </summary>
        public AddRemoveColumn() : base(new AddRemove())
        {
            ((AddRemove)base.SelectionControl).OnAddClicked += AddClicked;
            ((AddRemove)base.SelectionControl).OnRemoveClicked += RemoveClicked;
        }

        private void AddClicked(int RowIndex)
        {
            DataGridView.Rows.Insert(RowIndex + 1, 1);
        }

        private void RemoveClicked(int RowIndex)
        {
            DataGridView.Rows.RemoveAt(RowIndex);

            if (DataGridView.Rows.Count == 0)
            {
                DataGridView.Rows.Add();
            }

            if (RowIndex > this.DataGridView.RowCount - 1)
            {
                SelectionControl.Visible = false;
            }
        }
    }

    #endregion

    #region Progress Column
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
            this.DefaultCellStyle.NullValue = 0; //Untested., Delete on error
        }
    }

    public class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;
        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);



        }

        private int _Value = 0;
        public new int Value { get { return _Value; } set { _Value = value; DataGridView.InvalidateCell(this); } }
        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(int);
        }
        // Method required to make the Progress Cell consistent with the default Image Cell. 
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
                            int rowIndex, ref DataGridViewCellStyle cellStyle,
                            TypeConverter valueTypeConverter,
                            TypeConverter formattedValueTypeConverter,
                            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }



        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            try
            {

                int progressVal = Convert.ToInt32(_Value);


                float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C0 returns int which is 0 for anything but 100%.
                Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
                Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
                // Draws the cell grid
                base.Paint(g, clipBounds, cellBounds,
                 rowIndex, cellState, value, formattedValue, errorText,
                 cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

                if (percentage > 0.0)
                {
                    // Draw the progress bar and the text
                    g.FillRectangle(new SolidBrush(Color.FromArgb(203, 235, 108)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);
                    //g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + (cellBounds.Width / 2) - 5, cellBounds.Y + 2);

                    g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + (cellBounds.Width / 2) - 5, cellBounds.Y + (cellBounds.Height / 2) - cellStyle.Font.Height / 2);
                }
                else
                {
                    // draw the text
                    if (this.DataGridView.CurrentRow.Index == rowIndex)
                        g.DrawString(progressVal.ToString() + "%", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + (cellBounds.Width / 2) - 5, cellBounds.Y + (cellBounds.Height / 2) - cellStyle.Font.Height / 2);
                    else
                        g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + (cellBounds.Width / 2) - 5, cellBounds.Y + (cellBounds.Height / 2) - cellStyle.Font.Height / 2);
                }

            }
            catch (Exception ex)
            {
                base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            }

        }

    }
    #endregion

    [Browsable(false), ToolboxItem(false), DesignTimeVisible(false), DataGridViewColumnDesignTimeVisible(false)]
    public class AlwaysVisibleHostedControlColumn : DataGridViewImageColumn
    {
        #region Hidden base Properties
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCellStyle DefaultCellStyle { get { return base.DefaultCellStyle; } }


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int MinimumWidth { get { return base.MinimumWidth; } }


        [DefaultValue(DataGridViewColumnSortMode.NotSortable)]

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewColumnSortMode SortMode { get { return base.SortMode; } }


        [DefaultValue("")]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public new string DataPropertyName { get { return base.DataPropertyName; } }


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string HeaderText
        {
            get { return ""; }
        }

        #endregion

        #region Fields 

        public Control SelectionControl = null;
        private Bitmap SelectionControlImage = null;

        #endregion

        /// <summary>
        /// I always want this to be blank without text for the column header.
        /// </summary>
        public AlwaysVisibleHostedControlColumn(Control hostedControl)
        {
            SelectionControl = hostedControl;
            this.Resizable = DataGridViewTriState.False;

            base.HeaderText = "";
        }

        #region Set Up Column
        protected override void OnDataGridViewChanged()
        {
            base.OnDataGridViewChanged();
            if (DataGridView != null)
            {
                Activate();
            }
        }

        private void SetListeners()
        {
            SelectionControl.LostFocus += SelectionControl_LostFocus;
            this.DataGridView.CellMouseEnter += DataGridView_CellMouseEnter;
            this.DataGridView.BackgroundColorChanged += DataGridView_BackgroundColorChanged;
            this.DataGridView.RowHeightChanged += DataGridView_RowHeightChanged;
            this.DataGridView.ColumnWidthChanged += DataGridView_ColumnWidthChanged;
            this.DataGridView.Scroll += DataGridView_Scroll;
            this.DataGridView.RowsAdded += DataGridView_ColumnEowNoChanged;
            this.DataGridView.RowsRemoved += DataGridView_ColumnEowNoChanged;
            this.DataGridView.ColumnAdded += DataGridView_ColumnEowNoChanged;
            this.DataGridView.ColumnRemoved += DataGridView_ColumnEowNoChanged;
        }

        private void DataGridView_ColumnEowNoChanged(object? sender, EventArgs e)
        {
            System.Drawing.Point pt = DataGridView.PointToClient(Cursor.Position);
            DataGridView.HitTestInfo hittest = DataGridView.HitTest(pt.X, pt.Y);

            if (!(hittest.ColumnIndex == -1 || hittest.RowIndex == -1))
            {
                SetPosition(hittest.ColumnIndex, hittest.RowIndex);
            }
            else
            {
                SelectionControl.Visible = false;
            }
        }

        private void Activate()
        {
            SetListeners();

            this.DataGridView.Controls.Add(SelectionControl);

            SelectionControl.Visible = false;

            this.Width = SelectionControl.Width;
            SelectionControl.BackColor = this.DataGridView.BackgroundColor;

            this.DataGridView.RowTemplate.Height = SelectionControl.Height + 1;

            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                row.Height = SelectionControl.Height + 1;
            }

            SetNullImage();

            this.DataGridView.AllowUserToAddRows = false;
        }
        #endregion

        #region Events Controlling Behavior

        private void DataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            SelectionControl.Visible = false;
        }

        private void DataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            SelectionControl.Visible = false;
        }

        private void SetNullImage()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, this.DataGridView, new object[] { false }); // Need to turn off double buffering to set null val

            if (SelectionControlImage != null)
            {
                SelectionControlImage.Dispose();
            }

            SelectionControl.Visible = true;
            SelectionControlImage = new Bitmap(SelectionControl.Width, SelectionControl.Height);

            SelectionControl.DrawToBitmap(SelectionControlImage, new Rectangle(0, 0, SelectionControlImage.Width, SelectionControlImage.Height));

            this.DefaultCellStyle.NullValue = SelectionControlImage;

            SelectionControl.Visible = false;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, this.DataGridView, new object[] { true });
        }

        private void DataGridView_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Height <= 40)
            {
                e.Row.Height = 40;
            }

            SelectionControl.Visible = false;
            SetPosition(Index, e.Row.Index);
        }

        private void DataGridView_BackgroundColorChanged(object sender, EventArgs e)
        {
            SelectionControl.BackColor = this.DataGridView.BackgroundColor;

            SetNullImage();
            
        }

        private void SelectionControl_LostFocus(object sender, EventArgs e)
        {
            SelectionControl.Visible = false;
        }

        public void SetPosition(int ColumnIndex, int RowIndex)
        {
            Rectangle celrec = this.DataGridView.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);//.Rows[e.RowIndex].Cells[e.ColumnIndex].GetContentBounds();

            int x_Offet = (celrec.Width - SelectionControl.Width) / 2;
            int y_Offet = (celrec.Height - SelectionControl.Height) / 2;

            SelectionControl.Location = new Point(celrec.X + x_Offet, celrec.Y + y_Offet);
            SelectionControl.Visible = true;
            //  SelectionControl.RowIndex = RowIndex;
        }

        private void DataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.Index && e.RowIndex > -1)
            {
                Rectangle displayCellRect = this.DataGridView.GetCellDisplayRectangle(DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ColumnIndex, DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].RowIndex, true);

                if (displayCellRect.Height == this.DataGridView.Rows[DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].RowIndex].Height)
                {
                    SetPosition(e.ColumnIndex, e.RowIndex);
                }
            }
        }

        #endregion

    }
}

