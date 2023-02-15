 
namespace Utility.Models.Admin.Sales
{
    public class DailyOrderSummaryModel
    {
        public int? CustomerId { get; set; } = null;
        public decimal OrderReceivedToday { get; set; }

        public int OrderCompleted { get; set; }
        public decimal SalesAmountToday { get; set; }
        public decimal ItemsSoldToday { get; set; }
        public int FailedOrderReceived { get; set; }


        public int KNET { get; set; }
        public int VISA_Master { get; set; }
        public int Tabby { get; set; }
        public int COD { get; set; }
        public int Wallet { get; set; }
        public int QPay { get; set; }

        public string FormattedOrderReceivedToday { get; set; }
        public string FormattedSalesAmountToday { get; set; }
        public string FormattedItemsSoldToday { get; set; }
    }
}
