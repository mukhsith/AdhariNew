using Data.Common;
using Data.CustomerManagement;

namespace Data.ProductManagement
{
    public partial class ProductAvailabilityNotifyRequest : BaseEntityDate
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public bool Send { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
