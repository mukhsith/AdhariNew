using Data.Common;
using Data.CustomerManagement;
using Data.ProductManagement;
using System;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.Shop
{
    public partial class CartItem : BaseEntityDate
    {
        [StringLength(Constants.SmallDataSize)]
        public string CustomerGuidValue { get; set; }
        public int? CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? HoldUntil { get; set; } 
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
