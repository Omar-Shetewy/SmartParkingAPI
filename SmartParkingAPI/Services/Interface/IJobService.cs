namespace SmartParking.API.Services.Interface;

public interface IJobService
{
    Task<IEnumerable<Job>> GetAll();
    Task<Job> GetById(int id);
    Task<Job> Add(Job job);
    Job Update(Job job);
    Job Delete(Job job);
    Task<bool> isValidJob(int id);
}
