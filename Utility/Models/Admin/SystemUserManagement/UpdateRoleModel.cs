using System.Collections.Generic;

namespace Utility.Models.Admin.SystemUserManagement
{
    public class UpdateRoleModel 
    {
        public int RoleId { get; set; }
        public List<PermissionModel> Permissions { get; set; }
    }
}
