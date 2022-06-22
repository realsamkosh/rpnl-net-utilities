using RPNL.Net.Utilities.IdentityVerficationUtils.Metamap.Models.NG.Common;

namespace RPNL.Net.Utilities.IdentityVerficationUtils.Metamap.Models.NG.NIN
{
    public class NINResponseModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string birthdate { get; set; }
        public string nationality { get; set; }
        public string nin { get; set; }
        public string profession { get; set; }
        public string stateOfOrigin { get; set; }
        public string lgaOfOrigin { get; set; }
        public string placeOfOrigin { get; set; }
        public string title { get; set; }
        public string height { get; set; }
        public string email { get; set; }
        public string birthState { get; set; }
        public string birthCountry { get; set; }
        public NextOfKin nextOfKin { get; set; }
        public string nspokenlang { get; set; }
        public string religion { get; set; }
        public Residence residence { get; set; }
        public GovernmentFaceMatch governmentFaceMatch { get; set; }
    }
}
