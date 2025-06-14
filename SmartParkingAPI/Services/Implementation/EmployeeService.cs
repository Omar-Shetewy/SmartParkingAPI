namespace SmartParking.API.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByGarageId(int garageId)
        {
            return await _dbContext.Employees.Where(e => e.GarageId == garageId).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByJobId(int jobId)
        {
            return await _dbContext.Employees.Where(e => e.JobId == jobId).ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task<Employee> Add(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            _dbContext.SaveChanges();

            return employee;
        }

        public Employee Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();

            return employee;
        }

        public Employee Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();

            return employee;
        }
    }
}
