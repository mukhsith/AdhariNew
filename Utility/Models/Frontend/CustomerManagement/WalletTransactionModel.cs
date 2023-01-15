using System.Collections.Generic;
using Utility.Models.Frontend.CustomizedModel;

namespace Utility.Models.Frontend.CustomerManagement
{
    public class WalletTransactionModel
    {
        public string Title { get; set; }
        public string FormattedAmount { get; set; }
        public string WalletType { get; set; }
        public string Description { get; set; }       
        public string ImageUrl { get; set; }
        public string ColorCode { get; set; }
        public bool CreditTransaction { get; set; }
        public List<KeyValuPairModel> PaymentSummary { get; set; } = new();
    }
}
