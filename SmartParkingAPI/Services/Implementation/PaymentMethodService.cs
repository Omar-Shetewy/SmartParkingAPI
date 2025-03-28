
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace SmartParking.API.Services.Implementation
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentMethodService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAll()
        {
            return await _dbContext.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod> GetById(int id)
        {
            return await _dbContext.PaymentMethods.FindAsync(id);
        }

        public async Task<PaymentMethod> Add(PaymentMethod paymentMethod)
        {
            await _dbContext.PaymentMethods.AddAsync(paymentMethod);
            _dbContext.SaveChanges();

            return paymentMethod;
        }

        public PaymentMethod Update(PaymentMethod paymentMethod)
        {
            _dbContext.PaymentMethods.Update(paymentMethod);
            _dbContext.SaveChanges();

            return paymentMethod;
        }

        public PaymentMethod Delete(PaymentMethod paymentMethod)
        {
            _dbContext.PaymentMethods.Remove(paymentMethod);
            _dbContext.SaveChanges();

            return paymentMethod;
        }

        public async Task<bool> isValidPaymentMethod(int id)
        {
            return await _dbContext.PaymentMethods.AnyAsync(m => m.PaymentMethodId == id);
        }
    }
}
