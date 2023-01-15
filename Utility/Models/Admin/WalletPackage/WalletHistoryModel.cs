using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models.Admin.WalletPackage
{
    public class WalletHistoryModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TransactionNo { get; set; }

        public WalletTransactionType WalletTransactionTypeId { get; set; }
        public WalletType WalletTypeId { get; set; }
        public string Description { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Balance { get; set; }
        public bool Deleted { get; set; }
        public string FormattedCredit { get; set; }
        public string FormattedDebit { get; set; }
        public string FormattedBalance { get; set; }
    }
}
