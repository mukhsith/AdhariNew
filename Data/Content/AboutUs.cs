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

namespace Data.Content
{
    public class AboutUs : BaseEntityImage
    { 
        public string TextEn { get; set; }
        public string TextAr { get; set; } 
         
    }
}
