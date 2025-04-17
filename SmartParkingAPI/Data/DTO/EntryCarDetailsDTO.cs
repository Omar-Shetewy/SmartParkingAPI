namespace SmartParking.API.Data.DTO;

public class EntryCarDetailsDTO
{
    public string PlateNumber { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime ExitTime { get; set; }
    public bool IsPaid { get; set; }
    public bool IsActive { get; set; }
    public int GarageId { get; set; }
    public int? SpotId { get; set; }
}
