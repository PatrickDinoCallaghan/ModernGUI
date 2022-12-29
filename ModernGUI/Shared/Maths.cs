namespace ModernGUI.Shared
{
    public static class Maths
    {
        public static class Probability
        {
            public static class Odds
            {
                public static decimal Probability(string Fraction)
                {
                    string[] InOdds_arr = Fraction.Split('/');

                    if (Fraction.Contains('/'))
                    {
                        if (InOdds_arr.Length == 2)
                        {
                            if (ModernGUI.Shared.DataValidation.IsNumeric(InOdds_arr[0]) &&
                               ModernGUI.Shared.DataValidation.IsNumeric(InOdds_arr[1]))
                            {
                                return Probability(Convert.ToDecimal(InOdds_arr[0]), Convert.ToDecimal(InOdds_arr[1]));
                            }
                        }
                    }
                    throw new ArgumentException("Fractional odds not given in the right format.");
                }
                public static decimal Probability(decimal InNumerator, decimal InDenominator)
                {
                    return 1 - (InNumerator / (InNumerator + InDenominator));
                }

                public enum Format
                {
                    unknown,
                    Fractional,
                    Europeandecimal,
                    MoneyLine,
                }


                public static Format GetFormat(string InOdds)
                {
                    if (ModernGUI.Shared.DataValidation.IsNumeric(InOdds))
                    {
                        return Format.Fractional;
                    }
                    else if (InOdds.Contains('/'))
                    {
                        string[] OddsArr = InOdds.Split('/');
                        if (OddsArr.Length == 2 && InOdds.Length > 2)
                        {
                            if (OddsArr[0].Length > 0 && OddsArr[1].Length > 0)
                            {
                                if (ModernGUI.Shared.DataValidation.IsNumeric(OddsArr[0]) && ModernGUI.Shared.DataValidation.IsNumeric(OddsArr[1]))
                                {
                                    return Format.Fractional;
                                }
                            }
                        }
                    }

                    return Format.unknown;
                }
            }

        }
        public static class OverlapChecking
        {
            /// <summary>
            /// Checks if two rectangles overlap, If they do, return true.
            /// </summary>
            /// <param name="Rec1"></param>
            /// <param name="Rec2"></param>
            /// <returns></returns>
            public static bool Clash(Rectangle Rec1, Rectangle Rec2)
            {
                if (!(Rec1.Location.X >= Rec2.Location.X + Rec2.Width || Rec2.Width >= Rec1.Location.X + Rec1.Width))
                {
                    return true;
                }
                return false;
            }
            /// <summary>
            /// This is the clash check that all other clash checks are derived from. It checks if two periods of time clash
            /// </summary>
            /// <param name="Start1"></param>
            /// <param name="End1"></param>
            /// <param name="Start2"></param>
            /// <param name="End2"></param>
            /// <returns></returns>
            public static bool Clash(DateTime Start1, DateTime End1, DateTime Start2, DateTime End2)
            {
                if (!(Start1 >= End2 || Start2 >= End1))
                {
                    return true;
                }
                return false;
            }
        }
        public static int mod(int inint)
        {
            return ((inint * inint) ^ (1 / 2));
        }
        public static TimeSpan mod(TimeSpan InSpan)
        {
            TimeSpan BlankTimespan = new TimeSpan();

            if (InSpan < BlankTimespan)
            {
                return -InSpan;
            }
            else
            {
                return InSpan;
            }
        }
        public static bool WithinRange(double Value, double Min, double Max)
        {
            if ((Value - Min) * (Max - Value) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckValueIsWithinRangeOfValue(double Value, double checkvalue, double tolerance)
        {
            if (tolerance == 1)
            {
                return true;
            }
            double MinMax = Math.Abs(Value * tolerance);

            return WithinRange(checkvalue, Value - MinMax, Value + MinMax);

        }
    }
}
