using System.Collections.Generic;
using Utility.Models.Frontend.CustomizedModel;

namespace Utility.Models.Frontend.Shop
{
    public class CartSummaryModel
    {
        public CartSummaryModel()
        {
            AmountSplitUps = new List<KeyValuPairModel>();
        }
        public decimal WalletUsedAmount { get; set; }
        public string FormattedWalletUsedAmount { get; set; }
        public decimal Total { get; set; }
        public string FormattedTotal { get; set; }
        public string CouponCode { get; set; }
        public decimal WalletBalanceAmount { get; set; }
        public string FormattedWalletBalanceAmount { get; set; }
        public bool SkipPaymentMethodSelection { get; set; }
        public string Notes { get; set; }
        public List<KeyValuPairModel> AmountSplitUps { get; set; }
    }
}
