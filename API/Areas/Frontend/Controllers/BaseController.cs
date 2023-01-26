using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;

namespace API.Areas.Frontend.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly AppSettingsModel _appSettings;
        private string ApiBaseUrl { get; set; }
        public BaseController(IOptions<AppSettingsModel> options)
        {
            _appSettings = options.Value;
            ApiBaseUrl = _appSettings.APIBaseUrl;
        }
        protected string GetImageUrl(string folderName, string imageName)
        {
            return ApiBaseUrl + folderName + imageName;
        }
        protected string GetBaseImageUrl(string folderName)
        {
            return ApiBaseUrl + folderName;
        }
        public bool isEnglish
        {
            get
            {
                var headers = Request.Headers;
                if (headers.ContainsKey("lang"))
                {
                    StringValues langs;
                    headers.TryGetValue("lang", out langs);

                    var lang = langs.FirstOrDefault();
                    if (lang.ToUpper() == "EN")
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        public DeviceType HeaderDeviceTypeId
        {
            get
            {
                var headers = Request.Headers;
                if (headers.ContainsKey("deviceTypeId"))
                {
                    StringValues deviceTypeIds;
                    headers.TryGetValue("deviceTypeId", out deviceTypeIds);

                    var deviceTypeId = deviceTypeIds.FirstOrDefault();

                    Enum.TryParse(deviceTypeId, out DeviceType typeId);
                    return typeId;
                }

                return 0;
            }
        }
        public string ServiceAPIKey
        {
            get
            {
                var headers = Request.Headers;
                if (headers.ContainsKey("serviceAPIKey"))
                {
                    StringValues serviceAPIKeys;
                    headers.TryGetValue("serviceAPIKey", out serviceAPIKeys);

                    var serviceAPIKey = serviceAPIKeys.FirstOrDefault();
                    return serviceAPIKey;
                }

                return string.Empty;
            }
        }
        public int LoggedInCustomerId
        {
            get
            {
                try
                {
                    var headers = Request.Headers;
                    if (headers.ContainsKey("Authorization"))
                    {
                        StringValues tokens;
                        headers.TryGetValue("Authorization", out tokens);

                        var token = tokens.FirstOrDefault();
                        if (!string.IsNullOrEmpty(token))
                        {
                            var arrToken = token.Split(' ');
                            if (arrToken.Length == 2)
                            {
                                var tokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateAudience = false,
                                    ValidateIssuer = false,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.APIKey)),
                                    ValidateLifetime = false
                                };

                                var tokenHandler = new JwtSecurityTokenHandler();
                                SecurityToken securityToken;
                                var principal = tokenHandler.ValidateToken(arrToken[1], tokenValidationParameters, out securityToken);
                                var jwtSecurityToken = securityToken as JwtSecurityToken;
                                if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                                    throw new SecurityTokenException("Invalid token");

                                var encryptedCustomerId = principal.Claims.FirstOrDefault(c => c.Type == Constants.ClaimTypeId)?.Value;
                                if (string.IsNullOrWhiteSpace(encryptedCustomerId))
                                {
                                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                    return 0;
                                }

                                string customerId = Cryptography.Decrypt(encryptedCustomerId);
                                if (string.IsNullOrEmpty(customerId))
                                {
                                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                    return 0;
                                }

                                int.TryParse(customerId, out int id);
                                return id;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                return 0;
            }
        }
    }
}
