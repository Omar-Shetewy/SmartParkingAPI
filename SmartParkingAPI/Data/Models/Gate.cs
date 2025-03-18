namespace SmartParking.API.Data.Models;

public class Gate
{
    public int Id { get; set; }
    public string GateType { get; set; }

    public int GarageId { get; set; }
    [ForeignKey("GarageId")]
    public Garage Garage { get; set; }

}
