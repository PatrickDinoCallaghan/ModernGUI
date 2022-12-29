using System.Collections.ObjectModel;
using System.Windows.Media;


namespace RTFEditor
{
    /// <summary>
    /// Generates a string list of all fonts installed on the system
    /// </summary>
    class FontList : ObservableCollection<string>
    {
        public FontList()
        {
            foreach (System.Windows.Media.FontFamily f in Fonts.SystemFontFamilies)
            {
                this.Add(f.ToString());
            }
        }
    }
}
