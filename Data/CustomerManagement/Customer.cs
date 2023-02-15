using Data.Common;
using Data.Locations;
using System;
using System.ComponentModel.DataAnnotations;
using Utility.Enum;
using Utility.Helpers;

namespace Data.CustomerManagement
{
    public partial class Customer : BaseEntityCommon
    {
        [StringLength(Constants.NameDataSize)]
        public string Name { get; set; }

        [StringLength(Constants.MobileDataSize)]
        public string MobileNumber { get; set; }

        [StringLength(Constants.MobileDataSize)]
        public string NewMobileNumber { get; set; }

        [StringLength(Constants.EmailDataSize)]
        public string EmailAddress { get; set; }       
         
        [StringLength(Constants.SmallDataSize)]
        public string Password { get; set; }
        public int LanguageId { get; set; }
        public string Token { get; set; }
        public bool B2B { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string RequestPasswordChangeGuid { get; set; }
        public DateTime? NotificationFromDate { get; set; }
        public int CountryId { get; set; }
        public DeviceType DeviceTypeId { get; set; }
        public virtual Country Country { get; set; }
    }
}
