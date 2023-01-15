 
namespace Utility.Models.Admin.Sales
{
    public class DailySubscriptionSummaryModel
    {
        public int? CustomerId { get; set; } = null;
        public decimal SubscriptionOrdersReceivedToday { get; set; }
        public decimal SubscriptionSalesAmountToday { get; set; }
       // public decimal ItemsSoldToday { get; set; }

        public string FormattedSubscriptionOrdersReceivedToday { get; set; }
        public string FormattedSubscriptionSalesAmountToday { get; set; }
       // public string FormattedItemsSoldToday { get; set; }
    }
}
