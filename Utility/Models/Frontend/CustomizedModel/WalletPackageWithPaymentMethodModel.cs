using System.Collections.Generic;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CouponPromotion;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class WalletPackageWithPaymentMethodModel
    {
        public List<WalletPackageModel> WalletPackages { get; set; }
        public List<PaymentMethodModel> PaymentMethods { get; set; }
    }
}
