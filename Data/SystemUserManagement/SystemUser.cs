using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.SystemUserManagement
{
    public partial class SystemUser : BaseEntityCommon
    {
        public Guid GUID { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string FullName { get; set; }
         
        [StringLength(Constants.MobileDataSize)]
        public string MobileNumber { get; set; }

        [StringLength(Constants.EmailDataSize)]
        public string EmailAddress { get; set; }   
        public GenderType GenderTypeId { get; set; }

        [StringLength(Constants.SmallDataSize)] 
        [NotMapped]
        public string Password { get; set; }

        [StringLength(Constants.MediumDataSize)]
        public string EncryptedPassword { get; set; }

        [ForeignKeyAttribute("SystemUserRoleId")]
        public int RoleId { get; set; }
        public SystemUserRole Role { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
