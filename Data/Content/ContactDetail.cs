using Data.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Helpers;

namespace Data.Content
{
    public class ContactDetail : BaseEntityDate
    {
        [StringLength(Constants.ExtraLargeDataSize)]
        public string AddressEn { get; set; }

        [StringLength(Constants.ExtraLargeDataSize)]
        public string AddressAr { get; set; }

        [StringLength(Constants.MobileDataSize)]
        public string MobileNumber { get; set; }

        [StringLength(Constants.MobileDataSize)]
        public string WhatsAppNumber { get; set; }

        [StringLength(Constants.EmailDataSize)]
        public string EmailAddress { get; set; }
    }
}
