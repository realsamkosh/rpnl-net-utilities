using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpnl.net.utilities.PhoneNumberUtils
{
    public class PhoneNumberValidator
    {
        private readonly PhoneNumberUtil _phoneUtil = PhoneNumberUtil.GetInstance();
        public string VerifyPhoneNumber(string phonecode, string phonenumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phonecode) || string.IsNullOrEmpty(phonenumber))
                {
                    return "Phone Number and Issuing Country Cannot be Empty";
                }
                else
                {
                    PhoneNumberCheckViewModel result = new();
                    // Parse the number to check into a PhoneNumber object.
                    PhoneNumber phoneNumber = _phoneUtil.Parse(phonenumber, null);
                    result.Valid = _phoneUtil.IsValidNumber(phoneNumber);
                    result.HasExtension = phoneNumber.HasExtension;
                    if (result.Valid)
                    {
                        return "1";
                    }
                    else
                    {
                        return "Phone Number Not Valid";
                    }
                }
            }
            catch (NumberParseException npex)
            {
                return string.Format("{0}-{1}", npex.ErrorType.ToString(), npex.Message);
            }
        }
    }
}
