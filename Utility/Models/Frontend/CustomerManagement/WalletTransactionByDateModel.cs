using System.Collections.Generic;

namespace Utility.Models.Frontend.CustomerManagement
{
    public class WalletTransactionByDateModel
    {
        public WalletTransactionByDateModel()
        {
            WalletTransactions = new List<WalletTransactionModel>();
        }
        public string FormattedDate { get; set; }
        public List<WalletTransactionModel> WalletTransactions { get; set; }
    }
}
