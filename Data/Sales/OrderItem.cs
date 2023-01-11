using Data.Common;
using Data.ProductManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.Sales
{
    public partial class OrderItem : BaseEntityDate
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal UnitPrice { get; set; }
        public bool B2BPrice { get; set; }
        public DiscountType DiscountType { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DiscountValueApplied { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DiscountAmount { get; set; }
        public DiscountType CouponDiscountType { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CouponDiscountValueApplied { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CouponDiscountAmount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Total { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Weight { get; set; }
        [Column(TypeName = Constants.AmountDataType)]
        public decimal TotalWeight { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public ICollection<OrderItemDetail> OrderItemDetails { get; set; }
    }
}
