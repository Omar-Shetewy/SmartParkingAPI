namespace SmartParking.API.Data.DTO
{
    public class GarageDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int TotalSpots { get; set; }
        public int AvailableSpots { get; set; }
        public int ReservedSpots { get; set; }
    }
}
