 
namespace Utility.Models.Admin.Sales
{
    public class DriverDeliverySummaryModel
    {
        public int Normal { get; set; }
        public int Subscription { get; set; }

        public int Total { get; set; }

        public int Pending { get; set; }
        public int Completed { get; set; }
    }
}
