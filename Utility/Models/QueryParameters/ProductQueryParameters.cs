using Utility.Enum;

namespace Utility.Models.QueryParameters
{
    public class ProductQueryParameters
    {
        public string Keyword { get; set; } = "";
        public int Id { get; set; } = 0;
        public int CategoryId { get; set; } = 0;
        public int Limit { get; set; } = int.MaxValue;
        public int Page { get; set; } = 1;
        public int CustomerId { get; set; } = 0;
        public SortOptions? SortOption { get; set; } = null;
        public bool Favorite { get; set; } = false;
        public string CategorySeoName { get; set; } = "";
        public string SeoName { get; set; } = "";
        public string CustomerGuidValue { get; set; } = "";
        public ProductType? ProductType { get; set; } = null;
    }
}
