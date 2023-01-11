using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.API
{
    public class DataTableResult<T> where T : new()
    {

        public string Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public T Data { get; set; }
        public Exception Error { get; set; }
    }
}
