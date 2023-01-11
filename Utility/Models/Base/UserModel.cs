
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Utility.Enum;
using Utility.Helpers;

namespace Utility.Models.Base
{
    public   class UserModel 
    { 
        public int Id { get; set; }
        public string GUID { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public GenderType GenderTypeId { get; set; } 
        public string EncryptedPassword { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}
