using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Utility.API;
//using System.Web.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Utility.ResponseMapper;
using Data.SystemUserManagement;
using Utility.Enum;
using Services.Backend.SystemUserManagement;
using Utility.Models.Base;
using UAParser;
using Utility.Models.Admin.SystemUserManagement;

namespace API.Areas.Backend.Controllers
{
    public class SystemUserController : BaseController
    { 
        private readonly AppSettingsModel _appSettings;
        private readonly ILogger _logger; 
        public SystemUserController(
            ISystemUserService systemUserService,
            IOptions<AppSettingsModel> options,
            ILoggerFactory logger 
            ) : base(options, systemUserService, PermissionTypes.SystemUser)
        { 
            _logger = logger.CreateLogger(typeof(SystemUserController).Name);

            _appSettings = options.Value; 
        }
  
        #region Login Admin
        [HttpPost, Route("api/SystemUser/LoginAdminNew")]
        public async Task<IActionResult> LoginAdminNew(LoginModel loginModel)
        {
            ResponseMapper<AuthenticationResult> response = new();
            try
            {
                var activeUserCount = await base.SystemUserService.GetAllUserCount();
                if (activeUserCount == 0) //initialized Database if no users exists
                {
                    await InitializeDatabase();
                }
                var systemUserHistory = new SystemUserHistory();
                var user = await base.SystemUserService.GetUserByFullName(loginModel.FullName);
                if (user == null)
                {
                    systemUserHistory.Description = "Login user failed";
                    // response.Login(user);
                    await base.SystemUserService.AddUserHistory(systemUserHistory);
                    return NotFound(response);
                }
                var isValidPassword = Utility.Helpers.PasswordHasher.CompareEncryptedPassword(loginModel.Password  , user.EncryptedPassword);
                if (isValidPassword == false)
                {
                    systemUserHistory.Description = "Password does not match";
                    //  response.Login(user);
                    await base.SystemUserService.AddUserHistory(systemUserHistory);
                    return NotFound(response);
                }

                var authResult = Utility.API.APIHelper.GetJwtToken(user, _appSettings);
                //var authResult = GetJwtToken(systemUser);// GetAccessToken(systemUser, out string expiration);//CreateJwt(systemUser); 
                //user.Token = authResult.AccessToken;
                systemUserHistory.Name = user.UserName;
                systemUserHistory.Description = "Login success";
                systemUserHistory.UserId = user.Id;
                response.Login(authResult);
                await base.SystemUserService.AddUserHistory(systemUserHistory);
                await LoginHistory(loginModel);
                //response.Data = systemUser;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpGet, Route("api/SystemUser/LoginAdmin")]
        public async Task<IActionResult> LoginAdmin(string emailAddress, string password)
        {
            ResponseMapper<AuthenticationResult> response = new();
            try
            {
                var activeUserCount = await base.SystemUserService.GetAllUserCount();
                if (activeUserCount == 0) //initialized Database if no users exists
                {
                    await InitializeDatabase();
                }
                var systemUserHistory = new SystemUserHistory();
                var user = await base.SystemUserService.GetUserByEmail(emailAddress);
                if (user == null)
                {
                    systemUserHistory.Description = "Login user failed";
                   // response.Login(user);
                    await base.SystemUserService.AddUserHistory(systemUserHistory);
                    return NotFound(response);
                }
                var isValidPassword = Utility.Helpers.PasswordHasher.CompareEncryptedPassword(password, user.EncryptedPassword);
                if (isValidPassword == false)
                {
                    systemUserHistory.Description = "Password does not match";
                   //  response.Login(user);
                    await base.SystemUserService.AddUserHistory(systemUserHistory);
                    return NotFound(response);
                }
              
                var authResult = Utility.API.APIHelper.GetJwtToken(user, _appSettings);
                //var authResult = GetJwtToken(systemUser);// GetAccessToken(systemUser, out string expiration);//CreateJwt(systemUser); 
                //user.Token = authResult.AccessToken;
                systemUserHistory.Name = user.FullName;
                systemUserHistory.Description = "Login success";
                systemUserHistory.UserId = user.Id;
                response.Login(authResult);
                await base.SystemUserService.AddUserHistory(systemUserHistory);
                //await LoginHistory(loginModel);
                //response.Data = systemUser;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        private async Task LoginHistory(LoginModel loginModel)
        {
            var _Headers = HttpContext.Request.Headers["User-Agent"];
            var _Parser = Parser.GetDefault();
            try
            {
                ClientInfo _ClientInfo = _Parser.Parse(_Headers);

                LoginHistory login = new LoginHistory
                {
                    Browser = _ClientInfo.UA.Family,
                    OperatingSystem = _ClientInfo.OS.Family,
                    Device = _ClientInfo.Device.Family,
                    ActionStatus =  "Success"  ,
                    Latitude = loginModel.Latitude,
                    Longitude = loginModel.Longitude,
                };
               await base.SystemUserService.AddLoginHistory(login); 
            }
            catch (Exception ex)
            { 
            }
        }
            #endregion

        #region LoggedIn User Menu
        [HttpGet, Route("api/SystemUser/GetMenuByRoleId")]
        public async Task<IActionResult> GetMenuByRoleId(int id)
        {
            ResponseMapper<List<SystemUserPermission>> response = new();
            try
            {
                 
                if (!await Allowed((int)PermissionTypes.Dashboard))
                {
                    return Ok(accessResponse);
                }

                var items = await base.SystemUserService.GetMenuPermissionsById(id, IsEnglish);
                response.GetAll(items);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        #endregion

        #region System User Management

        [HttpPost, Route("api/SystemUser/GetAllUserForDataTable")]
        public async Task<IActionResult> GetAllUserForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await base.SystemUserService.GetAllSystemUserForDataTable(base.GetDataTableParameters,this.RoleId);
                return Ok(items);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpGet, Route("api/SystemUser/GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            ResponseMapper<List<SystemUser>> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var items = await base.SystemUserService.GetAllSystemUser(this.RoleId);
                 response.GetAll(items);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpGet, Route("api/SystemUser/GetAllDriver")]
        public async Task<IActionResult> GetAllDriver()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var items = await base.SystemUserService.GetAllDriver();
                response.GetAll(items);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpGet, Route("api/SystemUser/ByIdUser")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            ResponseMapper<SystemUser> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await base.SystemUserService.GetSystemUserById(id);
                response.GetById(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/SystemUser/AddEditUser")]
        public async Task<IActionResult> AddEditUser([FromForm] SystemUser item)
        {
            ResponseMapper<SystemUser> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                //if (await SystemUserService.Exists(item))
                //{
                //    accessResponse.Message = item.EmailAddress + " email is already taken";
                //    accessResponse.Success = false;
                //    accessResponse.StatusCode = 300;
                //    return Ok(accessResponse);
                //}

                if (item.Id > 0)
                {
                    item.ModifiedBy = this.UserId; 
                    await base.SystemUserService.UpdateSystemUser(item);
                    response.Update(item);
                }
                else
                {
                    item.CreatedBy = this.UserId; 
                    await base.SystemUserService.CreateSystemUser(item); 
                    response.Create(item);
                    response.Message = item.FullName + " user is created successfully";
                }

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpPost, Route("api/SystemUser/ToggleActiveUser")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<SystemUser> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var item = await base.SystemUserService.ToggleActiveSystemUser(Id);
                response.ToggleActive(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }
        [HttpDelete, Route("api/SystemUser/DeleteUser")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            ResponseMapper<SystemUser> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var systemUser = await base.SystemUserService.GetSystemUserById(Id);
                if (systemUser != null)
                {
                    var item = await base.SystemUserService.DeleteSystemUser(systemUser);
                    response.Delete(item);
                }
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }



        #endregion

        #region Permissions

        [HttpPost, Route("api/SystemUser/GetAllPermissionForDataTable")]
        public async Task<IActionResult> GetAllPermissionForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await base.SystemUserService.GetAllPermissionForDataTable(base.GetDataTableParameters);
                // response.GetAll(items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet, Route("api/SystemUser/GetAllPermission")]
        public async Task<IActionResult> GetAllPermission()
        {
            ResponseMapper<List<SystemUserPermission>> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var item = await base.SystemUserService.GetAllPermission();
                response.GetAll(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }
        
        [HttpGet, Route("api/SystemUser/GetPermissionById")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            ResponseMapper<SystemUserPermission> response = new();
            try
            {
                if (!await Allowed((int)PermissionTypes.Dashboard))
                {
                    return Ok(accessResponse);
                }

                var items = await base.SystemUserService.GetPermissionById(id);
                response.GetById(items);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        
        [HttpPost, Route("api/SystemUser/ToggleActivePermission")]
        public async Task<IActionResult> ToggleActivePermission(int Id)
        {
            ResponseMapper<SystemUser> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var item = await base.SystemUserService.ToggleActiveUserPermission(Id);
                response.ToggleActive(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/SystemUser/AddEditPermission")]
        public async Task<IActionResult> AddEditPermission([FromForm] SystemUserPermission item)
        {
            ResponseMapper<SystemUserPermission> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                if (item.Id > 0)
                {
                    item.ModifiedBy = this.UserId;
                    await base.SystemUserService.UpdateSystemPermission(item);
                    response.Update(item);
                }
                else
                {
                    item.CreatedBy = this.UserId;
                    await base.SystemUserService.CreateUserPermission(item);
                    response.Create(item);
                }

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        

        #endregion

        #region System User Role
        [HttpGet, Route("api/SystemUser/GetRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            ResponseMapper<SystemUserRole> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var item = await base.SystemUserService.GetSystemUserRoleById(id);
                response.GetById(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        [HttpPost, Route("api/SystemUser/AddEditRole")]
        public async Task<IActionResult> AddEditRoleAsync([FromForm] SystemUserRole item)
        {
            ResponseMapper<SystemUserRole> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                if (item.Id > 0)
                {
                    item.ModifiedBy = this.UserId; 
                    await base.SystemUserService.UpdateSystemUserRole(item);
                    response.Update(item);
                }
                else
                {
                    item.CreatedBy = this.UserId; 
                    await base.SystemUserService.CreateSystemUserRole(item);
                    response.Create(item);
                }

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/SystemUser/ToggleActiveRole")]
        public async Task<IActionResult> ToggleActiveRoleAsync(int Id)
        {
            ResponseMapper<SystemUser> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var item = await base.SystemUserService.ToggleActiveSystemUserRole(Id);
                response.ToggleActive(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpGet, Route("api/SystemUser/GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            ResponseMapper<List<SystemUserRole>> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var items = await base.SystemUserService.GetAllSystemUserRole(this.RoleId);
                response.GetAll(items);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/SystemUser/GetAllRolesForDataTable")]
        public async Task<IActionResult> GetAllRolesForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await base.SystemUserService.GetAllRolesForDataTable(base.GetDataTableParameters, this.RoleId);
                // response.GetAll(items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet, Route("api/SystemUser/GetPermissionByRoleId")]
        public async Task<IActionResult> GetPermissionByRoleId(int id)
        {
            ResponseMapper<List<SystemUserRolePermission>> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                }

                var item = await base.SystemUserService.GetRolePermission(id);
                response.GetAll(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
      
        
        [HttpPost, Route("api/SystemUser/UpdateRolePermission")]
        public async Task<IActionResult> UpdateRolePermission( [FromForm] UpdateRoleModel updateRole)
        {
            ResponseMapper<List<SystemUserRolePermission>> response = new();
            try
            {
                if (!await Allowed())
                {
                    return Ok(accessResponse);
                } 
                var item = await base.SystemUserService.UpdateRolePermission(updateRole);
                response.GetAll(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        #endregion


        #region Database Initialized system user + add/edit permissions+Dashboad access menu
        private async Task InitializeDatabase()
        {
            try
            {
                #region Default System Internal Role
                var rootRole = await base.SystemUserService.CreateSystemUserRole(
                    new SystemUserRole
                    {
                        Name = "Root", //internal system user
                        DisplayOrder = 1,
                        Active = true,
                        Deleted = false,
                        CreatedBy = null,
                        CreatedOn = DateTime.Now
                    });

                await base.SystemUserService.CreateSystemUserRole(
                    new SystemUserRole
                    {
                        Name = "Administrator", //admin
                        DisplayOrder = 2,
                        Active = true,
                        Deleted = false,
                        CreatedBy = null,
                        CreatedOn = DateTime.Now
                    }
                );
                #endregion

                #region System Internal user
                var user = await base.SystemUserService.CreateSystemUser( new SystemUser
                    {
                    FullName = "System Root Admin", //default system user
                    MobileNumber = "97429779",
                    EmailAddress = "bk@gmail.com", //login id
                    GenderTypeId = (GenderType)(int)GenderType.Male, 
                    EncryptedPassword = Utility.Helpers.PasswordHasher.EncryptPlainTextToCipherText("md999"), //password
                    DisplayOrder = 1,
                    RoleId = rootRole.Id,
                    Active = true,
                    Deleted = false,
                    CreatedBy = null,
                    CreatedOn = DateTime.Now,
                     }
                );
                #endregion

                #region initial Main Menu for permissions
                var permission1 = await base.SystemUserService.CreateUserPermission(
                    new SystemUserPermission
                    { 
                        Title = "Dashboard",
                        NavigationUrl = "#",
                        Icon = "fa-solid fa-house",
                        ParentPermissionId = null,
                        CreatedBy = user.Id, //user id
                        CreatedOn = DateTime.Now,
                        DisplayOrder = 1,
                        Active = true,
                        Deleted = false
                    });

                
                var permission2 = await base.SystemUserService.CreateUserPermission(
                    new SystemUserPermission
                    { 
                        Title = "User Management",
                        NavigationUrl = "#",
                        Icon = "fas fa-user-cog",
                        ParentPermissionId = null,
                        CreatedBy = user.Id,
                        CreatedOn = DateTime.Now,
                        DisplayOrder = 100, //to keep it at the end of the menu
                        Active = true,
                        Deleted = false
                    });
                var permission3 = await base.SystemUserService.CreateUserPermission(
                   new SystemUserPermission
                   { 
                       Title = "System Users",
                       NavigationUrl = "/SystemUserManagement/UserList",
                       Icon = null,
                       ParentPermissionId = permission2.Id, //master (Parent User Management)
                       CreatedBy = user.Id,
                       CreatedOn = DateTime.Now,
                       DisplayOrder = 101,
                       Active = true,
                       Deleted = false
                   });
                var permission4 = await base.SystemUserService.CreateUserPermission(
                   new SystemUserPermission
                   { 
                       Title = "System Roles",
                       NavigationUrl = "/SystemUserManagement/UserRoleList",
                       Icon = null,
                       ParentPermissionId = permission2.Id, //master (Parent User Management)
                       CreatedBy = user.Id,
                       CreatedOn = DateTime.Now,
                       DisplayOrder = 102,
                       Active = true,
                       Deleted = false
                   });
                var permission5 = await base.SystemUserService.CreateUserPermission(
                   new SystemUserPermission
                   { 
                       Title = "System Permissions",
                       NavigationUrl = "/SystemUserManagement/UserPermissionList",
                       Icon = null,
                       ParentPermissionId = permission2.Id, //master (Parent User Management)
                       CreatedBy = user.Id,
                       CreatedOn = DateTime.Now,
                       DisplayOrder = 103,
                       Active = true,
                       Deleted = false
                   });
                #endregion

                #region Menu Permissions
                //setup for initial permission
                await base.SystemUserService.CreateUserRolePermission(
                    new SystemUserRolePermission
                    { 
                        PermissionId = permission1.Id, //Dashboard
                        RoleId = rootRole.Id,//internal root user
                        Allowed = true
                    });
                await base.SystemUserService.CreateUserRolePermission(
                    new SystemUserRolePermission
                    { 
                        PermissionId = permission2.Id, //User Management
                        RoleId = rootRole.Id,//internal root user
                        Allowed = true
                    });
                await base.SystemUserService.CreateUserRolePermission(
                    new SystemUserRolePermission
                    { 
                        PermissionId = permission3.Id, //Create User 
                        RoleId = rootRole.Id,//internal root user
                        Allowed = true
                    });
                await base.SystemUserService.CreateUserRolePermission(
                    new SystemUserRolePermission
                    { 
                        PermissionId = permission4.Id, //Create Role
                        RoleId = rootRole.Id,//internal root user
                        Allowed = true
                    });
                await base.SystemUserService.CreateUserRolePermission(
                    new SystemUserRolePermission
                    { 
                        PermissionId = permission5.Id, // Create Permission
                        RoleId = rootRole.Id, //internal root user
                        Allowed = true
                    });
                #endregion


            }
            catch (Exception exp)
            {
                var msg = exp.Message;
                var ms = exp.InnerException;
            }
        }
        #endregion


        //[HttpGet, Route("api/SystemUser/LoginAdmin")]
        //public async Task<IActionResult> LoginAdmin(string emailAddress, string password)
        //{
        //    ResponseMapper<AuthenticationResult> response = new();
        //    try
        //    {
        //        var activeUserCount = await base.SystemUserService.GetAllUserCount();
        //        if (activeUserCount == 0) //initialized Database if no users exists
        //        {
        //            await InitializeDatabase();
        //        }
        //        var systemUserHistory = new SystemUserHistory();
        //        var user = await base.SystemUserService.GetUserByEmail(emailAddress);
        //        if (user == null)
        //        {
        //            systemUserHistory.Description = "Login user failed";
        //            // response.Login(user);
        //            await base.SystemUserService.AddUserHistory(systemUserHistory);
        //            return NotFound(response);
        //        }
        //        var isValidPassword = Utility.Helpers.PasswordHasher.CompareEncryptedPassword(password, user.EncryptedPassword);
        //        if (isValidPassword == false)
        //        {
        //            systemUserHistory.Description = "Password does not match";
        //            //  response.Login(user);
        //            await base.SystemUserService.AddUserHistory(systemUserHistory);
        //            return NotFound(response);
        //        }

        //        var authResult = Utility.API.APIHelper.GetJwtToken(user, _appSettings);
        //        //var authResult = GetJwtToken(systemUser);// GetAccessToken(systemUser, out string expiration);//CreateJwt(systemUser); 
        //        //user.Token = authResult.AccessToken;
        //        systemUserHistory.Name = user.FullName;
        //        systemUserHistory.Description = "Login success";
        //        systemUserHistory.UserId = user.Id;
        //        response.Login(authResult);
        //        await base.SystemUserService.AddUserHistory(systemUserHistory);
        //        //await LoginHistory(loginModel);
        //        //response.Data = systemUser;
        //        return Ok(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        response.CacheException(ex);
        //        _logger.LogError(ex.Message);
        //    }

        //    return Ok(response);
        //}
    }
}

