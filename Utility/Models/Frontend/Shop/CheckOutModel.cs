using System.Collections.Generic;
using Utility.Models.Frontend.CustomerManagement;

namespace Utility.Models.Frontend.Shop
{
    public class CheckOutModel
    {
        public CheckOutModel()
        {
            CartSummary = new CartSummaryModel();
            PaymentMethod = new Content.PaymentMethodModel();
            CartItems = new List<CartItemModel>();
        }
        public string EstimatedDelivery { get; set; }
        public string EstimatedDeliveryValue { get; set; }
        public string AddressText { get; set; }
        public CustomerModel Customer { get; set; }
        public CartSummaryModel CartSummary { get; set; }
        public Content.PaymentMethodModel PaymentMethod { get; set; }
        public IList<CartItemModel> CartItems { get; set; }
        public List<Content.PaymentMethodModel> PaymentMethods { get; set; } = new();
    }
}
