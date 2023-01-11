using System.Collections.Generic;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Admin.ProductManagement
{
    public class ProductAndCategoryModel
    {
        public List<ProductSmallModel> Products { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
