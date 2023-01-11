using System.Collections.Generic;

namespace Utility.Models.Frontend.CustomerManagement
{
    public class WalletModel
    {
        public decimal WalletBalance { get; set; }
        public string FormattedWalletBalance { get; set; }
        public decimal CashbackBalance { get; set; }
        public string FormattedCashbackBalance { get; set; }
        public List<WalletTransactionByDateModel> WalletTransactionByDates { get; set; } = new();
    }
}
