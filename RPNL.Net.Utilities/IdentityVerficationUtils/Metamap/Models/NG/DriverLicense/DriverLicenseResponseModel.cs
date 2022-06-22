using RPNL.Net.Utilities.IdentityVerficationUtils.Metamap.Models.NG.Common;

namespace RPNL.Net.Utilities.IdentityVerficationUtils.Metamap.Models.NG.DriverLicense
{
    public class DriverLicenseResponseModel
    {
        public string licenseNo { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string issuedDate { get; set; }
        public string expiryDate { get; set; }
        public string stateOfIssue { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public string middlename { get; set; }
        public GovernmentFaceMatch governmentFaceMatch { get; set; }
    }
}
