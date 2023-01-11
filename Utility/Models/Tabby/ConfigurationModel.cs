namespace Utility.Models.Tabby
{
    public class ConfigurationModel
    {
        public AvailableProductsModel available_products { get; set; }
        public string expires_at { get; set; }
        public ProductsModel products { get; set; }
    }
}
