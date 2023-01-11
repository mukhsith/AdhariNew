using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.API
{
    public class DataTableParam 
    {   
        public string Draw { get; set; }
        public bool IsEnglish { get; set; }
        public string SearchValue { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }        
        public int Skip { get; set; }
        public int PageSize { get; set; }
    }
}
