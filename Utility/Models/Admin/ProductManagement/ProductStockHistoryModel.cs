using System;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Utility.Models.Admin.ProductManagement
{
    public partial class ProductStockHistoryModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductType { get; set; } //1-base product,2-bundled product,3-subscription
        public string ProductUpdateType { get; set; } //1-automatic deduction, 2-
        public string ProductActionType { get; set; } //1-add to stock,2-remove from stock,3-set stock to 
        public int OldStock { get; set; }
        public int InputStock { get; set; }
        public int UpdatedStock { get; set; }         
        public decimal Price { get; set; }
        //public int? CountryId { get; set; }
        //public virtual Country Country { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Note { get; set; }
        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string OrderLink {get;set;}
        public bool Deleted { get; set; }
    }
}
 
