namespace SmartParking.API.Services.Interface;

public interface IPaymentMethodService
{
    Task<IEnumerable<PaymentMethod>> GetAll();
    Task<PaymentMethod> GetById(int id);
    Task<PaymentMethod> Add(PaymentMethod paymentMethod);
    PaymentMethod Update(PaymentMethod paymentMethod);
    PaymentMethod Delete(PaymentMethod paymentMethod);
    Task<bool> isValidPaymentMethod(int id);
}
