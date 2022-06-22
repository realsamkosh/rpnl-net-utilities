using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.ValidationUtils
{
    public static class CustomValidators
    {
        //public static IRuleBuilderOptions<T, string> IsPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder.SetValidator(new RegularExpressionValidator(@"((?:[0-9]\-?){6,14}[0-9]$)|((?:[0-9]\x20?){6,14}[0-9]$)"));
        //}

        public static IRuleBuilderOptions<T, string> IsAlphabetOnly<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"^[a-zA-Z ]+$");
        }

        //public static IRuleBuilderOptions<T, string> IsAlphaNumeric<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder.SetValidator(new RegularExpressionValidator(@"^[a-zA-Z0-9 ]+$"));
        //}

        //public static IRuleBuilderOptions<T, string> IsNumbersOnly<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder.SetValidator(new RegularExpressionValidator(@"^[0-9]*$"));
        //}

        //public static IRuleBuilderOptions<T, string> AcceptAlphabetPeriodEsclamationMarkAndQuestionMark<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder.SetValidator(new RegularExpressionValidator(@"^[a-zA-Z0-9,.!? ]*$"));
        //}

        //public static IRuleBuilderOptions<T, string> AcceptAlphabetComma<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder.SetValidator(new RegularExpressionValidator(@"^[a-zA-Z0-9, ]*$"));
        //}

        //public static IRuleBuilderOptions<T, string> IsUsernameAlphaNumeric<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder.SetValidator(new RegularExpressionValidator(@"^[a-zA-Z0-9]+$"));
        //}

        //public static IRuleBuilderOptions<T, string> AcceptAlphaNumericUnderscore<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder.SetValidator(new RegularExpressionValidator(@"^[a-zA-Z0-9_]+$"));
        //}

        public static IRuleBuilderOptions<T, string> AcceptAlphaNumericHyphen<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(@"^[a-zA-Z0-9- ]+$");
        }

        //public static IRuleBuilderOptions<T, string> AcceptAlphaNumericCommaPeriodHyphen<T>(this IRuleBuilder<T, string> ruleBuilder)
        //{
        //    return ruleBuilder.SetValidator(new RegularExpressionValidator(@"^[a-zA-Z0-9,.\- ]+$"));
        //}
    }
}
