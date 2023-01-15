using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utility.Helpers
{
    public static class Constants
    {
        /// <summary>
        /// fixed Role ID system-wise for internal system root only
        /// </summary>
        public readonly static int ROOT_ROLE_ID = 1;
        public readonly static int DRIVER_ROLE_ID = 4;
        /// <summary>
        /// This permission can only be visible to internal system root user
        /// </summary>
        public readonly static int ROOT_PERMISSION_ID = 5;

        public static int InvalidLoginAttemptsCount { get; set; } = 0;
        public const int MaxLoginAttempts = 3;
        public const string AlphaChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string AlphaNumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public const string Lang = "lang";
         
        public const string ClaimTypeUserId = "Id";
        public const string ClaimTypeGuid = "Guid";
        public const string ClaimTypeFullName = "FullName";
        public const string ClaimTypeRoleId = "RoleId";
        public const string ClaimTypeRoleName = "RoleName";
        public const string ClaimAuthenticationToken = "AuthenticationToken"; //this name should be same in "auth-helper.js" file
                                                                              //public const string ClaimTypeName = "Name";

        //frontend website
        public const string ClaimTypeId = "Id";
        public const string ClaimTypeName = "Name";
        public const string ClaimTypeEmailAddress = "EmailAddress"; //USER ID
        public const string ClaimTypeMobileNumber = "MobileNumber";
        //public const string ClaimTypeActive = "Active";
        //public const string ClaimTokenType = "Bearer"; 

        public const int NameDataSize = 200;
        public const int TitleDataSize = 200;
        public const int ImageNameDataSize = 50;
        public const int MobileDataSize = 20;
        public const int EmailDataSize = 200;
        public const int ShortDataSize = 10;
        public const int SmallDataSize = 50;
        public const int MediumDataSize = 100;
        public const int ExtraMediumDataSize = 150;
        public const int LargeDataSize = 200;
        public const int ExtraLargeDataSize = 400;
        public const int ExtraLargeDescriptionSize = 500;
        public const string AmountDataType = "decimal(18, 3)";
        public const string PercentageDataType = "decimal(18, 2)";

        /// <summary>
        /// Redirect to Account Controller
        /// </summary>
        public const string ControllerAccount = "/Account";
        /// <summary>
        /// Redirect to Home Controller
        /// </summary>
        public const string ControllerHome = "/Home";

        /// <summary>
        /// Controller Method Logout
        /// </summary>
        public const string ActionLogout = "Logout";

    }
}
