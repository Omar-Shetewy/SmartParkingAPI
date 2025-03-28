namespace SmartParking.API.Services.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAll();
        Task<IEnumerable<Payment>> GetByPaymentMethodId(int paymentMethodId);
        Task<Payment> GetById(int id);
        Task<Payment> Add(Payment payment);
        Payment Update(Payment payment);
        Payment Delete(Payment payment);
        Task<bool> isValidPayment(int id);
    }
}
