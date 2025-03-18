namespace SmartParking.API.Data.Models;

public class Garage
{
    public int GarageId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int TotalSpots { get; set; }
    public int AvailableSpots { get; set; }
    public int ReservedSpots { get; set; }
    public int IsActive { get; set; }
    public List<Spot>? Spots { get; set; }
    public List<Gate>? Gates { get; set; }
    public List<Camera>? Cameras { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;
}
