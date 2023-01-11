using Data.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;

namespace Data.Content
{
    public partial class SiteContent : BaseEntityImage
    {
        public string ContentEn { get; set; }
        public string ContentAr { get; set; }
        public AppContentType AppContentType { get; set; }
    }
}
