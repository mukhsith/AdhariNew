using Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Utility.Helpers;

namespace Data.SystemUserManagement
{
    public partial class SystemUserPermission : BaseEntityCommon
    {
        [StringLength(Constants.SmallDataSize)]
        public string Title { get; set; }
        [StringLength(Constants.SmallDataSize)]
        public string TitleAr { get; set; }
        [StringLength(Constants.MediumDataSize)]
        public string NavigationUrl { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string Icon { get; set; }
        public int? ParentPermissionId { get; set; }
        public virtual SystemUserPermission ParentPermission { get; set; }
        public ICollection<SystemUserPermission> ChildPermissions { get; set; }
        //public List<SystemUserPermission> GetMenuTree(List<SystemUserPermission> list, int? parentId)
        //{
        //    return list.Where(x => x.ParentPermissionId == parentId)
        //        .Select(x => new SystemUserPermission()
        //        {
        //            Id = x.Id,
        //            Title = x.Title,

        //            NavigationUrl = x.NavigationUrl,
        //            Icon = x.Icon,
        //            ParentPermissionId = x.ParentPermissionId,
        //            Active = x.Active,
        //            ChildPermissions = GetMenuTree(list, x.Id),

        //        })
        //        .ToList();
        //}
        public List<SystemUserPermission> GetMenuTree(List<SystemUserPermission> list, int? parentId)
        {
            return list.Where(x => x.ParentPermissionId == parentId)
                .Select(x => new SystemUserPermission()
                {
                    Id = x.Id,
                    Title = x.Title ,
                    TitleAr = x.TitleAr,
                    NavigationUrl = x.NavigationUrl,
                    Icon = x.Icon,
                    ParentPermissionId = x.ParentPermissionId,
                    Active = x.Active,
                    ChildPermissions = GetMenuTree(list, x.Id),

                })
                .ToList();
        }
        //<<<<<<< HEAD
        //         /// <summary>
        //         /// Notify the count of the pending records
        //         /// </summary>
        //        public bool NotificationBadge { get; set; }

        [NotMapped]
        public virtual int NotificationBadgeCount { get; set; }
        //=======
        //        [NotMapped]
        //        public int NotificationCount { get; set; }
        //>>>>>>> 944b76662bb960c0affbb4f6455c4a0b9e2e6346
    }
}
