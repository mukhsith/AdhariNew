using Data.Common;
using Data.ProductManagement;
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

namespace Data.Content
{
    public class Banner : BaseEntityCommon
    {
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageNameEn { get; set; }

        [NotMapped]
        public string ImageUrlEn { get; set; }

        [NotMapped]
        public IFormFile ImageEn { get; set; }

        [StringLength(Constants.ImageNameDataSize)]
        public string ImageNameAr { get; set; }

        [NotMapped]
        public string ImageUrlAr { get; set; }

        [NotMapped]
        public IFormFile ImageAr { get; set; }

        public bool LinkEnabled { get; set; }

        public BannerLinkType LinkType { get; set; }
        public string LinkUrl { get; set; }
        public bool ExcludeFromApp { get; set; }

        public int? ProductId { get; set; }
        
        public virtual Product  Product { get; set; }
    }
}
