using System;

namespace RPNL.Net.Utilities.IdentityVerificationUtils.Metamap.Models
{
    public class BaseResponseModel<TData>
    {
        public int status { get; set; }
        public string id { get; set; }
        public Error error { get; set; }
        public TData data { get; set; }
        public DateTime timestamp { get; set; }
    }
    public class Error
    {
        public string type { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
}
