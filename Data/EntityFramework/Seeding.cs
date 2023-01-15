using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using Utility.Enum;

namespace Data.EntityFramework
{
    public static class Seeding
    {
        public static void Data(this ModelBuilder modelBuilder)
        {
            #region system default Roles
            var role1 = new SystemUserRole
            {
                Id = 1,
                Name = "Root",
                DisplayOrder = 1,
                Active = true,
                Deleted = false,
                CreatedBy = null,
                CreatedOn = DateTime.Now
            };

            var role2 = new SystemUserRole
            {
                Id = 2,
                Name = "Administrator",
                DisplayOrder = 2,
                Active = true,
                Deleted = false,
                CreatedBy = null,
                CreatedOn = DateTime.Now
            };

            var role3 = new SystemUserRole
            {
                Id = 3,
                Name = "Sales Manager",
                DisplayOrder = 3,
                Active = true,
                Deleted = false,
                CreatedBy = null,
                CreatedOn = DateTime.Now
            };

            modelBuilder.Entity<SystemUserRole>().HasData(role1);
            modelBuilder.Entity<SystemUserRole>().HasData(role2);
            modelBuilder.Entity<SystemUserRole>().HasData(role3);

            #endregion


            #region  System Default Permissions 
            var per1 = new SystemUserPermission
            {
                Id = 1,
                Title = "Dashboard",
                NavigationUrl = "#",
                Icon = null,
                ParentPermissionId = null,
                CreatedBy = null,
                CreatedOn = DateTime.Now,
                DisplayOrder = 1,
                Active = true,
                Deleted = false
            };

            var per2 = new SystemUserPermission
            {
                Id = 2,
                Title = "User Management",
                NavigationUrl = "#",
                Icon = "fas fa-user-cog",
                ParentPermissionId = null,
                CreatedBy = null,
                CreatedOn = DateTime.Now,
                DisplayOrder = 100,
                Active = true,
                Deleted = false
            };

            var per3 = new SystemUserPermission
            {
                Id = 3,
                Title = "System Users",
                NavigationUrl = "/SystemUserManagement/UserList",
                Icon = null,
                ParentPermissionId = per2.Id,
                CreatedBy = null,
                CreatedOn = DateTime.Now,
                DisplayOrder = 101,
                Active = true,
                Deleted = false
            };
            var per4 =
               new SystemUserPermission
               {
                   Id = 4,
                   Title = "System Roles",
                   NavigationUrl = "/SystemUserManagement/UserRoleList",
                   Icon = null,
                   ParentPermissionId = per2.Id,
                   CreatedBy = null,
                   CreatedOn = DateTime.Now,
                   DisplayOrder = 102,
                   Active = true,
                   Deleted = false
               };

            var per5 = new SystemUserPermission
            {
                Id = 5,
                Title = "System Permissions",
                NavigationUrl = "/SystemUserManagement/UserPermissionList",
                Icon = null,
                ParentPermissionId = per2.Id,
                CreatedBy = null,
                CreatedOn = DateTime.Now,
                DisplayOrder = 103,
                Active = true,
                Deleted = false
            };
            modelBuilder.Entity<SystemUserPermission>().HasData(per1);
            modelBuilder.Entity<SystemUserPermission>().HasData(per2);
            modelBuilder.Entity<SystemUserPermission>().HasData(per3);
            modelBuilder.Entity<SystemUserPermission>().HasData(per4);
            modelBuilder.Entity<SystemUserPermission>().HasData(per5);

            modelBuilder.Entity<SystemUserRolePermission>().HasData(
                new SystemUserRolePermission
                {
                    Id = 1,
                    PermissionId = per1.Id,
                    RoleId = role1.Id,
                    Allowed = true
                });

            modelBuilder.Entity<SystemUserRolePermission>().HasData(new SystemUserRolePermission
            {
                Id = 2,
                PermissionId = per2.Id,
                RoleId = role1.Id,
                Allowed = true
            });

            modelBuilder.Entity<SystemUserRolePermission>().HasData(new SystemUserRolePermission
            {
                Id = 3,
                PermissionId = per3.Id,
                RoleId = role1.Id,
                Allowed = true
            });

            modelBuilder.Entity<SystemUserRolePermission>().HasData(new SystemUserRolePermission
            {
                Id = 4,
                PermissionId = per4.Id,
                RoleId = role1.Id,
                Allowed = true
            });

            modelBuilder.Entity<SystemUserRolePermission>().HasData(new SystemUserRolePermission
            {
                Id = 5,
                PermissionId = per5.Id,
                RoleId = role1.Id,
                Allowed = true
            });

            #endregion

            modelBuilder.Entity<SystemUser>().HasData(
                 new SystemUser
                 {
                     Id = 1,
                     GUID= Guid.NewGuid(),
                     FullName = "system",
                     MobileNumber = "97429779",
                     EmailAddress = "bk@gmail.com",
                     GenderTypeId = (int)GenderType.UnSpecified,
                     EncryptedPassword = Utility.Helpers.PasswordHasher.EncryptPlainTextToCipherText("md999"),
                     DisplayOrder = 1,
                     RoleId = role1.Id,
                     Active = true,
                     Deleted = false,
                     CreatedBy = null,
                     CreatedOn = DateTime.Now,
                 }
             );
        }

    }
}
