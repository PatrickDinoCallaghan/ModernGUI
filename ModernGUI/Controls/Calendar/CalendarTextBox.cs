namespace ModernGUI.Controls
{
    public class CalendarTextBox
        : System.Windows.Forms.TextBox
    {
        #region Fields
        private Calendar _calendar;
        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="CalendarTextBox"/> for the specified <see cref="Calendar"/>
        /// </summary>
        /// <param name="calendar">Calendar where this control lives</param>
        public CalendarTextBox(Calendar calendar)
        {
            _calendar = calendar;
        }

        #endregion

        #region Props

        /// <summary>
        /// Gets the calendar where this control lives
        /// </summary>
        public Calendar Calendar
        {
            get { return _calendar; }
        }


        #endregion

        #region Methods



        #endregion
    }
}
