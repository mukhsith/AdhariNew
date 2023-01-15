using Utility.Models.Frontend.Content;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class PageFooterModel
    {
        public PageFooterModel()
        {
            SocialMediaLink = new();
            PageFooterContentModel = new(); //BKD
        }
        public PageFooterContentModel PageFooterContentModel { get; set; }
        public SocialMediaLinkModel SocialMediaLink { get; set; }
    }
    public class PageFooterContentModel
    {
        public string TermsConditions { get; set; }
        public string PrivacyPolicy { get; set; }
        public string RefundPolicy { get; set; }
    }
}
