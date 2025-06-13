namespace SmartParking.API.Data.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }  = DateTime.Now.AddMinutes(10);
        public int UserId { get; set; }
        public int GarageId { get; set; }
        public User User { get; set; }
        public Garage Garage { get; set; }
        public Payment Payment { get; set; }
    }
}
