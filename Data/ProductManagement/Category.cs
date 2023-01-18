using Data.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Helpers;

namespace Data.ProductManagement
{
    public class Category : BaseEntityImage
    {
 
        [StringLength(Constants.MediumDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string NameAr { get; set; }
        /// <summary>
        /// will be save internally
        /// </summary>
        [StringLength(Constants.LargeDataSize)]
        public string SeoName { get; set; }

        /// <summary>
        /// normal icon for mobile application
        /// </summary>
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageNormalIconName { get; set; }

        [NotMapped]
        public string ImageNormalIconUrl { get; set; }

        [NotMapped]
        public IFormFile ImageNormalIcon { get; set; }

        /// <summary>
        /// selected icon for mobile application
        /// </summary>
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageSelectedIconName { get; set; }

        [NotMapped]
        public string ImageSelectedIconUrl { get; set; }

        [NotMapped]
        public IFormFile ImageSelectedIcon { get; set; }

        /// <summary>
        /// Desktop
        /// </summary>
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageDesktopName { get; set; }

        [NotMapped]
        public string ImageDesktopUrl { get; set; }

        [NotMapped]
        public IFormFile ImageDesktop { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageMobileName { get; set; }

        [NotMapped]
        public string ImageMobileUrl { get; set; }

        [NotMapped]
        public IFormFile ImageMobile { get; set; }

        /// <summary>
        /// normal icon for mobile application
        /// </summary>
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageNormalIconNameAr { get; set; }

        [NotMapped]
        public string ImageNormalIconUrlAr { get; set; }

        [NotMapped]
        public IFormFile ImageNormalIconAr { get; set; }

        /// <summary>
        /// selected icon for mobile application
        /// </summary>
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageSelectedIconNameAr { get; set; }

        [NotMapped]
        public string ImageSelectedIconUrlAr { get; set; }

        [NotMapped]
        public IFormFile ImageSelectedIconAr { get; set; }

        /// <summary>
        /// Desktop
        /// </summary>
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageDesktopNameAr { get; set; }

        [NotMapped]
        public string ImageDesktopUrlAr { get; set; }

        [NotMapped]
        public IFormFile ImageDesktopAr { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageMobileNameAr { get; set; }

        [NotMapped]
        public string ImageMobileUrlAr { get; set; }

        [NotMapped]
        public IFormFile ImageMobileAr { get; set; }

        /// <summary>
        ///  default is 1=base product,2-bundled product,3-subscription
        /// </summary>
        public ProductType ProductTypeId { get; set; } = ProductType.BaseProduct;
    }
}
