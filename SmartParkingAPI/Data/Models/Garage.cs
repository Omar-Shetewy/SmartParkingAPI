namespace SmartParking.API.Data.Models;

public class Garage
{
    public int GarageId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int TotalSpots { get; set; }
    public int AvailableSpots { get; set; }
    public int ReservedSpots { get; set; } = 0;
    public int IsActive { get; set; } = 1;
    public List<Spot> Spots { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public List<ReservationRecord> ReservationRecords { get; set; }
}
