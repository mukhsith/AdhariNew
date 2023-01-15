using System.Collections.Generic;
using Utility.Enum;
using Utility.Models.Frontend.CustomizedModel;

namespace Utility.Models.Frontend.ProductManagement
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int PiecesPerPacking { get; set; }
        public int ItemSizeId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } // as requested by Mr. Fakhruddin
        public decimal Price { get; set; }
        public string FormattedPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string FormattedDiscountedPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool Favorite { get; set; }
        public bool Notified { get; set; }
        public string AvailabilityText { get; set; }
        public string SeoName { get; set; }
        public string DetailsUrl { get; set; }
        public int CartQuantity { get; set; }
        public ProductType ProductType { get; set; }
        public ItemSizeModel ItemSize { get; set; }
        public CategoryModel Category { get; set; }
        public List<SubscriptionDurationModel> SubscriptionDurations { get; set; } = new();
        public List<SubscriptionDeliveryDateModel> SubscriptionDeliveryDates { get; set; } = new();
        public List<KeyValuPairModel> SubscriptionPackTitles { get; set; } = new();
    }
}
