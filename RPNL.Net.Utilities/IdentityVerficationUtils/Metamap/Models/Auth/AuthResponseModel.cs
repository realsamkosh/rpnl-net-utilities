using System;
using System.Collections.Generic;
using System.Text;

namespace RPNL.Net.Utilities.IdentityVerficationUtils.Metamap.Models.Auth
{
    public class AuthResponseModel
    {
        public string access_token { get; set; }
        public int expiresIn { get; set; }
        public PayloadModel payload { get; set; }
    }
}
