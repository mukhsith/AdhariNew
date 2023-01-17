using System.Collections.Generic;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class HomepageModel
    {
        public List<BannerModel> Banners { get; set; } = new();
        public List<CategoryModel> Categories { get; set; } = new();
        public CompanySettingModel CompanySetting { get; set; }
    }
}

