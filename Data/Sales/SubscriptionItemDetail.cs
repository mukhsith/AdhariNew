using Data.Common;

namespace Data.Sales
{
    public partial class SubscriptionItemDetail : BaseEntityDate
    {
        public int SubscriptionId { get; set; }
        public int ProductId { get; set; }
        public int ChildProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
