using System.Text.RegularExpressions;

namespace ModernGUI.Shared
{
    public static class DataValidation
    {            /// <summary>
                 /// This checks if the object is either null or a blank string, ie inval == "" or inval == null
                 /// </summary>
                 /// <param name="inval">Object you are checking is empty or null</param>
                 /// <returns></returns>
        public static bool StringIsEmpyOrNull(object inval)
        {
            if (inval != null)
            {
                if (Convert.ToString(inval) == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        public static bool IsNumeric(object inValue)
        {
            bool bValid = false;
            try
            {
                double myDT = Convert.ToDouble(inValue.ToString());
                bValid = true;
            }
            catch (FormatException e)
            {
                bValid = false;
            }

            return bValid;
        }
        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }

        public static bool OnlyLetters(string value, bool ExcludeSpace = false)
        {
            //

            if (ExcludeSpace)
            {
                return Regex.IsMatch(value, @" ^[a-zA-Z]+$");
            }
            else
            {
                return Regex.IsMatch(value, @"^[A-Za-z ]+$");

            }


        }
        public static bool OnlyLettersAndNumbers(string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"); ;
        }


        public static class KeyboardInput
        {
            public static bool EditKeys(char KeyCode)
            {
                return EditKeys((System.Windows.Forms.Keys)KeyCode);
            }
            public static bool EditKeys(System.Windows.Forms.Keys Key)
            {

                if (Key == System.Windows.Forms.Keys.Back || Key == System.Windows.Forms.Keys.Enter
                     || Key == System.Windows.Forms.Keys.Down || Key == System.Windows.Forms.Keys.Up || Key == System.Windows.Forms.Keys.Left || Key == System.Windows.Forms.Keys.Right
                     || Key == System.Windows.Forms.Keys.Delete)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
