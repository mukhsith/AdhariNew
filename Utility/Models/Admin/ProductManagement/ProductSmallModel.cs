using Utility.Enum;

namespace Utility.Models.Admin.ProductManagement
{
    public class ProductSmallModel
    { 
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public string NameEn  { get; set; }
        public string NameAr { get; set; }
        //public int PiecesPerPacking { get; set; }
        public string ItemSizeNameEn { get; set; }
        public string ItemSizeNameAr { get; set; }
        public decimal Price { get; set; }
        public int MaxQty { get; set; }
        public string ImageUrl { get; set; }
        public int DisplayOrder { get; set; } 
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
