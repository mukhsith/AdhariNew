using Data.Common;
using Data.Locations;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    public partial class ProductStockHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductType ProductType { get; set; } //1-base product,2-bundled product,3-subscription
        public ProductUpdateType ProductUpdateType { get; set; } //1-product created,2-automatic deduction, 3-Manual Adjustment
        public ProductActionType ProductActionType { get; set; } //1-add to stock,2-remove from stock,3-set stock to 
        public int OldStock { get; set; }
        public int InputStock { get; set; }
        public int UpdatedStock { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Price { get; set; }
        //public int? CountryId { get; set; }
        //public virtual Country Country { get; set; }
        public int? CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual SystemUser SystemUser { get; set; }
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// Manual Adjustment->Add to Stock--> Received Shipment from Bahrain. Check Invoice Number 10002345. </td>
        /// Automatic Deduction-->Remove from Stock--><td>Order Received <a class="text-primary" href="#">Linked to Order #25684</a></td>
        /// Product Creation-->Set Stock To-->Product Created
        /// Manual Adjustment->Set Stock To--> Result of Recount. Found only 30 Cartons in Warehouse</td>
        /// </summary>
        public string Note { get; set; }
        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string OrderLink { get; set; }
        public bool Deleted { get; set; }
        public RelatedEntityType RelatedEntityTypeId { get; set; }
        public int RelatedEntityId { get; set; }
        public virtual string GetProductType()
        {
            if (this.ProductType == ProductType.BaseProduct)
            {
                return "Base Product";
            }
            else if (this.ProductType == ProductType.BundledProduct)
            {
                return "Bundled Product";
            }
            else if (this.ProductType == ProductType.SubscriptionProduct)
            {
                return "Subscription Product";
            }
            return "None";
        }

        public virtual string GetProductUpdateType()
        {
            if (this.ProductUpdateType == ProductUpdateType.AutomaticDeduction)
            {
                return "Automatic Deduction";
            }
            else if (this.ProductUpdateType == ProductUpdateType.ManualAdjustment)
            {
                return "Manual Adjustment";
            }
            else if (this.ProductUpdateType == ProductUpdateType.ProductCreation)
            {
                return "Product Created";
            }
            return "None";
        }

        public virtual string GetProductActionType()
        {
            if (this.ProductActionType == ProductActionType.AddToStock)
            {
                return "Add to Stock";
            }
            else if (this.ProductActionType == ProductActionType.RemoveFromStock)
            {
                return "Remove from Stock";
            }
            else if (this.ProductActionType == ProductActionType.SetStockTo)
            {
                return "Set Stock To";
            }
            //else if (this.ProductAction == ProductAction.OrderReceived)
            //{
            //    return "Order Received" + OrderLink;
            //}
            //else if (this.ProductAction == ProductAction.ReceivedShipment)
            //{
            //    return "Received Shipment from " +  Country.TitleEn;
            //}
            return "None";
        }

    }
}

