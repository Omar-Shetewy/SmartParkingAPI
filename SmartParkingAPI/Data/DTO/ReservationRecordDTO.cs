namespace SmartParking.API.Data.DTO
{
    public class ReservationRecordDTO
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public int GarageId { get; set; }
        public int PaymentId { get; set; }
    }
}
