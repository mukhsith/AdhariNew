using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models.Base
{
    public class BaseModel
    {
        public int Id { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public bool Active { get; set; } = false;
        public bool Deleted { get; set; } = false;  

    }
}
