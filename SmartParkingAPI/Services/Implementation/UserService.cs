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

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> GetBy(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User> GetBy(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User> GetBy(string email, string password)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
    }

    public async Task<bool> isValidUser(int id)
    {
        return await _dbContext.Users.AnyAsync(u => u.UserId == id);
    }
}
