namespace SmartParking.API.Data.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }
        public List<Payment> Payment { get; set; }
    }
}
