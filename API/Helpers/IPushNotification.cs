namespace API.Helpers
{
    public interface IPushNotification
    {
        bool SendNotification(string title, string body, string deviceToken);
    }
}
