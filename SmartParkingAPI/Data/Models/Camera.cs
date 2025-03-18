namespace SmartParking.API.Data.Models;

public class Camera
{
    public Guid CameraId { get; set; }
    public string CameraName { get; set; }
    public string CameraLocation { get; set; }

    public int GarageId { get; set; }
    [ForeignKey("GarageId")]
    public Garage Garage { get; set; }
}
