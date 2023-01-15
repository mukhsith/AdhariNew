using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Helpers;

namespace Data.Common
{
    public partial class BaseEntityImage : BaseEntityCommon
    {
        [StringLength(Constants.ImageNameDataSize)]
        public string ImageName { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
