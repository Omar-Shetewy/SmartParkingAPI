namespace SmartParking.API.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByAsync(string email);
    Task<User> GetByAsync(int id);
    User Update(User user);
    User Delete(User user);
    Task<bool> isValidUserAsync(int userId);
}
