﻿namespace ModernGUI.Shared
{
    public static class DateTimeTools
    {
        public static DateTime ZeroDateTime(DateTime InDatetime)
        {
            return DateTime.ParseExact(InDatetime.ToString("dd/MM/yyyy") + " 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
