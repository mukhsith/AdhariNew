using System.Collections.Generic;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class FooterModel
    {
        public FooterModel()
        {
            Categories = new List<CategoryModel>();
        }
        public IList<CategoryModel> Categories { get; set; }
    }
}
