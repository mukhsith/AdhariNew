using System;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.Shop
{
    public class CartItemModel
    {
      
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string CustomerGuidValue { get; set; }
        public int? CustomerId { get; set; }
        public int ProductId { get; set; } 
        public int Quantity { get; set; }
        public DateTime? HoldUntil { get; set; } 
        public decimal Total { get; set; }
        public string FormattedTotal { get; set; }
        public decimal ShortTotal { get; set; }
        public string FormattedShortTotal { get; set; } 
        public int CartId { get; set; } 
        public ProductModel Product { get; set; }
    }
}
