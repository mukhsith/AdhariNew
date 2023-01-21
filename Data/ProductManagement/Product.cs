using Data.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.ProductManagement
{

    public partial class Product : BaseEntityImage
    {

        [StringLength(Constants.MediumDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string NameAr { get; set; }

        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string DescriptionEn { get; set; }

        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string DescriptionAr { get; set; }
        public string SeoName { get; set; }
        public int PiecesPerPacking { get; set; }
        public int Stock { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Price { get; set; }
        [Column(TypeName = Constants.AmountDataType)]
        public decimal DiscountedPrice { get; set; }
        public DateTime? DiscountFromDate { get; set; }
        public DateTime? DiscountToDate { get; set; }
        public bool B2BPriceEnabled { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal B2BPrice { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal B2BDiscountedPrice { get; set; }
        public DateTime? B2BDiscountFromDate { get; set; }
        public DateTime? B2BDiscountToDate { get; set; }
        public ProductType ProductType { get; set; }
        public bool SpecialPackage { get; set; }
        public int? SubscriptionDurationId { get; set; }
        public string SubscriptionDurationIds { get; set; }
        public int MinCartQuantity { get; set; }
        public int MaxCartQuantity { get; set; }
        public int B2BMinCartQuantity { get; set; }
        public int B2BMaxCartQuantity { get; set; }
        public ICollection<ProductDetail> ProductDetails { get; set; }
        public int? ItemSizeId { get; set; }

        [ForeignKey("ItemSizeId")]
        public virtual ItemSize ItemSize { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual SubscriptionDuration SubscriptionDuration { get; set; }

        /// <summary>
        /// Web+Mobile Api will receive  Price, DiscountPrice, FormattedPrice, FormattedDiscountPrice
        /// B2B or B2B discount will apply in this method
        /// DiscountedPrice property value will be changed based on Discount formula 
        /// </summary>
        /// <param name="b2bCustomer"></param>
        public void ApplyDiscount(bool b2bCustomer)
        {

            if (b2bCustomer) //B2B Customer only for api response
            {
                if (B2BPriceEnabled)
                {
                    if (B2BDiscountFromDate.HasValue && B2BDiscountToDate.HasValue)
                    {
                        if ((DateTime.Now.Date >= B2BDiscountFromDate.Value.Date) && (DateTime.Now.Date <= B2BDiscountToDate.Value.Date))
                        {
                            if (B2BDiscountedPrice > 0 && B2BDiscountedPrice < B2BPrice)
                            {
                                this.DiscountedPrice = this.B2BDiscountedPrice;
                            }
                            else
                            {
                                this.DiscountedPrice = 0;
                            }
                        }
                        else
                        {
                            //if discount duration is defined and its range not in today, so no discount will apply
                            //so discount price will be zero
                            this.DiscountedPrice = 0;
                        }

                    }
                    else
                    {
                        if (B2BDiscountedPrice > 0 && B2BDiscountedPrice < B2BPrice)
                        {
                            this.DiscountedPrice = this.B2BDiscountedPrice;
                        }
                        else
                        {
                            this.DiscountedPrice = 0;
                        }

                    }
                }
            }
            else
            {
                if (DiscountFromDate.HasValue && DiscountToDate.HasValue)
                {
                    if ((DateTime.Now.Date >= DiscountFromDate.Value.Date) && (DateTime.Now.Date <= DiscountToDate.Value.Date))
                    {
                        //discount price is already assigned to DiscountedPrice column
                        if (DiscountedPrice > 0 && DiscountedPrice < Price)
                            this.DiscountedPrice = DiscountedPrice;
                        else
                            this.DiscountedPrice = 0;
                    }
                }
                else
                {
                    if (DiscountedPrice > 0 && DiscountedPrice < Price)
                        this.DiscountedPrice = DiscountedPrice;
                    else
                        this.DiscountedPrice = 0;
                }
            }
        }

        /// <summary>
        /// 1- if customer is b2C return price based on the condition
        /// 2- default price will be Price, if customer is B2B and all condition failed
        /// </summary>
        /// <param name="b2bCustomer"></param>
        /// <returns></returns>
        public decimal GetPrice(bool b2bCustomer)
        {
            //            decimal price = 0;
            if (b2bCustomer) //B2B Customer only for api response
            {
                return GetB2BPrice();

            }
            else
            {
                return GetB2CPrice();
            }
        }

        private decimal GetB2CPrice()
        {
            decimal price = this.Price;
            if (DiscountFromDate.HasValue && DiscountToDate.HasValue)
            {
                if ((DateTime.Now.Date >= DiscountFromDate.Value.Date) && (DateTime.Now.Date <= DiscountToDate.Value.Date))
                {
                    //discount price is already assigned to DiscountedPrice column
                    if (DiscountedPrice > 0 && DiscountedPrice < Price)
                        price = DiscountedPrice;
                }
            }
            else
            {
                if (DiscountedPrice > 0 && DiscountedPrice < Price)
                    price = DiscountedPrice;
            }
            return price;
        }

        /// <summary>
        /// if B2B price is not enabled, return normal price, check b2bPrice
        /// </summary>
        /// <returns></returns>
        private decimal GetB2BPrice()
        {
            decimal price = this.Price; //  
            if (B2BPriceEnabled)
            {
                if (B2BDiscountFromDate.HasValue && B2BDiscountToDate.HasValue)
                {
                    if ((DateTime.Now.Date >= B2BDiscountFromDate.Value.Date) && (DateTime.Now.Date <= B2BDiscountToDate.Value.Date))
                    {
                        if (B2BDiscountedPrice > 0 && B2BDiscountedPrice < B2BPrice)
                            price = B2BDiscountedPrice;
                        else
                            price = B2BPrice;
                    }
                    else
                    {
                        //if discount duration is defined and its range not in today, so no discount will apply
                        //so discount price will be zero
                        price = B2BPrice;
                    }

                }
                else
                {
                    if (B2BDiscountedPrice > 0 && B2BDiscountedPrice < B2BPrice)
                        price = B2BDiscountedPrice;
                    else
                        price = B2BPrice;

                }
            }
            return price;
        }
        public decimal GetPriceFrontend(bool b2bCustomer)
        {
            decimal price = Price;

            if (b2bCustomer && B2BPriceEnabled)
            {
                price = B2BPrice > 0 ? B2BPrice : Price;
            }

            return price;
        }
        public decimal GetDiscountedPriceFrontend(bool b2bCustomer)
        {
            decimal discountedPrice = 0;

            if (b2bCustomer && B2BPriceEnabled)
            {
                bool b2bDiscountPriceEnabled = true;

                if (B2BDiscountFromDate.HasValue)
                {
                    if (DateTime.Now.Date < B2BDiscountFromDate.Value.Date)
                    {
                        b2bDiscountPriceEnabled = false;
                    }
                }

                if (B2BDiscountToDate.HasValue)
                {
                    if (DateTime.Now.Date > B2BDiscountToDate.Value.Date)
                    {
                        b2bDiscountPriceEnabled = false;
                    }
                }

                if (B2BDiscountedPrice <= 0)
                {
                    b2bDiscountPriceEnabled = false;
                }

                if (b2bDiscountPriceEnabled)
                {
                    discountedPrice = B2BDiscountedPrice;
                }
            }
            else
            {
                bool discountPriceEnabled = true;

                if (DiscountFromDate.HasValue)
                {
                    if (DateTime.Now.Date < DiscountFromDate.Value.Date)
                    {
                        discountPriceEnabled = false;
                    }
                }

                if (DiscountToDate.HasValue)
                {
                    if (DateTime.Now.Date > DiscountToDate.Value.Date)
                    {
                        discountPriceEnabled = false;
                    }
                }

                if (DiscountedPrice <= 0)
                {
                    discountPriceEnabled = false;
                }

                if (discountPriceEnabled)
                {
                    discountedPrice = DiscountedPrice;
                }
            }

            return discountedPrice;
        }
    }
}

