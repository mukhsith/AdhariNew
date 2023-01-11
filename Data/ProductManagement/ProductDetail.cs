using Data.Common;

namespace Data.ProductManagement
{
    public partial class ProductDetail : BaseEntityDate
    {
        public int ProductId { get; set; }
        public int ChildProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}
