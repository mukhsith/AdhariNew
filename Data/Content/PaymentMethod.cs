using Data.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Helpers;

namespace Data.Content
{
    public class PaymentMethod : BaseEntityCommon
    {
        [StringLength(Constants.NameDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.NameDataSize)]
        public string NameAr { get; set; }

        [StringLength(Constants.ImageNameDataSize)]
        public string ImageName { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [StringLength(Constants.ImageNameDataSize)]
        public string ImageNameAr { get; set; }

        [NotMapped]
        public string ImageUrlAr { get; set; }

        [NotMapped]
        public IFormFile ImageAr { get; set; }

        [StringLength(Constants.ImageNameDataSize)]
        public string IconName { get; set; }

        [NotMapped]
        public string IconUrl { get; set; }

        [NotMapped]
        public IFormFile Icon { get; set; }
        public string ColorCode { get; set; }
        public bool NormalCheckoutRegisteredCustomer { get; set; }
        public bool NormalCheckoutGuestCustomer { get; set; }
        public bool SubscriptionCheckoutRegisteredCustomer { get; set; }
        public bool SubscriptionCheckoutGuestCustomer { get; set; }
        public bool ForWalletPackage { get; set; }
        public bool ForQuickPay { get; set; }
    }
}
