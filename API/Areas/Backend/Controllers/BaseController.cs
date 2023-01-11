using Data.SystemUserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;
 
using Utility.ResponseMapper;
using Services.Backend.SystemUserManagement;
using Utility.API;
using Microsoft.AspNetCore.Http;
using static Utility.Helpers.Common;
using System.Linq.Dynamic.Core.Tokenizer;
using Utility.Enum;

namespace API.Areas.Backend.Controllers
{
    [ApiController]
    [AllowAnonymous]
    //[API.Filters.KeyAuthorization]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : ControllerBase
    {
        protected readonly AppSettingsModel AppSettings;
        protected readonly ISystemUserService SystemUserService;
        public ResponseMapper<SystemUser> accessResponse = new();
        protected string FullName = string.Empty;
        protected int UserId = 0;
        protected int RoleId = 0;
        protected TokenInfo tokenInfo = new();
        protected int PermissionType;
        private string ApiBaseUrl { get; set; }
        protected bool TokenExpired { get; set; }
        public BaseController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            PermissionTypes permissionType=(int)PermissionTypes.None)
        {
            AppSettings = options.Value;
            ApiBaseUrl = AppSettings.APIBaseUrl;
            SystemUserService = systemUserService;
            PermissionType = (int)permissionType;
            
        }

        protected string GetImageUrl(string folderName, string imageName)
        {
            return ApiBaseUrl + folderName + imageName;
        }
        protected string GetBaseImageUrl(string folderName)
        {
            return ApiBaseUrl + folderName;
        }
        private Tuple<int, int> AuthorizeSystemUser
        {
            get
            {

                this.UserId = 0;
                this.RoleId = 0;
                var headers = Request.Headers;
                if (headers.ContainsKey("Authorization"))
                {
                    StringValues tokens;
                    headers.TryGetValue("Authorization", out tokens);

                    var token =  tokens.ToString().Replace("Bearer ","");
                    if (!string.IsNullOrEmpty(token))
                    { 
                       var auth = APIHelper.IsValidToken(token, AppSettings);
                        if (auth.TokenExpired) {
                            //no action if token is expired
                            TokenExpired = true;
                        }
                        else if(auth.User is not null) {
                         this.UserId = auth.User.Id;
                         this.RoleId = int.Parse(auth.User.RoleId,0); 
                      }  
                    }
                }
                return new Tuple<int, int>(UserId, RoleId);
                
            }
        }

     
        public bool IsEnglish
        {
            get
            {
                var headers = Request.Headers;
                if (headers.ContainsKey(Constants.Lang))
                {
                    headers.TryGetValue(Constants.Lang, out StringValues langs);

                    var lang = langs.FirstOrDefault();
                    if (lang.ToUpper().Contains("EN-US") || lang.ToUpper().Contains("EN"))
                    {
                        return true;
                    }
                } else
                {
                    return true;
                }

                return false;
            }
        }
        
        
        [HttpGet, Route("Allowed")]
        public async Task<bool> Allowed(int permissionId=(int)PermissionTypes.None)
        {

            accessResponse = new();
            var auth = AuthorizeSystemUser;
           
            if (permissionId!= (int)PermissionTypes.None) { 

                this.PermissionType = permissionId;
            }
            if (AppSettings.EnableAuthorization)
            {
                if (TokenExpired)
                {
                    accessResponse.Message = "Token is Expired, please login again";
                    accessResponse.Success = false;
                    accessResponse.StatusCode = 250; 
                    return false;
                }
                var allowed = await SystemUserService.Allowed(RoleId, this.PermissionType);
                if (!allowed)
                {
                    accessResponse.Message = "You are not authorized, Access right is denied";
                    accessResponse.Success = false;
                    accessResponse.StatusCode = 401;
                    return false;
                }
            }

            return true;
        }
        #region DataTable
        public DataTableParam GetDataTableParameters
        {
            get
            {
                return new DataTableParam()
                {
                    Draw = Draw,
                    IsEnglish=IsEnglish,
                    SearchValue = SearchValue,
                    SortColumn = SortColumn,
                    SortColumnDirection = SortColumnDirection,
                    Skip = Skip,
                    PageSize = PageSize
                };
            }
        }
        public string Draw
        {
            get
            {
                try
                {
                    return Request.Form["draw"].FirstOrDefault();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public string SortColumn
        {
            get
            {
                try
                {
                    return Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public string SortColumnDirection
        {
            get
            {
                try
                {
                    return Request.Form["order[0][dir]"].FirstOrDefault();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public string SearchValue
        {
            get
            {
                try
                {
                    return Request.Form["search[value]"].FirstOrDefault().ToLower() ?? "";
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        public int PageSize
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
                }
                catch
                {
                    return 0;
                }
            }
        }
        public int Skip
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
                }
                catch
                {
                    return 0;
                }
            }
        }
        #endregion
    }
}
