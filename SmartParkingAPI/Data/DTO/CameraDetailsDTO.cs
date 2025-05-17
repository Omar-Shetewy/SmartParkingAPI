namespace SmartParking.API.Data.DTO
{
    public class CameraDetailsDTO
    {
        public int CameraId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GarageId { get; set; }
    }
}
