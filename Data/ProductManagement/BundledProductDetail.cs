using Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.ProductManagement
{
    
    public partial class BundledProductDetail : BaseEntityId
    { 
        [ForeignKey("BundledProductId")]
        public int BundledProductId { get; set; }  
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; } 
        public decimal Price()
        {
            if (Quantity > 0)
            {
                return Product.Price * Quantity;
            } else
            {
                return Product.Price;
            }
        }
        
      
    }
}
