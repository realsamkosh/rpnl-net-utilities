﻿namespace RPNL.Net.Utilities.IdentityVerificationUtils.Metamap.Models.NIN
{
    public class NINRequestModel
    {
        public string documentNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string callbackUrl { get; set; }
    }
}
