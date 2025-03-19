namespace SmartParking.API.Data.DTO
{
    public class CarDetailsDTO
    {
        public int CarId { get; set; }
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }

        public int UserId { get; set; }
    }
}
