using System.Collections.Generic;
using Utility.Enum;
using Utility.Models.Frontend.Content;

namespace Utility.Models.Frontend.ProductManagement
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HoverImageUrl { get; set; }
        public string ImageUrl { get; set; }
        public string SelectedImageUrl { get; set; }
        public string ImageDesktopUrl { get; set; }
        public string ImageMobileUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string SeoName { get; set; }
        public ProductType ProductTypeId { get; set; }
        public List<ProductModel> Products { get; set; } = new();

        /// <summary>
        /// do not delete its used in admin
        /// </summary>
        public string NameEn { get; set; }
        /// <summary>
        /// do not delete its used in admin
        /// </summary>
        public string NameAr { get; set; }
    }
}
