namespace SmartParking.API.Data.DTO;

public class EntryCarDTO
{
    public string PlateNumber { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime ExitTime { get; set; }
    public bool IsActive { get; set; }
    public int GarageId { get; set; }
}
