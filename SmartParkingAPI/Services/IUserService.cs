namespace SmartParkingAPI.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetBy(int id);
    Task<User> GetBy(string email);
    Task<User> GetBy(string email, string password);
    Task<User> Add(User user);
}
