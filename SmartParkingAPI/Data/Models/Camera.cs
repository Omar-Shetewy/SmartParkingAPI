namespace SmartParking.API.Data.Models
{
    public class Camera
    {
        public int CameraId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GarageId { get; set; }
        public Garage Garage { get; set; }
    }
}
