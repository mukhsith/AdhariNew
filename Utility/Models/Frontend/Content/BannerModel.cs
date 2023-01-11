using Utility.Enum;

namespace Utility.Models.Frontend.Content
{
    public class BannerModel
    {
        public string ImageUrl { get; set; }
        public bool LinkEnabled { get; set; }
        public BannerLinkType LinkType { get; set; }
        public string LinkUrl { get; set; }
        public int? ProductId { get; set; }
        public int? ProductId2 { get; set; }
        public int? ProductId3 { get; set; }
    }
}
