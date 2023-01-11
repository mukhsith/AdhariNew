using Utility.Enum;

namespace Utility.Models.Frontend.Content
{
    public  class SiteContentModel
    {
        public string TermsAndConditions { get; set; }
        public string PrivacyPolicy { get; set; }
        public string RefundPolicy { get; set; }
        public string Content { get; set; }
        public AppContentType AppContentType { get; set; }
        public string ImageUrl { get; set; }
    }
}
