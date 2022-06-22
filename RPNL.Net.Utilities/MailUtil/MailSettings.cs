using System;
using System.Collections.Generic;
using System.Text;

namespace RPNL.Net.Utilities.MailUtil
{
    public class MailSettings
    {
        /// <summary>
        /// Email Port Number
        /// </summary>
        public string Provider { get; set; }
        public string SenderEmail { get; set; }
        public SMTP SMTP { get; set; }
        public string BCC { get; set; }
        public string CC { get; set; }
    }
}
