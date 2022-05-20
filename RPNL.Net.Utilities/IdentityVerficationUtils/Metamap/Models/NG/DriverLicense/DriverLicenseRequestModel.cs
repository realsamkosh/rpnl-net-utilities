namespace RPNL.Net.Utilities.IdentityVerificationUtils.Metamap.Models.DriverLicense
{
    public class DriverLicenseRequestModel
    {
        public string documentNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string callbackUrl { get; set; }
    }
}
