namespace SmartParking.API.Data.Models;

public class Spot
{
    public int SpotId { get; set; }
    public int SensorId { get; set; }
    public int Floor { get; set; }
    public int Number { get; set; }
    public int Status { get; set; }
    public int GarageId { get; set; }
    [ForeignKey("GarageId")]
    public Garage Garage { get; set; }
    [ForeignKey("SensorId")]
    public Sensor Sensor { get; set; }
}
