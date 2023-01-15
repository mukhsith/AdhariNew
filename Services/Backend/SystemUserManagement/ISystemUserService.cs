using Data.Content;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Admin.SystemUserManagement;
using Utility.Models.Base;
namespace Services.Backend.SystemUserManagement
{
    public interface ISystemUserService
    {
        #region System user
        Task<string> GetUserName(int id);
        Task<bool> Exists(SystemUser model);
        Task<int> GetAllUserCount();
        Task<dynamic> GetAllDriver();
        Task<List<SystemUser>> GetAllSystemUser(int roleId);

        Task<DataTableResult<List<SystemUser>>> GetAllSystemUserForDataTable(DataTableParam param, int roleId);
        Task<SystemUser> GetSystemUserById(int id);
        Task<SystemUser> GetSystemUserByEmail(string email);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> GetUserByFullName(string name);
        Task<SystemUser> CreateSystemUser(SystemUser model);
        Task<bool> UpdateSystemUser(SystemUser model);
        Task<SystemUser> UpdateSystemUser(int id, string newPassword);
        Task<bool> DeleteSystemUser(SystemUser model);
        Task<bool> ToggleActiveSystemUser(int id);
        Task<bool> UpdateDisplayOrderSystemUser(int id, int num = 0);
        #endregion

        #region LoggedIn User Menu List
        Task<List<SystemUserPermission>> GetMenuPermissionsById(int roleId, bool isEnglish);
        //Task<List<SystemUserPermission>> GetMenuPermissionsById(int roleId);
        #endregion
        
        #region System user role
        Task<List<SystemUserRole>> GetAllSystemUserRole(int roleId);
        Task<DataTableResult<List<SystemUserRole>>> GetAllRolesForDataTable(DataTableParam param, int roleId);

        Task<SystemUserRole> GetSystemUserRoleById(int id);
        Task<SystemUserRole> CreateSystemUserRole(SystemUserRole model);
        Task<bool> UpdateSystemUserRole(SystemUserRole model);
        Task<bool> DeleteSystemUserRole(SystemUserRole model);
        Task<bool> ToggleActiveSystemUserRole(int id);
        Task<bool> UpdateDisplayOrderSystemUserRole(int id, int num = 0);
        Task<bool> Allowed(int roleId, int permissionId); 
        #endregion

        #region System User Role Permission
        Task<List<SystemUserRolePermission>> GetRolePermission(int roleId);
        Task<List<SystemUserRolePermission>> UpdateRolePermission(UpdateRoleModel updateRole);
        
        Task<List<SystemUserPermission>> GetAllPermission(); //below new
        Task<DataTableResult<List<SystemUserPermission>>> GetAllPermissionForDataTable(DataTableParam param);

        Task<SystemUserPermission> GetPermissionById(int id);
        Task<bool> ToggleActiveUserPermission(int id);
        Task<SystemUserPermission> CreateUserPermission(SystemUserPermission model);
        Task<bool> UpdateSystemPermission(SystemUserPermission model);

        Task<SystemUserRolePermission> CreateUserRolePermission(SystemUserRolePermission model);
        #endregion

        #region System user history
        Task<SystemUserHistory> AddUserHistory(SystemUserHistory model);
        #endregion

        #region System user log history 
        Task<LoginHistory> AddLoginHistory(LoginHistory model);
        #endregion
    }
}
