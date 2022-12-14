namespace ModernGUI.Controls
{
    /// <summary>
    /// Holds data of a Calendar Loading Items of certain date range
    /// </summary>
    public class CalendarLoadEventArgs
        : EventArgs
    {
        #region Fields
        private Calendar _calendar;
        private DateTime _dateStart;
        private DateTime _dateEnd;

        #endregion

        #region Constructor

        public CalendarLoadEventArgs(Calendar calendar, DateTime dateStart, DateTime dateEnd)
        {
            _calendar = calendar;
            _dateEnd = dateEnd;
            _dateStart = dateStart;
        }

        #endregion

        #region Props

        /// <summary>
        /// Gets the calendar that originated the event
        /// </summary>
        public Calendar Calendar
        {
            get { return _calendar; }
        }

        /// <summary>
        /// Gets the start date of the load
        /// </summary>
        public DateTime DateStart
        {
            get { return _dateStart; }
            set { _dateStart = value; }
        }

        /// <summary>
        /// Gets the end date of the load
        /// </summary>
        public DateTime DateEnd
        {
            get { return _dateEnd; }
        }


        #endregion
    }
}
