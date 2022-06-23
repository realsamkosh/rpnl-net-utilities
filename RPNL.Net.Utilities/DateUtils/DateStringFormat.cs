using System;
using System.Collections.Generic;
using System.Text;

namespace RPNL.Net.Utilities.DateUtils
{
    public class DateStringFormat
    {
        public static List<string> ShortDateFormats()
        {
            List<string> formats = new List<string>
            { "dd/MM/yyyy","dd-MM-yyyy","yyyy-MM-dd","yyyy/MM/dd","MM/dd/yyyy","yyyy-MM-ddTHH:mm:ss.fffZ",
            "MM/dd/yyyy HH:mm:ss","dd/MM/yyyy HH:mm:ss"};
            return formats;
        }
    }
}
