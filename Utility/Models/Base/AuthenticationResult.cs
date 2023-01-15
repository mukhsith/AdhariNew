using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;

namespace Utility.Models.Base
{
    public class AuthenticationResult
    {
        public ClaimsPrincipal Principal { get; set; } = new();
        /// <summary>
        /// token is expired or empty
        /// </summary>
        public bool TokenExpired { get; set; }
        public string ErrorMessage { get; set; }
        public SecurityTokenException TokenException { get; set; } = new();

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        // Expiry is there to make the client easier to know whether the access token has expired or not. The same info is in AccessToken.

        /// <summary>
        /// Expire date time for AccessToken.
        /// </summary>
        public DateTime Expiry { get; set; }
        public bool Success { get; set; }
        public string RoleName { get; set; }
        public string FullName { get; set; }

        public UserModel User { get; set; } = new();
    }
}