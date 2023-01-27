using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ModernGUI.Shared
{


    public static class StringTools
    {
        #region WordWrap

        private const string _newline = "\r\n";

        public static string WordWrap(string the_string, int width = 75)
        {
            if (the_string == null)
            {
                return "";
            }
            int pos, next;
            StringBuilder sb = new StringBuilder();

            // Lucidity check
            if (width < 1)
                return the_string;

            // Parse each line of text
            for (pos = 0; pos < the_string.Length; pos = next)
            {
                // Find end of line
                int eol = the_string.IndexOf(_newline, pos);

                if (eol == -1)
                    next = eol = the_string.Length;
                else
                    next = eol + _newline.Length;

                // Copy this line of text, breaking into smaller lines as needed
                if (eol > pos)
                {
                    do
                    {
                        int len = eol - pos;

                        if (len > width)
                            len = BreakLine(the_string, pos, width);

                        sb.Append(the_string, pos, len);
                        sb.Append(_newline);

                        // Trim whitespace following break
                        pos += len;

                        while (pos < eol && Char.IsWhiteSpace(the_string[pos]))
                            pos++;

                    } while (eol > pos);
                }
                else sb.Append(_newline); // Empty line
            }

            return sb.ToString();
        }

        private static int BreakLine(string text, int pos, int max)
        {
            // Find last whitespace in line
            int i = max - 1;
            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
                i--;
            if (i < 0)
                return max; // No whitespace found; break at maximum length
                            // Find start of whitespace
            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
                i--;
            // Return length of text before whitespace
            return i + 1;
        }

        #endregion

        #region String Similarity index

        public static bool StringsAreVerySimilar(string InStr1, string InStr2)
        {

            if (System.Text.RegularExpressions.Regex.Replace(InStr1.ToLower(), @"\s+", "") == System.Text.RegularExpressions.Regex.Replace(InStr2.ToLower(), @"\s+", ""))
            {
                return true;
            }

            return false;
        }

        public static bool StringsAreSimilar(string InStr1, string InStr2, double JWD = 0.15) // First actual good thing ive done in weeks
        {
            InStr1 = InStr1.Trim(); // Trim out any stupid whitespace. The user didnt mean to do this....
            InStr2 = InStr2.Trim();

            // First you need to check if an abbrivation has been used GSK is similar to glaxosmithkline
            char[] InStr1_CharArray = InStr1.ToCharArray();
            char[] InStr2_CharArray = InStr2.ToCharArray();

            string InStr1a_Abr = ""; string InStr2a_Abr = ""; // You are now comparing 4 strings moron, great idea, your CPU hates you.
            string InStr1b_Abr = ""; string InStr2b_Abr = ""; // 6 now, well done. Hope its worth it.

            //We do this so we dont keep comparing two non blank lists
            List<string> InStr1_List = new List<string>();    // This will be the non blank emtpy list of strings from InStr1
            List<string> InStr2_List = new List<string>();    // This will be the non blank emtpy list of strings from InStr2

            #region Loads Lists with possible abbreviations

            //Check all uppcase letters in a string.
            foreach (char item in InStr1_CharArray) // Makes an abbreviation from the first string input
            {
                if (char.IsUpper(item) == true)
                {
                    InStr1a_Abr = InStr1a_Abr + item;
                }
            }
            if (InStr1a_Abr != "")
            {
                InStr1_List.Add(InStr1a_Abr);
            }

            foreach (char item in InStr2_CharArray) // Makes an abbreviation from the second string input
            {
                if (char.IsUpper(item) == true)
                {
                    InStr2a_Abr = InStr2a_Abr + item;
                }
            }
            if (InStr1b_Abr != "")
            {
                InStr2_List.Add(InStr2a_Abr);
            }

            if (InStr1.Contains(" ") == true)
            {
                string[] Str_Arr_Temp1 = InStr1.Split(' ');
                foreach (string item in Str_Arr_Temp1)
                {
                    InStr1b_Abr = InStr1b_Abr + item.Substring(0, 1);
                }
            }
            if (InStr1b_Abr != "")
            {
                InStr1_List.Add(InStr1b_Abr);
            }

            if (InStr2.Contains(" ") == true)
            {
                string[] Str_Arr_Temp2 = InStr2.Split(' ');

                foreach (string item in Str_Arr_Temp2)
                {
                    InStr2b_Abr = InStr2b_Abr + item.Substring(0, 1);
                }
            }
            if (InStr2b_Abr != "")
            {
                InStr2_List.Add(InStr2b_Abr);
            }
            #endregion

            InStr1_List.Add(InStr1);
            InStr2_List.Add(InStr2);
            foreach (string item1 in InStr1_List)
            {
                foreach (string item2 in InStr2_List)
                {
                    if (item1.ToLower() == item2.ToLower()) // Keeping it static will improve performace
                    {
                        return true;
                    }
                }
            }

            if (JaroWinklerDistance.distance(InStr1, InStr2) < JWD) // Keeping it static will improve performace
            {
                return true;
            }

            return false;
        }

        public static class JaroWinklerDistance
        {
            /* The Winkler modification will not be applied unless the 
             * percent match was at or above the mWeightThreshold percent 
             * without the modification. 
             * Winkler's paper used a default value of 0.7
             */
            private static readonly double mWeightThreshold = 0.7;

            /* Size of the prefix to be concidered by the Winkler modification. 
             * Winkler's paper used a default value of 4
             */
            private static readonly int mNumChars = 4;

            /// <summary>
            /// Returns the Jaro-Winkler distance between the specified  
            /// strings. The distance is symmetric and will fall in the 
            /// range 0 (perfect match) to 1 (no match). 
            /// </summary>
            /// <param name="aString1">First String</param>
            /// <param name="aString2">Second String</param>
            /// <returns></returns>
            public static double distance(string aString1, string aString2)
            {
                return 1.0 - proximity(aString1, aString2);
            }

            /// <summary>
            /// Returns the Jaro-Winkler distance between the specified  
            /// strings. The distance is symmetric and will fall in the 
            /// range 0 (no match) to 1 (perfect match). 
            /// </summary>
            /// <param name="aString1">First String</param>
            /// <param name="aString2">Second String</param>
            /// <returns></returns>
            public static double proximity(string aString1, string aString2)
            {
                int lLen1 = aString1.Length;
                int lLen2 = aString2.Length;
                if (lLen1 == 0)
                    return lLen2 == 0 ? 1.0 : 0.0;

                int lSearchRange = System.Math.Max(0, Math.Max(lLen1, lLen2) / 2 - 1);

                // default initialized to false
                bool[] lMatched1 = new bool[lLen1];
                bool[] lMatched2 = new bool[lLen2];

                int lNumCommon = 0;
                for (int i = 0; i < lLen1; ++i)
                {
                    int lStart = Math.Max(0, i - lSearchRange);
                    int lEnd = Math.Min(i + lSearchRange + 1, lLen2);
                    for (int j = lStart; j < lEnd; ++j)
                    {
                        if (lMatched2[j]) continue;
                        if (aString1[i] != aString2[j])
                            continue;
                        lMatched1[i] = true;
                        lMatched2[j] = true;
                        ++lNumCommon;
                        break;
                    }
                }
                if (lNumCommon == 0) return 0.0;

                int lNumHalfTransposed = 0;
                int k = 0;
                for (int i = 0; i < lLen1; ++i)
                {
                    if (!lMatched1[i]) continue;
                    while (!lMatched2[k]) ++k;
                    if (aString1[i] != aString2[k])
                        ++lNumHalfTransposed;
                    ++k;
                }
                // System.Diagnostics.Debug.WriteLine("numHalfTransposed=" + numHalfTransposed);
                int lNumTransposed = lNumHalfTransposed / 2;

                // System.Diagnostics.Debug.WriteLine("numCommon=" + numCommon + " numTransposed=" + numTransposed);
                double lNumCommonD = lNumCommon;
                double lWeight = (lNumCommonD / lLen1
                                 + lNumCommonD / lLen2
                                 + (lNumCommon - lNumTransposed) / lNumCommonD) / 3.0;

                if (lWeight <= mWeightThreshold) return lWeight;
                int lMax = Math.Min(mNumChars, Math.Min(aString1.Length, aString2.Length));
                int lPos = 0;
                while (lPos < lMax && aString1[lPos] == aString2[lPos])
                    ++lPos;
                if (lPos == 0) return lWeight;
                return lWeight + 0.1 * lPos * (1.0 - lWeight);

            }

        }


        #endregion
        public static string BulletList(List<string> StringList, string AdditionalText = "") //This returns a bullet point list of every string within a list
        {
            string OutPutStr = "";
            int EventCount = 0;

            foreach (string ListedStr in StringList)
            {
                if (EventCount == StringList.Count)

                {
                    OutPutStr = OutPutStr + "\u2022" + ListedStr + AdditionalText;
                }
                else
                {
                    OutPutStr = OutPutStr + "\u2022" + ListedStr + AdditionalText + "\n";
                }
                EventCount++;
            }

            return OutPutStr;
        }

        public static string GetNumbers(string input)
        {
            string RtnString = new string(input.Where(c => char.IsDigit(c)).ToArray());

            if (RtnString != "")
            {
                return RtnString;

            }
            else
            {
                return "0";
            }

        }
        public static string GetNumbersDouble(string input)
        {
            char[] chars = input.Where(c => char.IsDigit(c) || c == '.').ToArray();

            string RtnString = "";

            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsDigit(chars[i]) == true || chars[i] == '.' && i != 0 && RtnString.Contains('.') == false)
                {
                    RtnString = RtnString + chars[i];
                }
            }

            if (RtnString.Length > 0)
            {
                if (RtnString != "" && Char.IsDigit(RtnString.ToArray()[0]))
                {
                    return RtnString;
                }
            }

            return "0";
        }



        // Allows you to take the jarowiklerDistance of a string through an extension method
        public static double Similarities(this string value, string aString1)
        {
            return JaroWinklerDistance.distance(value, aString1);
        }    
    }
}




