namespace SmartParking.API.Data.Models;

public class Car
{
    public int CarId { get; set; }
    public string PlateNumber { get; set; }
    public string Model { get; set; }
    public string Type { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    //public int SpotId { get; set; }
    //public Spot Spot { get; set; }
}
