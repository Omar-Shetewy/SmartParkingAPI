namespace SmartParking.API.Services.Interface;

public interface INotificationService
{
    Task SendNotificationAsync(string token, string title, string body);
}
