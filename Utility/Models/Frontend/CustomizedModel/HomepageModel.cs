using System.Collections.Generic;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class HomepageModel
    {
        public HomepageModel()
        {
            Categories = new List<CategoryModel>();
            Banners = new List<BannerModel>();
        }
        public IEnumerable<BannerModel> Banners { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
        public CompanySettingModel CompanySetting { get; set; }
    }
}

