using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Data.ProductManagement
{
    
    public class ItemSize : BaseEntityCommon
    {

        [StringLength(Constants.MediumDataSize)]
        public string NameEn { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string NameAr { get; set; }


    }
}
