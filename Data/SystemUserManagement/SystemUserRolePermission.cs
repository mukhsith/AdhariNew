using Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.SystemUserManagement
{
    public partial class SystemUserRolePermission : BaseEntityDate
    {
        [ForeignKeyAttribute("SystemUserPermissionId")]
        public int PermissionId { get; set; }
        [ForeignKeyAttribute("SystemUserRoleId")]
        public int RoleId { get; set; }      
        public bool Allowed { get; set; }
        public virtual SystemUserPermission Permission { get; set; }
        public virtual SystemUserRole Role { get; set; }
    }
}
