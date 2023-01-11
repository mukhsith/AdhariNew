using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.Sales
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string FormattedUnitPrice { get; set; }
        public decimal Total { get; set; }
        public string FormattedTotal { get; set; }
        public ProductModel Product { get; set; }
    }
}
