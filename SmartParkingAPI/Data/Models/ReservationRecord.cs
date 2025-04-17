namespace SmartParking.API.Data.Models
{
    public class ReservationRecord
    {
        public int ReservationRecordId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        
        public int GarageId { get; set; }
        public Garage Garage { get; set; }

        public Payment Payment { get; set; }
    }
}
