namespace Utility.Models.Admin.CustomerManagement
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            Name = string.Empty;
            MobileNumber = string.Empty;
            EmailAddress = string.Empty;
            Token = string.Empty;
            Expiration = string.Empty;
            ReturnUrl = string.Empty;
        }
        public int Id { get; set; }
        public string CustomerGuidValue { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string FormattedMobile { get; set; }
        public string EmailAddress { get; set; }
        public int OTPDetailId { get; set; }
        public string OTP { get; set; }
        public double MillisecondsForExpiry { get; set; }
        public string Token { get; set; }
        public string Expiration { get; set; }
        public string ReturnUrl { get; set; }
        public bool Active { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public bool Guest { get; set; }
    }
}
