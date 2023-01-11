using System.Collections.Generic;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Admin.ProductManagement
{
    public class CategoryAndItemSizeModel
    {
        public List<ItemSizeModel> ItemSize { get; set; }
        public List<CategoryModel> Category { get; set; }
    }
}
