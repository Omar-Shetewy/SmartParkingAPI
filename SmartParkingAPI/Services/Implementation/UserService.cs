using SmartParking.API.Services.Interface;
using SmartParkingAPI.Data.Models;

namespace SmartParking.API.Services.Implementation;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
        _dbContext.SaveChanges();

        return user;
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return  await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByAsync(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetByAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<bool> isValidUserAsync(int id)
    {
        return await _dbContext.Users.AnyAsync(u => u.UserId == id);
    }

    public User Update(User user)
    {
        _dbContext.SaveChanges();

        return user;
    }

    public User UpdatePass(User user, string password)
    {
        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
        {
            return null;
        }

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);

        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
        return user;
    }

    public User Delete(User user)
    {
        _dbContext.Remove(user);
        _dbContext.SaveChanges();

        return user;
    }

}
