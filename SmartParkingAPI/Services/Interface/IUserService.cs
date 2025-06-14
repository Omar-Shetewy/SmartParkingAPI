namespace SmartParking.API.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByAsync(string email);
    Task<User> GetByAsync(int id);
    Task<bool> isValidEmailAsync(string email);
    User Update(User user);
    User UpdatePass(User user, string password);
    User Delete(User user);
    Task<bool> isValidUserAsync(int userId);
}
