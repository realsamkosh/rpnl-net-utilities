using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.ValidationUtils
{
    public class ValidationHelpers
    {

        public static bool ValidateGrade(string _grade)
        {
            List<string> Grade = new List<string>() { "A", "B", "C", "P", "F", "PP", "NA", "AB" };
            return Grade.Contains(_grade);
        }

        public static bool ValidateNumericValue(string Value)
        {
            return int.TryParse(Value, out int number);
        }

        public static bool ValidateDecimalValue(string Value)
        {
            return decimal.TryParse(Value, out decimal number);
        }

        public static bool IsAlphaNumeric(string input) => Regex.IsMatch(input, "^[a-zA-Z0-9 ]+$");
        public static bool IsAlphabetsOnly(string input) => Regex.IsMatch(input, "^[a-zA-Z ]+$");
        public static bool IsNumberOnly(string input) => Regex.IsMatch(input, "^[0-9]*$");
    }
}
