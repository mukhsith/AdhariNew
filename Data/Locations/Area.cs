using Data.Common;
using Data.Locations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Helpers;

namespace Data.Locations
{
    public class Area : BaseEntityCommon
    {
        [StringLength(Constants.MediumDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string NameAr { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DeliveryFee { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal MinOrderAmount { get; set; }
        public int GovernorateId { get; set; }
        public virtual Governorate Governorate { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

    }
}
