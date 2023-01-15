using System.Collections.Generic;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.Locations;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class PageHeaderModel
    {
        public PageHeaderModel()
        {
            Categories = new List<CategoryModel>();
            CompanySettings = new();
        }
        public IList<CategoryModel> Categories { get; set; }
        public CompanySettingModel CompanySettings { get; set; }
    }
}
