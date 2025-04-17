namespace SmartParking.API.Data.DTO
{
    public class PaymentUpdateDTO
    {
        public double Amount { get; set; }
        public bool PaymentStatus { get; set; }

        public int PaymentMethodId { get; set; }
    }
}
