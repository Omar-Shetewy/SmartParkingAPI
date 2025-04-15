namespace SmartParking.API.Services.Interface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<IEnumerable<Employee>> GetByGarageId(int garageId);
        Task<IEnumerable<Employee>> GetByJobId(int jobId);
        Task<Employee> GetById(int id);
        Task<Employee> Add(Employee employee);
        Employee Update(Employee employee);
        Employee Delete(Employee employee);
    }
}
