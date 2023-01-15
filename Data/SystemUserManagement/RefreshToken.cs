using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.SystemUserManagement
{
    public class RefreshToken : BaseEntityDate
    { 
        public string UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Cancelled { get; set; }
        //[ForeignKey(nameof(UserId))]
        //public IdentityUser User {get;set;}
    }
}
