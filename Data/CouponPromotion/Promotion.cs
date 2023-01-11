using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Data.CouponPromotion
{
    public class Promotion : BaseEntityDate
    {
        public bool SignupEnabled { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal SignupCashbackValue { get; set; }
        public int SignupCashbackValueExpiryInNoOfDays { get; set; }
        public DateTime? SignupFromDate { get; set; }
        public DateTime? SignupToDate { get; set; }
        public bool CashbackOnPurchaseEnabled { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CashbackOnPurchaseMinOrderAmount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CashbackOnPurchaseValue { get; set; }
        public DateTime? CashbackOnPurchaseFromDate { get; set; }
        public DateTime? CashbackOnPurchaseToDate { get; set; }
        public int CashbackOnPurchaseExpiryInNoOfDays { get; set; }
        public bool CashbackRedeemEnabled { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CashbackRedeemMinWalletAmount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CashbackRedeemMinOrderAmount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CashbackValueToDeduct { get; set; }
        public bool PromotionEnabled(decimal amount)
        {
            bool cashbackOnPurchaseEnabled = CashbackOnPurchaseEnabled;

            if (cashbackOnPurchaseEnabled)
            {
                if (CashbackOnPurchaseFromDate.HasValue)
                {
                    if (DateTime.Now.Date < CashbackOnPurchaseFromDate.Value.Date)
                    {
                        cashbackOnPurchaseEnabled = false;
                    }
                }

                if (CashbackOnPurchaseToDate.HasValue)
                {
                    if (DateTime.Now.Date > CashbackOnPurchaseToDate.Value.Date)
                    {
                        cashbackOnPurchaseEnabled = false;
                    }
                }

                if (amount < CashbackOnPurchaseMinOrderAmount)
                {
                    cashbackOnPurchaseEnabled = false;
                }
            }

            return cashbackOnPurchaseEnabled;
        }
    }
}
