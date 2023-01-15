 
namespace Utility.Models.Admin.Sales
{
    public class DailyOrderSummaryModel
    {
        public int? CustomerId { get; set; } = null;
        public decimal OrderReceivedToday { get; set; }
        public decimal SalesAmountToday { get; set; }
        public decimal ItemsSoldToday { get; set; }

        public string FormattedOrderReceivedToday { get; set; }
        public string FormattedSalesAmountToday { get; set; }
        public string FormattedItemsSoldToday { get; set; }
    }
}
