namespace Utility.Models.Frontend.CustomizedModel
{
    public class CustomerRegisterModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
        public int OTPValidMinutes { get; set; }
        public int OTPDetailId { get; set; }
        public bool Guest { get; set; }
    }
}
