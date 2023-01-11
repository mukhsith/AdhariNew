using System.ComponentModel.DataAnnotations;

namespace Utility.Models.Frontend.CustomerManagement
{
    public class VerifyOTPModel
    {
        [Required(ErrorMessage = "Request id is required")]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "OTP is required")]
        public string OTP { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public string CustomerGuidValue { get; set; }
        public int? GuestType { get; set; }
    }
}
