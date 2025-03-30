namespace SmartParking.API.Data.Models;

public class PlateRecord
{
    public int Id { get; set; }
    public string PlateNumber { get; set; }
    public byte[] Image { get; set; }
}

