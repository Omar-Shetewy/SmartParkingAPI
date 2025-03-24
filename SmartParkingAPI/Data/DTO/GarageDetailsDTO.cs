namespace SmartParking.API.Data.DTO;

public class GarageDetailsDTO
{
    public int GarageId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int TotalSpots { get; set; }
    public int AvailableSpots { get; set; }
    public int ReservedSpots { get; set; }

}
