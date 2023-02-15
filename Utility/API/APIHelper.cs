using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Base;

namespace Utility.API
{
    /// <summary>
    /// API Helper
    /// </summary>
    public partial class APIHelper : IAPIHelper
    {
        private readonly AppSettingsModel _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string lang;

        // Key: userName; Guid: refresh token value.
        // Recommend to persistent this alone with the user records.
        // Based on the scenario, you might have 1 user, 1 refresh token or 1 user, multiple refresh tokens.
        static readonly ConcurrentDictionary<string, Guid> _refreshToken = new ConcurrentDictionary<string, Guid>();

        public APIHelper(IOptions<AppSettingsModel> options, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
            lang = _appSettings.DefaultLang;
            if (!string.IsNullOrEmpty(System.Globalization.CultureInfo.CurrentCulture.Name))
            {
                lang = System.Globalization.CultureInfo.CurrentCulture.Name.ToUpper();
            }
        }
        private string GetContentType(PostContentType postContentType)
        {
            var contentType = "application/json";
            if (PostContentType.applicationJson == postContentType)
            {
                contentType = "application/json";
            }

            return contentType;
        }
        /// <summary>
        /// Post async      
        /// </summary>
        /// <param name="url">API url</param>
        /// <param name="requestObj">Request object</param>
        public virtual async Task<T> PostAsync<T>(string url, object requestObj, PostContentType postContentType)
        {
            T responseModel = default;
            using (var httpClient = new HttpClient())
            {
                AppendHeader(httpClient);
                var contentType = GetContentType(postContentType);

                // JsonContent content = JsonContent.Create(requestObj);
                StringContent content = new(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, contentType);
                //var myContent = JsonConvert.SerializeObject(requestObj);
                //HttpContent httpContent = new(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
                using var response = await httpClient.PostAsync(_appSettings.APIBaseUrl + url, content);
                if (response.StatusCode.ToString() == "OK")
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    string apiResponse = "{'success':false,'message':'Unauthorized','messageCode':401}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "BadRequest")
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':400}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "InternalServerError")
                {
                    string apiResponse = "{'success':false,'message':'InternalServerError','messageCode':500}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':0}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }

            return responseModel;
        }

        /// <summary>
        /// Post async      
        /// </summary>
        /// <param name="url">API url</param>
        /// <param name="requestObj">Request object</param>
        public virtual async Task<T> PostAsync<T>(string url, object requestObj, string baseUrl = "")
        {
            T responseModel = default;
            using (var httpClient = new HttpClient())
            {
                AppendHeader(httpClient);

                StringContent content = new(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");

                string completeUrl = string.Empty;
                if (!string.IsNullOrEmpty(baseUrl))
                    completeUrl = baseUrl + url;
                else
                    completeUrl = _appSettings.APIBaseUrl + url;

                using var response = await httpClient.PostAsync(completeUrl, content);
                if (response.StatusCode.ToString() == "OK")
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    string apiResponse = "{'success':false,'message':'Unauthorized','messageCode':401}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "BadRequest")
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':400}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "InternalServerError")
                {
                    string apiResponse = "{'success':false,'message':'InternalServerError','messageCode':500}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':0}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }

            return responseModel;
        }
        
        /// <summary>
        /// Put async      
        /// </summary>
        /// <param name="url">API url</param>
        /// <param name="requestObj">Request object</param>
        public virtual async Task<T> PutAsync<T>(string url, object requestObj, string baseUrl = "")
        {
            T responseModel = default;
            using (var httpClient = new HttpClient())
            {
                AppendHeader(httpClient);
                StringContent content = new(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");

                string completeUrl = string.Empty;
                if (!string.IsNullOrEmpty(baseUrl))
                    completeUrl = baseUrl + url;
                else
                    completeUrl = _appSettings.APIBaseUrl + url;

                using var response = await httpClient.PutAsync(completeUrl, content);
                if (response.StatusCode.ToString() == "OK")
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    string apiResponse = "{'success':false,'message':'Unauthorized','messageCode':401}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "BadRequest")
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':400}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "InternalServerError")
                {
                    string apiResponse = "{'success':false,'message':'InternalServerError','messageCode':500}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':0}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }

            return responseModel;
        }

        /// <summary>
        /// Delete async      
        /// </summary>
        /// <param name="url">API url</param>
        public virtual async Task<T> DeleteAsync<T>(string url, string baseUrl = "")
        {
            T responseModel = default;
            using (var httpClient = new HttpClient())
            {
                AppendHeader(httpClient);

                string completeUrl = string.Empty;
                if (!string.IsNullOrEmpty(baseUrl))
                    completeUrl = baseUrl + url;
                else
                    completeUrl = _appSettings.APIBaseUrl + url;

                using var response = await httpClient.DeleteAsync(completeUrl);
                if (response.StatusCode.ToString() == "OK")
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    string apiResponse = "{'success':false,'message':'Unauthorized','messageCode':401}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "BadRequest")
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':400}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "InternalServerError")
                {
                    string apiResponse = "{'success':false,'message':'InternalServerError','messageCode':500}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':0}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }

            return responseModel;
        }

        /// <summary>
        /// Get async      
        /// </summary>
        /// <param name="url">API url</param>
        public virtual async Task<T> GetAsync<T>(string url, string baseUrl = "")
        {
            T responseModel = default;
            using (var httpClient = new HttpClient())
            {
                AppendHeader(httpClient);

                string completeUrl = string.Empty;
                if (!string.IsNullOrEmpty(baseUrl))
                    completeUrl = baseUrl + url;
                else
                    completeUrl = _appSettings.APIBaseUrl + url;

                using var response = await httpClient.GetAsync(completeUrl);
                if (response.StatusCode.ToString() == "OK")
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "Unauthorized")
                {
                    string apiResponse = "{'success':false,'message':'Unauthorized','messageCode':401}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "BadRequest")
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':400}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else if (response.StatusCode.ToString() == "InternalServerError")
                {
                    string apiResponse = "{'success':false,'message':'InternalServerError','messageCode':500}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else
                {
                    string apiResponse = "{'success':false,'message':'BadRequest','messageCode':0}";
                    responseModel = JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }

            return responseModel;
        }

        /// <summary>
        /// Append headers
        /// </summary>
        /// <param name="httpClient">HttpClient object</param>
        public HttpClient AppendHeader(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("key", _appSettings.APIKey);
            httpClient.DefaultRequestHeaders.Add("lang", lang);
            httpClient.DefaultRequestHeaders.Add("user-agent", "web-admin");
            httpClient.DefaultRequestHeaders.Add("PaymentAccessToken", _appSettings.PaymentAPIAccessToken);
            httpClient.DefaultRequestHeaders.Add("deviceTypeId", ((int)DeviceType.Web).ToString());

            var token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            return httpClient;
        }

        /// <summary>
        /// Encrypt JWT token
        /// If someone tries to view the JWT without validating/decrypting the token,
        /// then no claims are retrieved and the token is safe guarded.
        /// </summary>
        /// <param name="user">user info</param>
        /// <param name="_appSettings">appsetting.json</param>
        /// <returns></returns>
        public static AuthenticationResult GetJwtToken(UserModel user, AppSettingsModel _appSettings)
        {
            AuthenticationResult auth = new();
            try
            {

                var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_appSettings.APIKey));
                // minimum Sixteen Characters
                var securityKey1 = new SymmetricSecurityKey(Encoding.Default.GetBytes(_appSettings.SecurityKey));

                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _appSettings.Audience),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Role,user.RoleName.ToString()),
                    new Claim(JwtRegisteredClaimNames.NameId, user.GUID.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FullName.ToString()),
                    new Claim(Constants.ClaimTypeUserId, user.Id.ToString()),
                    new Claim(Constants.ClaimTypeGuid, user.GUID.ToString()),
                    new Claim(Constants.ClaimTypeFullName, user.FullName.ToString()),
                    new Claim(Constants.ClaimTypeRoleId, user.RoleId.ToString()),
                    new Claim(Constants.ClaimTypeRoleName, user.RoleName.ToString()),
                };

                var TokenEncryption = new EncryptingCredentials(securityKey1, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

                var handler = new JwtSecurityTokenHandler();

                var jwtSecurityToken = handler.CreateJwtSecurityToken(
                    issuer: _appSettings.Issuer,
                    audience: _appSettings.Issuer,
                    subject: new ClaimsIdentity(claims),
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMonths(2),
                    issuedAt: DateTime.Now,
                    signingCredentials: signingCredentials,
                    encryptingCredentials: TokenEncryption);

                DateTime expiry = DateTime.Now.AddMonths(2);
                //string tokenString = handler.WriteToken(jwtSecurityToken);
                //return tokenString;

                auth.AccessToken = handler.WriteToken(jwtSecurityToken);
                auth.RefreshToken = GenerateRefreshToken(user.FullName);
                auth.Expiry = expiry;
                auth.FullName = user.FullName;
                auth.RoleName = user.RoleName;
                auth.Success = true;

            }
            catch (Exception exp)
            {
                auth.ErrorMessage = exp.Message;
            }
            // var jwt = new JwtSecurityToken(tokenString);
            //  return jwt.ToString();
            //return   ( new { Token = tokenString } );
            return auth;
        }

        private static string GenerateRefreshToken(string userName)
        {
            Guid newRefreshToken = _refreshToken.AddOrUpdate(userName, (u) => Guid.NewGuid(), (k, old) => Guid.NewGuid());
            return newRefreshToken.ToString("D");
        }


        /// <summary>
        /// This is the input JWT which we want to validate.  
        /// If we retrieve the token without decrypting the claims, we won't get any claims,
        ///  DO not use this jwt variable var jwt = new JwtSecurityToken(token);
        /// </summary>
        /// <param name="token">encrypted token</param>
        /// <param name="_appSettings">appsetting.json for property value {APIKey,Issuer,SecurityKey}</param>
        /// <returns></returns>
        public static AuthenticationResult IsValidToken(string token, AppSettingsModel _appSettings)
        {
            AuthenticationResult authenticationResult = new();
            //return if token is empty
            if (string.IsNullOrEmpty(token))
            {
                authenticationResult.TokenExpired = true;
                return authenticationResult;
            }
            var apiKey = _appSettings.APIKey;
            var Issuer = _appSettings.Issuer;
            var SecurityKey = _appSettings.SecurityKey;

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(apiKey));
            //security key to decrypt token
            var securityKey1 = new SymmetricSecurityKey(Encoding.Default.GetBytes(SecurityKey));

            // Validation parameters
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = Issuer.ToString(),
                ValidAudience = Issuer.ToString(),
                IssuerSigningKey = securityKey,
                // This is the decryption key
                TokenDecryptionKey = securityKey1
            };

            //token handler to validate token
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                //if token validation is success, token claims can be reterived
                authenticationResult.Principal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                //token decryption key not matched
                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.Aes128KW, StringComparison.InvariantCultureIgnoreCase) ||
                !jwtSecurityToken.Header.Enc.Equals(SecurityAlgorithms.Aes128CbcHmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    //if token is expired
                    authenticationResult.TokenExpired = true;
                    authenticationResult.ErrorMessage = "Invalid Token";
                    authenticationResult.TokenException = new SecurityTokenException("Invalid token");
                    return authenticationResult;
                }

                //retrieve token user info
                if (authenticationResult.Principal.Claims.ToList().FirstOrDefault(c => c.Type == Constants.ClaimTypeRoleId) != null)
                {
                    authenticationResult.User.Id = int.Parse(authenticationResult.Principal.FindFirst(Constants.ClaimTypeUserId)?.Value, 0);
                    authenticationResult.User.GUID = authenticationResult.Principal.FindFirst(Constants.ClaimTypeGuid)?.Value;
                    authenticationResult.User.FullName = authenticationResult.Principal.FindFirst(Constants.ClaimTypeFullName)?.Value;
                    authenticationResult.User.RoleId = authenticationResult.Principal.FindFirst(Constants.ClaimTypeRoleId)?.Value;
                    authenticationResult.User.RoleName = authenticationResult.Principal.FindFirst(Constants.ClaimTypeRoleName)?.Value;
                }

            }
            catch (SecurityTokenExpiredException exp)
            {

                authenticationResult.TokenExpired = true;
                authenticationResult.ErrorMessage = "Security token is expired";
                authenticationResult.TokenException = exp;
            }
            catch (SecurityTokenException exp)
            {
                authenticationResult.TokenExpired = true;
                authenticationResult.ErrorMessage = "Security token exception is raised";
                authenticationResult.TokenException = exp;
            }

            return authenticationResult;
        }
        public virtual string GetUserIP()
        {
            var result = string.Empty;
            try
            {
                //first try to get IP address from the forwarded header
                if (_httpContextAccessor.HttpContext.Request.Headers != null)
                {
                    var forwardedHttpHeaderKey = "X-FORWARDED-FOR";

                    var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
                        result = forwardedHeader.FirstOrDefault();
                }

                //if this header not exists try get connection remote IP address
                if (string.IsNullOrEmpty(result) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                    result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch
            {
                return string.Empty;
            }

            return result;
        }
    }
}
