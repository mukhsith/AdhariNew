namespace Utility.Models.Frontend.CustomerManagement
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string TimeAgo { get; set; }
        public string FormattedDate { get; set; }
    }
}
