
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Data.SystemUserManagement;
using Microsoft.Extensions.Options;
using Utility.API;

namespace Admin.Controllers
{
    public class SystemUserManagementController : BaseController
    {
        public SystemUserManagementController(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options, logger.CreateLogger(typeof(SystemUserManagementController).Name))
        { }

        [HttpGet]
        public IActionResult UserList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserAddEdit(int id)
        {
            return View(new SystemUser { Id = id });
        }

        [HttpGet]
        public IActionResult UserPermissionList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserPermissionAddEdit(int id)
        {
            return View(new SystemUserPermission { Id = id });
        }

        [HttpGet]
        public IActionResult UserRoleList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserRoleAddEdit(int id)
        {
            return View(new SystemUserRole { Id = id });
        }

      
    }
}
