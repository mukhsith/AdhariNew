using System.ComponentModel.DataAnnotations;

namespace Utility.API
{
    public class LoginModel

    {
        [Required(ErrorMessage = "Please enter Name")]
        public string FullName { get; set; }
        //[Required(ErrorMessage = "Please enter Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
        public string Latitude { get; set; }
        public string Longitude {get;set;}
        public bool RememberMe { get; set; }
        public bool IsLoginSucceeded { get; set; }
    }
}
