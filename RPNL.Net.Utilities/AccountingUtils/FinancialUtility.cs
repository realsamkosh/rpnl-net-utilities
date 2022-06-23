using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.AccountingUtils
{
    public class FinancialUtility
    {
        public static IEnumerable<CultureInfo> GetCultureInfosByCurrencySymbol(string currencySymbol)
        {
            if (currencySymbol == null)
            {
                throw new ArgumentNullException(nameof(currencySymbol));
            }

            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Where(x => new RegionInfo(x.LCID).ISOCurrencySymbol == currencySymbol);
        }

        public static string CultureInfoProcessor(string currencySymbol)
        {
            string result = currencySymbol switch
            {
                "NGN" => "en_NG",
                "EUR" => "en_GB",
                "USD" => "en_US",
                "CNY" => "zh-CN",
                "EGP" => "ar-EG",
                "JOD" => "ar-JO",
                "ALL" => "sq-AL",

                _ => "en_NG",
            };
            return result;
        }

        public static (string, string) MajorCurrencyCoinCurrency(string currencySymbol)
        {
            var major = string.Empty;
            var coin = string.Empty;
            switch (currencySymbol)
            {
                case "NGN":
                    major = "Naira";
                    coin = "Kobo";
                    break;
                case "EUR":
                    major = "Euros";
                    coin = "Cent";
                    break;
                case "USD":
                    major = "Dollars";
                    coin = "Cent";
                    break;
                case "CNY":
                    major = "Yuan";
                    coin = "Fen";
                    break;
                case "EGP":
                    major = "Egyptian Pound";
                    coin = "Piastre";
                    break;
                case "JOD":
                    major = "Egyptian Pound";
                    coin = "Piastre";
                    break;
                case "ALL":
                    major = "Egyptian Pound";
                    coin = "Piastre";
                    break;
                default:
                    major = "Naira";
                    coin = "Kobo";
                    break;
            }
            return (major, coin);
        }
    }
}
