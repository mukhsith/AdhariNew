using Data.Common;
using Data.CustomerManagement;
using System;
using System.ComponentModel.DataAnnotations;
using Utility.Enum;
using Utility.Helpers;

namespace Data.SMS
{
    public partial class OTPDetail : BaseEntityDate
    {
        [StringLength(Constants.ExtraLargeDataSize)]
        public string Destination { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string OTP { get; set; }
        public DateTime OTPValidFrom { get; set; }
        public DateTime OTPValidTo { get; set; }
        public NotificationType Type { get; set; }
        public int? CustomerId { get; set; }
        public int? CustomerRegisterRequestId { get; set; }
        public int NumberOfTries { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerRegisterRequest CustomerRegisterRequest { get; set; }
    }
}
