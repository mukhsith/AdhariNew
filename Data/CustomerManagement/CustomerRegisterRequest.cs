using Data.Common;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.CustomerManagement
{
    public partial class CustomerRegisterRequest: BaseEntityDate
    {
        [StringLength(Constants.NameDataSize)]
        public string Name { get; set; }

        [StringLength(Constants.EmailDataSize)]
        public string EmailAddress { get; set; }
        public int CountryId { get; set; }

        [StringLength(Constants.MobileDataSize)]
        public string MobileNumber { get; set; }
        
        [StringLength(Constants.SmallDataSize)]
        public string Password { get; set; } 
    }
}
