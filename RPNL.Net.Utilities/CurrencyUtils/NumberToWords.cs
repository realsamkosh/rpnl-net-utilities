using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.CurrencyUtils
{
    public class NumberToWords
    {
        private static string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
                                          "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",  "Seventeen", "Eighteen", "Nineteen" };
        private static string[] tensMap = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        //var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        //var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };


        public static string ConvertAmount(decimal amount, string currency, string unitcur)
        {
            try
            {
                long amount_int = (long)amount;
                long amount_dec = (long)Math.Round((amount - amount_int) * 100);
                if (amount_dec == 0)
                {
                    return ConvertCurrency(amount_int) + $" {currency} Only.";
                }
                else
                {
                    return ConvertCurrency(amount_int) + $" {currency} " + ConvertCurrency(amount_dec) + $" {unitcur} Only.";
                }
            }
            catch (Exception e)
            {
                var result = $"{e}";
                // TODO: handle exception  
            }
            return "";
        }
        private static string ConvertCurrency(long number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ConvertCurrency(Math.Abs(number));

            string words = "";

            if (number / 1000000 > 0)
            {
                words += ConvertCurrency(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words += ConvertCurrency(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += ConvertCurrency(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";


                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if (number % 10 > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        //https://www.c-sharpcorner.com/article/convert-numeric-value-into-words-currency-in-c-sharp/
    }

}
