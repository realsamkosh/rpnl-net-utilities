﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.ValidationUtil
{
    public class ValidationHelpers
    {

        public static bool ValidateGrade(string _grade)
        {
            List<string> Grade = new List<string>() { "A", "B", "C", "P", "F", "PP", "NA", "AB" };
            return Grade.Contains(_grade);
        }

        public static Boolean ValidateNumericValue(String Value)
        {
            return Int32.TryParse(Value, out int number);
        }

        public static Boolean ValidateDecimalValue(String Value)
        {
            return Decimal.TryParse(Value, out decimal number);
        }

        public bool IsAlphaNumeric(string input) => Regex.IsMatch(input, "^[a-zA-Z0-9 ]+$");
        public bool IsAlphabetsOnly(string input) => Regex.IsMatch(input, "^[a-zA-Z ]+$");
        public bool IsNumberOnly(string input) => Regex.IsMatch(input, "^[0-9]*$");
    }
}
