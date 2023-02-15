using Data.Content;
using Data.EntityFramework;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Helpers;
using System.Linq.Dynamic.Core; 
using Utility.Models.Base;
using Utility.Enum;
using System.Drawing;
using Utility.Models.Admin.SystemUserManagement;
using Services.Backend.Sales;

namespace Services.Backend.SystemUserManagement
{
    public class SystemUserService : ISystemUserService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected readonly IOrderService _orderService;


        protected string ErrorMessage = string.Empty;
        public SystemUserService(ApplicationDbContext dbcontext,
            IOrderService orderService)
        {
            _dbcontext = dbcontext;
            _orderService = orderService;


        }

        #region System user
        public async Task<string> GetUserName(int id)
        {
            return await _dbcontext.SystemUsers.Where(x=>x.Id==id).Select(x=>x.FullName).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> GetAllUserCount()
        {
            return await _dbcontext.SystemUsers.Where(x => x.Deleted == false).AsNoTracking().CountAsync();
        }
        public async Task<dynamic> GetAllDriver()
        {
            dynamic users =  await _dbcontext
                           .SystemUsers 
                           .Where(x => x.Deleted == false && x.RoleId==Constants.DRIVER_ROLE_ID)
                           .Select(x=> new  {x.Id,x.FullName,x.Deleted,x.RoleId})
                           .AsNoTracking()
                           .ToListAsync(); 

            return users;
        }
        public async Task<List<SystemUser>> GetAllSystemUser(int roleId)
        {
            List<SystemUser> users = new();
            if (roleId == Utility.Helpers.Constants.ROOT_ROLE_ID) //internal system user
            {
                users = await _dbcontext
                           .SystemUsers
                           .Include(x => x.Role)
                           .Where(x => x.Deleted == false)
                           .AsNoTracking()
                           .ToListAsync();
            }
            else
            {
                users = await _dbcontext
                           .SystemUsers
                           .Include(x => x.Role)
                           .Where(x => x.Deleted == false && x.RoleId > Utility.Helpers.Constants.ROOT_ROLE_ID)
                           .AsNoTracking()
                           .ToListAsync();
            }


            return users;
        }
        public async Task<DataTableResult<List<SystemUser>>> GetAllSystemUserForDataTable(DataTableParam param, int roleId)
        {
            DataTableResult<List<SystemUser>> result = new() { Draw = param.Draw };
            try

            {
                var items = _dbcontext.SystemUsers.Include(x => x.Role).Where(x => x.Deleted == false);

                if (roleId != Constants.ROOT_ROLE_ID)
                {
                    items = items.Where(x => x.RoleId > Utility.Helpers.Constants.ROOT_ROLE_ID);

                }

                //User Search
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.UserName.ToLower().Contains(SearchValue) ||
                     obj.FullName.ToLower().Contains(SearchValue) ||
                     obj.MobileNumber.ToLower().Contains(SearchValue) ||
                     obj.EmailAddress.ToString().Contains(SearchValue)
                     );
                }
                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);//.ToList();
                }
                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).ToListAsync();
                
                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        private SystemUser GetPassword(SystemUser user) {
            if (user is not null)
            {
                user.Password = PasswordHasher.DecryptCipherTextToPlainText(user.EncryptedPassword);
            }
            return user;
        }

        public async Task<SystemUser> GetSystemUserById(int id)
        {
           // var allPermissions = await _dbcontext.SystemUserRolePermissions.Where(x => x.Allowed == true).AsNoTracking().ToListAsync();

            var data = await _dbcontext
                            .SystemUsers
                            .Include(x => x.Role)
                            .Where(x=>x.Id==id)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
             data = GetPassword(data);
           
            return data;
        }
        public async Task<SystemUser> GetSystemUserByEmail(string email)
        {
            var data = await _dbcontext
                            .SystemUsers
                            .Where(a => a.EmailAddress.ToLower() == email.ToLower())
                            .Include(a => a.Role)
                            .FirstOrDefaultAsync();
            return data;
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {
            UserModel user = new();
            var data = await _dbcontext
                            .SystemUsers
                            .Where(a => a.EmailAddress.ToLower() == email.ToLower())
                            .Include(a => a.Role)
                            .FirstOrDefaultAsync();
            if(data is not null)
            {
                user.Id = data.Id;
                user.UserName = data.UserName.ToString();
                user.FullName = data.FullName.ToString();
                user.EmailAddress = data.EmailAddress.ToString();
                user.MobileNumber = data.MobileNumber.ToString();
                user.EncryptedPassword = data.EncryptedPassword.ToString();
                user.RoleName = data.Role.Name;
                user.RoleId = data.RoleId.ToString();
                user.GUID = data.GUID.ToString();
            }
            return user;
        }
        public async  Task<UserModel> GetUserByFullName(string fullName)
        {
            UserModel user = new();
            var data = await _dbcontext
                            .SystemUsers
                            .Where(a => a.FullName.ToLower() == fullName.ToLower() && a.Active==true)
                            .Include(a => a.Role)
                            .FirstOrDefaultAsync();
            if (data is not null)
            {
                user.Id = data.Id;
                user.UserName = data.UserName.ToString();
                user.FullName = data.FullName.ToString();
                user.EmailAddress = data.EmailAddress.ToString();
                user.MobileNumber = data.MobileNumber.ToString();
                user.EncryptedPassword = data.EncryptedPassword.ToString();
                user.RoleName = data.Role.Name;
                user.RoleId = data.RoleId.ToString();
                user.GUID = data.GUID.ToString();
            }
            return user;
        }
        public async Task<SystemUser> CreateSystemUser(SystemUser model)
        {
            
            var encryptedPassword = PasswordHasher.EncryptPlainTextToCipherText(model.Password); 
            model.EncryptedPassword = encryptedPassword;
            model.GUID = Guid.NewGuid();
            model.CreatedOn = DateTime.Now;
            await _dbcontext.SystemUsers.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> Exists(SystemUser model)
        {
            var data = await _dbcontext.SystemUsers
                            .Where(a => a.EmailAddress.ToLower() == model.EmailAddress.ToLower())
                            .AsNoTracking().FirstOrDefaultAsync();
            if (data is not null && data.Id != model.Id)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> UpdateSystemUser(SystemUser model)
        {
            var data = await _dbcontext
                .SystemUsers
                .Where(x=>x.Id == model.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (data is not null)
            {
                
                
                //get existingPassword 
                var existingPassword = PasswordHasher.DecryptCipherTextToPlainText(data.EncryptedPassword);
                //match with user provided password
                if(existingPassword != model.Password)
                {  // if not match update
                    data.EncryptedPassword = PasswordHasher.EncryptPlainTextToCipherText(model.Password);
                }
                data.UserName = model.UserName;
                data.FullName = model.FullName;
                data.MobileNumber = model.MobileNumber;
                data.RoleId = model.RoleId;
                data.GenderTypeId = model.GenderTypeId; 
                data.ModifiedOn = DateTime.Now;
                 
                _dbcontext.Update(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            } 
                return false;
             
        }

        public async Task<SystemUser> UpdateSystemUser(int id, string newPassword)
        {
            var data = await _dbcontext.SystemUsers.FindAsync(id);
            if (data is not null)
            {
                var encryptedPassword = PasswordHasher.EncryptPlainTextToCipherText(newPassword);
                
                data.ModifiedOn = DateTime.Now;
                data.EncryptedPassword = encryptedPassword;
                await _dbcontext.SaveChangesAsync();
                return data;
            }
            return data;
        }
        public async Task<bool> DeleteSystemUser(SystemUser model)
        {
            model.ModifiedOn = DateTime.Now;
            model.Deleted = true;
            return await UpdateSystemUser(model);
        }
        public async Task<bool> ToggleActiveSystemUser(int id)
        {
            var data = await _dbcontext.SystemUsers.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateDisplayOrderSystemUser(int id, int num = 0)
        {
            var data = await _dbcontext.SystemUsers.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion
        #region LoggedIn User Permission
        public async Task<List<SystemUserPermission>> GetMenuPermissionsById(int roleId, bool isEnglish)
        {
            var PendingCustomerFeedbackCount = (await _dbcontext.CustomerFeedbacks.Where(x => x.Status == 0).AsNoTracking().ToListAsync()).Count;
            var SalesOrderBadgeCount = 0;
            var model = await _orderService.GetTodaySales();
            if (model is not null)
            {

                SalesOrderBadgeCount = (int)model.OrderReceivedToday;

            }




                var permissions = await (from role in _dbcontext.SystemUserRolePermissions
                                     join permission in _dbcontext.SystemUserPermissions
                                     on role.PermissionId equals permission.Id
                                     where role.RoleId == roleId
                                     && role.Allowed == true
                                     && permission.Active == true

                                     select new SystemUserPermission
                                     {
                                         Id = permission.Id,
                                         Title = isEnglish? permission.Title:permission.TitleAr,
                                         TitleAr= permission.TitleAr,
                                         NavigationUrl = permission.NavigationUrl,
                                         Icon = permission.Icon,
                                         Active = permission.Active,
                                         ParentPermissionId = permission.ParentPermissionId,
                                         DisplayOrder = permission.DisplayOrder
                                     })
                                        .OrderBy(x => x.DisplayOrder)
                                        .AsNoTracking()
                                        .ToListAsync();


            var menuItems = new SystemUserPermission().GetMenuTree(permissions, null);
            foreach(var item in menuItems)
            {
                //item.Title = isEnglish ? item.Title : item.TitleAr;
                //foreach(var childItem in item.ChildPermissions)
                //{
                //    childItem.Title = isEnglish ? item.Title : item.TitleAr;
                //}
                if (item.ChildPermissions.Count > 0)
                {
                    var found = item.ChildPermissions.FirstOrDefault(x => x.Id == (int)PermissionTypes.CustomerFeedback);
                    if (found != null)
                    {
                        item.ChildPermissions.First(x => x.Id == (int)PermissionTypes.CustomerFeedback).NotificationBadgeCount = PendingCustomerFeedbackCount;
                        item.NotificationBadgeCount = PendingCustomerFeedbackCount;

                    }


                    var foundOrder = item.ChildPermissions.FirstOrDefault(x => x.Id == (int)PermissionTypes.Orders);
                    if (foundOrder != null)
                    {
                        item.ChildPermissions.First(x => x.Id == (int)PermissionTypes.Orders).SalesOrderBadgeCount = SalesOrderBadgeCount;
                        item.SalesOrderBadgeCount = SalesOrderBadgeCount;

                    }

                }
            }
            //for(var index=0; index < menuItems.Count; index++)
            //{
            //    var item = menuItems[index];
            //    if (item.ChildPermissions.Count > 0)
            //    {
            //        var found =item.ChildPermissions.FirstOrDefault(x=>x.Id == (int)PermissionTypes.CustomerFeedback);
            //        if(found!=null)
            //         {
            //            item.ChildPermissions.First(x => x.Id == (int)PermissionTypes.CustomerFeedback).NotificationBadgeCount = PendingCustomerFeedbackCount;
            //            item.NotificationBadgeCount = PendingCustomerFeedbackCount;

            //        }
            //    }
              
            //}
           
            return menuItems;
        }
        #endregion
        
        #region System User Permissions

        public async Task<SystemUserPermission> CreateSystemUserPermission(SystemUserPermission model)
        {
            
            await _dbcontext.SystemUserPermissions.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateSystemUserPermission(SystemUserPermission model)
        {
            model.ModifiedOn = DateTime.Now;
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteSystemUserPermission(SystemUserPermission model)
        {
            model.Deleted = true;
            model.ModifiedOn = DateTime.Now;
            return await UpdateSystemUserPermission(model);
        }
        public async Task<bool> ToggleActiveUserPermission(int id)
        {
            var data = await _dbcontext.SystemUserPermissions.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateDisplayOrderSystemUserPermission(int id, int num = 0)
        {
            var data = await _dbcontext.SystemUserPermissions.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<List<SystemUserPermission>> GetAllSystemUserPermission()
        {
            var data = await _dbcontext
                           .SystemUserPermissions
                           .Where(x => x.Deleted == false)
                           .AsNoTracking()
                           .ToListAsync();

            return data;
        }
        #endregion
        
        #region System user Roles 
        public async Task<List<SystemUserRole>> GetAllSystemUserRole(int roleId)
        {
            List<SystemUserRole> userRoles = new();
            if (roleId == Utility.Helpers.Constants.ROOT_ROLE_ID)
            {
                userRoles = await _dbcontext
                           .SystemUserRoles
                           .Where(x => x.Deleted == false)
                           .ToListAsync();
            }
            else
            {
                userRoles = await _dbcontext
                           .SystemUserRoles
                           .Where(x => x.Deleted == false && x.Id > Utility.Helpers.Constants.ROOT_ROLE_ID)
                           .ToListAsync();

            }

            return userRoles;
        }

        public async Task<DataTableResult<List<SystemUserRole>>> GetAllRolesForDataTable(DataTableParam param, int roleId)
        {
            DataTableResult<List<SystemUserRole>> result = new() { Draw = param.Draw };
            try

            {
                //List<SystemUserRole> userRoles = new();
               
                var items = _dbcontext.SystemUserRoles.Where(x => x.Deleted == false);  

                if (roleId > Constants.ROOT_ROLE_ID)
                {
                    items = items.Where(obj => obj.Id == Constants.ROOT_ROLE_ID); 

                }
               

                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);//.ToList();
                }
                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).ToListAsync();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        public async Task<SystemUserRole> GetSystemUserRoleById(int id)
        {
            var data = await _dbcontext.SystemUserRoles.FindAsync(id);
            return data;
        }
        public async Task<SystemUserRole> CreateSystemUserRole(SystemUserRole model)
        {
            try
            {
                model.CreatedOn = DateTime.Now;
                await _dbcontext.SystemUserRoles.AddAsync(model);
                await _dbcontext.SaveChangesAsync();
            } catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return model;
        }
        public async Task<bool> UpdateSystemUserRole(SystemUserRole model)
        {
            try
            {
                model.ModifiedOn = DateTime.Now;
                _dbcontext.Update(model);
                return await _dbcontext.SaveChangesAsync() > 0;
            } catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return false;
        }
        public async Task<bool> DeleteSystemUserRole(SystemUserRole model)
        {
            model.Deleted = true;
            model.ModifiedOn = DateTime.Now;
            return await UpdateSystemUserRole(model);
        }
        public async Task<bool> ToggleActiveSystemUserRole(int id)
        {
            var data = await _dbcontext.SystemUserRoles.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateDisplayOrderSystemUserRole(int id, int num = 0)
        {
            var data = await _dbcontext.SystemUserRoles.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> Allowed(int roleId, int permissionId)
        {
            var rolePermission = await _dbcontext
                                .SystemUserRolePermissions
                                .Where(a => a.RoleId == roleId 
                                && a.PermissionId == permissionId)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            if (rolePermission == null)
                return false;

            return rolePermission.Allowed;
        }
        public async Task<List<SystemUserRolePermission>> GetRolePermission(int roleId)
        {
            var data = await _dbcontext
                          .SystemUserRolePermissions
                          .Include(x=>x.Role)
                          .Where(x => x.Deleted == false && x.RoleId==roleId)
                          .AsNoTracking()
                          .ToListAsync();
            return data;
        }

        public async Task<SystemUserPermission> GetPermissionById(int id)
        {
            var data = await _dbcontext
                          .SystemUserPermissions 
                          .Where(x => x.Deleted == false && x.Id == id)
                          .AsNoTracking()
                          .FirstOrDefaultAsync();
            return data;
        }
        public async Task<List<SystemUserPermission>> GetAllPermission()
        {
            var data = await _dbcontext
                          .SystemUserPermissions
                          .Where(x => x.Deleted == false)
                          .AsNoTracking()
                          .ToListAsync();
            return data;
        }

        public async Task<DataTableResult<List<SystemUserPermission>>> GetAllPermissionForDataTable(DataTableParam param)
        {
             
                DataTableResult<List<SystemUserPermission>> result = new() { Draw = param.Draw };
                try
                {
                    var items = _dbcontext.SystemUserPermissions
                             .Select(x => new SystemUserPermission
                             {
                                 Id = x.Id,
                                 DisplayOrder = x.DisplayOrder,
                                 Title = x.Title,
                                 TitleAr=x.TitleAr,
                                 NavigationUrl = x.NavigationUrl,
                                 ParentPermissionId = x.ParentPermissionId, 
                                 CreatedOn = x.CreatedOn,
                                 Active = x.Active,
                                 Deleted = x.Deleted
                             }).Where(x => x.Deleted == false);
                //User Search
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.Title.ToLower().Contains(SearchValue) ||
                     obj.NavigationUrl.ToLower().Contains(SearchValue) ||
                     obj.DisplayOrder.ToString().Contains(SearchValue)
                     );
                }

                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                    {
                        //using System.Linq.Dynamic.Core;
                        //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                        items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);//.ToList();
                    }
                    result.RecordsTotal = items.Count();
                    result.RecordsFiltered = items.Count();
                    result.Data = await items.Skip(param.Skip).Take(param.PageSize).ToListAsync();
                    
                    return result;
                }
                catch (Exception err)
                {
                    result.Error = err;
                }
                return result;
             
        } 


        public async Task<SystemUserPermission> CreateUserPermission(SystemUserPermission model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.SystemUserPermissions.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
         
        public async Task<bool> UpdateSystemPermission(SystemUserPermission model)
        {
            var data = await _dbcontext
                                .SystemUserPermissions
                                .Where(x => x.Id == model.Id)
                                .FirstOrDefaultAsync();
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.ModifiedBy = model.ModifiedBy;
                data.Title = model.Title;
                data.TitleAr = model.TitleAr;
                data.ParentPermissionId = model.ParentPermissionId;
                data.NavigationUrl = model.NavigationUrl;
                data.Icon = model.Icon;
                data.DisplayOrder = model.DisplayOrder;
                data.Active = model.Active; 
            }
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<List<SystemUserRolePermission>> UpdateRolePermission(UpdateRoleModel updateRole)
        {
            var data = await _dbcontext
                            .SystemUserRolePermissions
                            .Where(x => x.RoleId == updateRole.RoleId)
                            .AsNoTracking()
                            .ToListAsync();
            foreach (var permission in updateRole.Permissions)
            {
                var find = data.Where(x => x.PermissionId == permission.Id).FirstOrDefault();
                if (find is not null)
                {
                    find.Allowed = permission.Allowed;
                    find.ModifiedOn = DateTime.Now;
                    _dbcontext.Update(find);
                }
                else
                {

                    var newItem = new SystemUserRolePermission
                    {
                        Allowed = permission.Allowed,
                        PermissionId = permission.Id,
                        RoleId = updateRole.RoleId,
                        CreatedOn = DateTime.Now,
                        Deleted = false
                    };
                    await _dbcontext.AddAsync(newItem);
                }
            }

            await _dbcontext.SaveChangesAsync();
            return data;
        }
        public async Task<List<SystemUserRolePermission>> UpdateRolePermission(int roleId, List<int> permissions)
        {
           var data = await _dbcontext
                            .SystemUserRolePermissions
                            .Where(x => x.RoleId == roleId)
                            .AsNoTracking()
                            .ToListAsync();

            foreach(var permissionId in permissions)
            {
                var find = data.Where(x => x.PermissionId == permissionId).FirstOrDefault();
                if(find is not  null)
                {
                    find.Allowed = true;
                     _dbcontext.Update(find);
                } else
                { 
                    
                   var newItem = new SystemUserRolePermission
                    {
                        Allowed = true,
                        PermissionId = permissionId,
                        RoleId = roleId,
                        CreatedOn = DateTime.Now,
                        Deleted = false
                    };
                    await _dbcontext.AddAsync(newItem);
                }
            }
             
              await _dbcontext.SaveChangesAsync();
            return data; 
        }

        public async Task<SystemUserRolePermission> CreateUserRolePermission(SystemUserRolePermission model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.SystemUserRolePermissions.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        #endregion

        #region System user history
        public async Task<SystemUserHistory> AddUserHistory(SystemUserHistory model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.SystemUserHistories.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }


        #endregion

        #region User Login History 
        public async Task<LoginHistory> AddLoginHistory(LoginHistory model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.LoginHistories.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        #endregion

    }
}
