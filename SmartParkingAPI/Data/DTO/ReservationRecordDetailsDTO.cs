namespace SmartParking.API.Data.DTO
{
    public class ReservationRecordDetailsDTO
    {
        public int ReservationRecordId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int GarageId { get; set; }
    }
}
