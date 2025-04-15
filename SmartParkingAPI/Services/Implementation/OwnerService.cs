
namespace SmartParking.API.Services.Implementation
{
    public class OwnerService : IOwnerService
    {
        private readonly ApplicationDbContext _dbContext;

        public OwnerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _dbContext.Owners.ToListAsync();
        }

        public async Task<Owner> GetById(int id)
        {
            return await _dbContext.Owners.FindAsync(id);
        }

        public async Task<Owner> Add(Owner owner)
        {
            await _dbContext.Owners.AddAsync(owner);
            _dbContext.SaveChanges();

            return owner;
        }

        public Owner Update(Owner owner)
        {
            _dbContext.Owners.Update(owner);
            _dbContext.SaveChanges();

            return owner;
        }

        public Owner Delete(Owner owner)
        {
            _dbContext.Owners.Remove(owner);
            _dbContext.SaveChanges();

            return owner;
        }

        public bool isValidOwner(int id)
        {
            return _dbContext.Owners.Any(o => o.OwnerId == id);
        }
    }
}
