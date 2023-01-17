using System.Collections.Generic;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class PageHeaderModel
    {
        public PageHeaderModel()
        {
            CompanySettings = new();
        }
        public List<CategoryModel> Categories { get; set; } = new();
        public CompanySettingModel CompanySettings { get; set; }
    }
}
