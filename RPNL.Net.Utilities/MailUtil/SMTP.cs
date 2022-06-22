using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNL.Net.Utilities.MailUtil
{
    public class SMTP
    {
        public bool EnableSsl { get; set; }
        public int DeliveryMethod { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string SenderUserName { get; set; }
        public string SenderName { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPHost { get; set; }
        public string Password { get; set; }
    }
}
