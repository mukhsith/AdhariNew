using Utility.Enum;

namespace Utility.Models.Frontend.Content
{
    public class BannerModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool LinkEnabled { get; set; }
        public BannerLinkType LinkType { get; set; }
        public string LinkUrl { get; set; }
        public int? ProductId { get; set; }
        public int DisplayOrder { get; set; }
        public string ProductSeoName { get; set; }
        public string CategorySeoName { get; set; }
        public bool DeviceDependent { get; set; }
    }
}
