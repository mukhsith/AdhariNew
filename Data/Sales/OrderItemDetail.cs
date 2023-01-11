using Data.Common;

namespace Data.Sales
{
    public partial class OrderItemDetail : BaseEntityDate
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int ChildProductId { get; set; }
        public int Quantity { get; set; }
    }
}
