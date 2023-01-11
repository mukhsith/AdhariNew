using Data.Common;
using Data.CustomerManagement;
using Data.ProductManagement;
using System;

namespace Data.Shop
{
    public partial class SubscriptionHolding : BaseEntityDate
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? HoldUntil { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
