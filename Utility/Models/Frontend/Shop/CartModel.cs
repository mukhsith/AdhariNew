using System.Collections.Generic;

namespace Utility.Models.Frontend.Shop
{
    public class CartModel
    {
        public CartModel()
        {
            CartItems = new List<CartItemModel>();
        }
        public decimal SubTotal { get; set; }
        public string FormattedSubTotal { get; set; }
        public IList<CartItemModel> CartItems { get; set; }
    }
}
