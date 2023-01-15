using Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Helpers;

namespace Data.CouponPromotion
{
    public class WalletPackage : BaseEntityCommon
    {
        [StringLength(Constants.MediumDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string NameAr { get; set; }

        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string DescriptionEn { get; set; }

        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string DescriptionAr { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Amount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal WalletAmount { get; set; }
    }
}
