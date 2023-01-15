using Data.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.CustomerManagement
{
    public class WalletTransaction : BaseEntityDate
    {
        public int CustomerId { get; set; }
        public string TransactionNo { get; set; }
        public WalletType WalletTypeId { get; set; }
        public RelatedEntityType RelatedEntityTypeId { get; set; }
        public int RelatedEntityId { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Credit { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal RemainingCredit { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Debit { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Balance { get; set; }
        public WalletTransactionType WalletTransactionTypeId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
