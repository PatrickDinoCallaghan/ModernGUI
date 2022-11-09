using ModernGUI.Properties;
using ModernGUI.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModernGUI
{

    /// <summary>
    /// This instantiate OpenSans fonts as and when they are required in one neat easy to use class.
    /// </summary>
    public class OpenSans
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);

        public enum Weight
        {
            Regular,
            Medium
        }

        private FontFamily LoadFont(byte[] fontResource)
        {
            int dataLength = fontResource.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontResource, 0, fontPtr, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(fontPtr, (uint)fontResource.Length, IntPtr.Zero, ref cFonts);
            privateFontCollection.AddMemoryFont(fontPtr, dataLength);

            return privateFontCollection.Families.Last();
        }

        private readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        /// <summary>
        /// This jagged dictionary ensures only required fonts are instantiated once
        /// </summary>
        private Dictionary<Weight, Dictionary<int, Font>> _LoadedFonts = new Dictionary<Weight, Dictionary<int, Font>>();
        public Font this[int size, Weight weight = Weight.Regular]
        {
            get
            {
                if ((_LoadedFonts.ContainsKey(weight)))
                {
                    Dictionary<int, Font> temp = _LoadedFonts[weight];
                    if (temp.ContainsKey(size))
                    {
                        return temp[size];
                    }
                }
                switch (weight)
                {
                    case Weight.Regular:
                        if (!_LoadedFonts.ContainsKey(weight))
                        {
                            _LoadedFonts.Add(weight, new Dictionary<int, Font>());
                        }
                        _LoadedFonts[weight].Add(size, new Font(LoadFont(Resources.OpenSans_Regular), size));
                        return _LoadedFonts[weight][size];
                    default:
                        if (!_LoadedFonts.ContainsKey(weight))
                        {
                            _LoadedFonts.Add(weight, new Dictionary<int, Font>());
                        }
                        _LoadedFonts[weight].Add(size, new Font(LoadFont(Resources.OpenSans_Medium), size));
                        return _LoadedFonts[weight][size];
                }
            }
        }
    }
}
