namespace SmartParking.API.Services.Implementation;

public class NotificationService : INotificationService
{
    public NotificationService()
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("ispot-notification.json")
            });
        }
    }

    public async Task SendNotificationAsync(string token, string title, string body)
    {
        var message = new Message
        {
            Token = token,
            Notification = new Notification
            {
                Title = title,
                Body = body
            }
        };

        await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }
}
