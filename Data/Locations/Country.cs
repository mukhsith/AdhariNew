using Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Helpers;

namespace Data.Locations
{
    public partial class Country : BaseEntityImage
    {
        [StringLength(Constants.TitleDataSize)]
        public string TitleEn { get; set; }

        [StringLength(Constants.TitleDataSize)]
        public string TitleAr { get; set; }               
        public bool Default { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string MobileCode { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string CurrencyCodeEn { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string CurrencyCodeAr { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string MyFatoorahCurrencyCode { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string CurrencyFormat { get; set; }

        [StringLength(Constants.ShortDataSize)]
        public string CountryCode { get; set; }
        public string DeliveryTextEn { get; set; }
        public string DeliveryTextAr { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DeliveryFee { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string FlagIcon { get; set; }
    }
}
