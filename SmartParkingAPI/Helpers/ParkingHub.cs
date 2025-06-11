namespace SmartParking.API.Helpers;

public class ParkingHub : Hub
{
    public async Task SendSpot(string userId, string spotNumber)
    {
        await Clients.User(userId).SendAsync("ReceiveSpot", spotNumber);
    }
}

