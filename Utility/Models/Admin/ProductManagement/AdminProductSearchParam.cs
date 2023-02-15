using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.API;

namespace Utility.Models.Admin.ProductManagement
{
    public class AdminProductSearchParam
    {
        public bool IsEnglish { get; set; }
        public int SelectedTab { get; set; }
        public DataTableParam DatatableParam { get; set; }
        public string productName { get; set; } = null;
        public int? categoryID { get; set; } = null;
    }
}
