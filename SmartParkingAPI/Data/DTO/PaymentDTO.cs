namespace SmartParking.API.Data.DTO
{
    public class PaymentDTO
    {
        public double Amount { get; set; }
        public bool PaymentStatus { get; set; }

        public int PaymentMethodId { get; set; }
    }
}
