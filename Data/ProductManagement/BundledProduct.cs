using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Helpers;

namespace Data.ProductManagement
{

    public class BundledProduct : BaseEntityImage
    {

        [StringLength(Constants.MediumDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string NameAr { get; set; }

        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string DescriptionEn { get; set; }

        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string DescriptionAr { get; set; }

        [StringLength(Constants.ExtraMediumDataSize)] 
        public string SeoName { get; set; }
        public int CategoryId { get; set; }

        public ProductType ProductType { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Price { get; set; }
        [Column(TypeName = Constants.AmountDataType)]
        public decimal DiscountedPrice { get; set; }
        public DateTime? DiscountFromDate { get; set; }
        public DateTime? DiscountToDate { get; set; }

        /// <summary>
        /// Pricing B2B
        /// </summary>
        public bool B2BPriceEnabled { get; set; }
        [Column(TypeName = Constants.AmountDataType)]
        public decimal B2BPrice { get; set; }
        [Column(TypeName = Constants.AmountDataType)]
        public decimal B2BDiscountedPrice { get; set; }
        public DateTime? B2BDiscountFromDate { get; set; }
        public DateTime? B2BDiscountToDate { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<BundledProductDetail> BundledProductDetails { get; set; }

        public virtual decimal GetB2CPrice()
        {
            if (DiscountFromDate is not null && DiscountToDate is not null)
            {
                if (DiscountFromDate >= DateTime.Now && DiscountToDate <= DateTime.Now)
                {
                    if (DiscountedPrice > 0)
                    {
                        return DiscountedPrice;
                    }
                    else
                    {
                        return Price;
                    }
                }
            }
            
            return Price;
             
        }

        public virtual decimal GetB2BCPrice()
        {
            if (B2BDiscountFromDate is not null && B2BDiscountToDate is not null)
            {
                if (B2BDiscountFromDate >= DateTime.Now && B2BDiscountToDate <= DateTime.Now)
                {
                    if (B2BDiscountedPrice > 0)
                    {
                        return B2BDiscountedPrice;
                    }
                    else
                    {
                        return B2BPrice;
                    }
                }
            }

            return B2BPrice;

        }
    }
}
