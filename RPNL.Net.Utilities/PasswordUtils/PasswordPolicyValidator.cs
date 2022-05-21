﻿using RPNL.Net.Utilities.ResponseUtil;
using System.Collections.Generic;
using System.Linq;

namespace RPNL.Net.Utilities.PasswordUtils
{
    public class PasswordPolicyValidator
    {        
        public static List<string> ValidatePasswordPolicy(string password, bool uppercase, bool requiredigit, bool lowercase, int requiredlength, bool reqnonalphanumeric)
        {
            List<string> result = new List<string>();
            //Check the Expected Length First
            if (password.Length < requiredlength)
            {
                result.Add($"The Minimum required Password Length is {requiredlength}.");
            }

            // Check Upper Case 360 degree
            if (uppercase == true && !password.Any(char.IsUpper))
            {
                result.Add($"Upper Case Letter is required in your password.");
            }

            if (uppercase == false && password.Any(char.IsUpper))
            {
                result.Add($"Upper Case Letter is not required in your password.");
            }

            //Check Lower case 360 degree
            if (lowercase == true && !password.Any(char.IsLower))
            {
                result.Add($"Lower Case Letter is required.");
            }

            if (uppercase == false && password.Any(char.IsLower))
            {
                result.Add($"Lower Case Letter is not required in your password.");
            }

            //Check Digit case 360 degree
            if (requiredigit == true && !password.Any(char.IsDigit))
            {
                result.Add($"At least one digit is required.");
            }

            if (requiredigit == false && password.Any(char.IsDigit))
            {
                result.Add($"At leat one digit is not required in your password.");
            }

            //Check Non Aplhanuemric case 360 degree
            var list = new[] { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "\"", "_", "?", "-" };
            if (reqnonalphanumeric == true && !list.Any(password.Contains))
            {
                result.Add($"At least one special character ({string.Join("", list)}) is required.");
            }

            if (reqnonalphanumeric == false && list.Any(password.Contains))
            {
                result.Add($"Special Character is not required in your password.");
            }

            //Words That cannot be used as Password
            if (password.ToLower().Contains("password"))
            {
                result.Add(string.Format(ResponseErrorMessages.CannotContainPassword, "password"));
            }

            return result;
        }
    }

}
