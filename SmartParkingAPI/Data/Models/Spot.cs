namespace SmartParking.API.Data.Models;

public class Spot
{
    public int SpotId { get; set; }
    public int Floor { get; set; }
    public string Code { get; set; }
    public int Status { get; set; }
    public int GarageId { get; set; }
    [ForeignKey("GarageId")]
    [JsonIgnore]
    public Garage Garage { get; set; }

}
