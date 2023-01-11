using System;
using Utility.Models.Base;

namespace Utility.Models.Frontend.CouponPromotion
{
    public class PromotionModel : BaseModel
    {
        public bool SignupEnabled { get; set; }
        public float SignupCashbackValue { get; set; }
        public DateTime? SignupFromDate { get; set; }
        public DateTime? SignupToDate { get; set; }
        public bool CashbackOnPurchaseEnabled { get; set; }
        public float CashbackOnPurchaseMinOrderAmount { get; set; }
        public float CashbackOnPurchaseValue { get; set; }
        public DateTime? CashbackOnPurchaseFromDate { get; set; }
        public DateTime? CashbackOnPurchaseToDate { get; set; }
        public bool CashbackRedeemEnabled { get; set; }
        public float CashbackRedeemMinWalletAmount { get; set; }
        public float CashbackRedeemMinOrderAmount { get; set; }
        public float CashbackValueToDeduct { get; set; }
    }
}
