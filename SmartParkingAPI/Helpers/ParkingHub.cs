namespace SmartParking.API.Helpers;

public class ParkingHub : Hub
{
    public async Task SendSpot(string userId, string spotName)
    {
        await Clients.User(userId).SendAsync("ReceiveSpot", spotName);
    }

    public async Task SendAlert(string userId, string title, string message)
    {
        await Clients.User(userId).SendAsync("SendAlert", title, message);
    }
    public async Task SendEntryCar(CarPositionDTO carPosition)
    {
        await Clients.All.SendAsync("ReceiveEntryCar", carPosition);
    }
    public async Task SendExitCar(int spotid)
    {
        await Clients.All.SendAsync("ReceiveExitCar", spotid);
    }
    public async Task SendAllEntryCars(int entryCars)
    {
        await Clients.All.SendAsync("ReceiveAllEntryCars", entryCars);
    }
}

