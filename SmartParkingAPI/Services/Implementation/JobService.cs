namespace SmartParking.API.Services.Implementation
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext _dbContext;

        public JobService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Job>> GetAll()
        {
            return await _dbContext.Jobs.ToListAsync();
        }

        public async Task<Job> GetById(int id)
        {
            return await _dbContext.Jobs.FindAsync(id);
        }

        public async Task<Job> Add(Job job)
        {
            await _dbContext.Jobs.AddAsync(job);
            _dbContext.SaveChanges();

            return job;
        }

        public Job Update(Job job)
        {
            _dbContext.Jobs.Update(job);
            _dbContext.SaveChanges();

            return job;
        }

        public Job Delete(Job job)
        {
            _dbContext.Jobs.Remove(job);
            _dbContext.SaveChanges();

            return job;
        }

        public async Task<bool> isValidJob(int id)
        {
            return await _dbContext.Jobs.AnyAsync(j => j.JobId == id);
        }
    }
}
