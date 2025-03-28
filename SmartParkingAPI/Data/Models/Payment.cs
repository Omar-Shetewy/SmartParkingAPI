namespace SmartParking.API.Data.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public bool PaymentStatus { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public ReservationRecord ReservationRecord { get; set; }
    }
}
