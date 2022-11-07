namespace ModernGUI.Controls
{
    /// <summary>
    /// Interface implemented by every selectable element of the calendar
    /// </summary>
    public interface ICalendarSelectableElement
        : ISelectableElement, IComparable<ICalendarSelectableElement>
    {

        /// <summary>
        /// Gets the calendar this element belongs to
        /// </summary>
        Calendar Calendar { get; }

        /// <summary>
        /// Gets the calendar
        /// </summary>
        DateTime Date { get; }

    }
}
