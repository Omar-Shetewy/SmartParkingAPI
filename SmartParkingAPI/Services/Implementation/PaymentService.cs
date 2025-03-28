namespace SmartParking.API.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            return await _dbContext.Payments.ToListAsync(); 
        }

        public async Task<IEnumerable<Payment>> GetByPaymentMethodId(int paymentMethodId)
        {
            return await _dbContext.Payments.Where(p => p.PaymentMethodId == paymentMethodId).ToListAsync();
        }

        public async Task<Payment> GetById(int id)
        {
            return await _dbContext.Payments.FindAsync(id);
        }

        public async Task<Payment> Add(Payment payment)
        {
            await _dbContext.Payments.AddAsync(payment);
            _dbContext.SaveChanges();

            return payment;
        }

        public Payment Update(Payment payment)
        {
            _dbContext.Payments.Update(payment);
            _dbContext.SaveChanges();

            return payment;
        }

        public Payment Delete(Payment payment)
        {
            _dbContext.Payments.Remove(payment);
            _dbContext.SaveChanges();

            return payment;
        }

        public async Task<bool> isValidPayment(int id)
        {
            return await _dbContext.Payments.AnyAsync(p => p.PaymentId == id);
        }
    }
}
