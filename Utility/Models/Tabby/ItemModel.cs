namespace Utility.Models.Tabby
{
    public class ItemModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public string unit_price { get; set; }
        public string discount_amount { get; set; }
        public string reference_id { get; set; }
        public string image_url { get; set; }
        public string product_url { get; set; }
        public string gender { get; set; }
        public string category { get; set; }
        public string color { get; set; }
        public string product_material { get; set; }
        public string size_type { get; set; }
        public string size { get; set; }
        public string brand { get; set; }
        public int ordered { get; set; }
        public int captured { get; set; }
        public int shipped { get; set; }
        public int refunded { get; set; }
    }
}
