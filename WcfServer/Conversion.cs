using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wcfServer;

namespace WcfServer
{
    public class Conversion
    {
        private static string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] tens = { "", "", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static string[] grades = { "", " thousand ", " million " };

        public static string NumberToWord(string numberStr)
        {
            try
            {
                OutgoingResponseFormat.SetResponseFormat(OutgoingResponseFormat.GetFormat());  //client can request for a json or xml response dynamically

                //numberStr = string.Concat(numberStr.Trim().Where(c => !char.IsWhiteSpace(c))); 
                numberStr = numberStr.Trim().Replace(" ", string.Empty);
                if (numberStr.Except("0123456789,").Any())
                    return "Invalid characters.";

                string cents = string.Empty;
                long number = 0;
                Boolean hasComma = false;

                if (numberStr.Contains(','))
                {
                    string[] numberSplit = numberStr.ToString().Split(',');
                    number = (numberSplit[0] == string.Empty) ? 0 : long.Parse(numberSplit[0]);
                    cents = (numberSplit[1] == string.Empty) ? "0" : numberSplit[1].TrimEnd('0');
                    hasComma = true;
                }
                else
                    number = long.Parse(numberStr);

                if (number < 0 || number > 999999999 || (cents != string.Empty && long.Parse(cents) > 99))
                    return "number is out of range.";

                return (hasComma == false) ? ConvertBeforeComma(number) : ConvertBeforeComma(number) + " and " + ConvertAfterComma(cents);
            }
            catch(Exception ex)
            {
                return "Service Error: " + ex.Message.ToString();
            }
        }
        private static string ConvertBeforeComma(long number)
        {
            if (number == 0)
                return "zero dollars";

            string wordFinal = string.Empty;
            int i = 0;

            while (number > 0)
            {
                string firstThreeNumber = GetFirstThreeNumber(number);
                string convertResult = ConvertFirstThree(firstThreeNumber);
                if (convertResult == string.Empty)
                    i++;
                else
                    wordFinal = convertResult + grades[i++] + wordFinal;
                number = GetRemaining(number);
            }
            wordFinal += (wordFinal == "one") ? " dollar" : " dollars";
            return wordFinal;
        }

        private static string GetFirstThreeNumber(long number)
        {
            string numStr = number.ToString().PadLeft(3, '0');
            return numStr.Substring(numStr.Length - 3, 3);
        }
        private static long GetRemaining(long number)
        {
            string numStr = number.ToString();
            if (numStr.Length <= 3)
                return 0;
            else
                return long.Parse(numStr.Substring(0, numStr.Length - 3));
        }
        private static string ConvertFirstThree(string numStr)
        {
            string word = string.Empty;
            if (int.Parse(numStr) == 0)
                return word;

            int thirdDigit = int.Parse(numStr[0].ToString());
            int secondDigit = int.Parse(numStr[1].ToString());
            int firstDigit = int.Parse(numStr[2].ToString());

            word = (thirdDigit == 0) ? "" : ones[thirdDigit] + " hundred ";                                                             //hundreds
            word += (secondDigit == 0 || secondDigit == 1) ? "" : ((firstDigit == 0) ? tens[secondDigit] : tens[secondDigit] + "-");    //tens
            word += (secondDigit == 0 || secondDigit == 1) ? ones[secondDigit * 10 + firstDigit] : ones[firstDigit];                      //ones
            return word;
        }

        private static string ConvertAfterComma(string cent)
        {
            if (cent == string.Empty || long.Parse(cent) == 0)
                return "zero cents";

            string word = string.Empty;
            cent = cent.PadRight(2, '0');

            int secondDigit = int.Parse(cent[0].ToString());
            int firstDigit = int.Parse(cent[1].ToString());

            word += (secondDigit == 0 || secondDigit == 1) ? "" : ((firstDigit == 0) ? tens[secondDigit] : tens[secondDigit] + "-");    //tens
            word += (secondDigit == 0 || secondDigit == 1) ? ones[secondDigit * 10 + firstDigit] : ones[firstDigit];                    //ones

            word += (word == "one") ? " cent" : " cents";
            return word;
        }
    }
}