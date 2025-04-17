namespace SmartParking.API.Data.DTO
{
    public class PaymentDetailsDTO
    {
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public bool PaymentStatus { get; set; }

        public int PaymentMethodId { get; set; }
        public int ReservationRecordId { get; set; }
    }
}
