namespace Utility.Models.Frontend.CustomerManagement
{
    public class RefreshTokenModel
    {
        public RefreshTokenModel()
        {
            OldToken = string.Empty;
            NewToken = string.Empty;
            Expiration = string.Empty;
        }
        public string OldToken { get; set; }
        public string NewToken { get; set; }
        public string Expiration { get; set; }
    }
}
