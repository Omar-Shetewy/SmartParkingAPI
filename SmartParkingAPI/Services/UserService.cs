using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartParkingAPI.Data;
using SmartParkingAPI.Data.Models;

namespace SmartParkingAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Add(User user)
        {
            _dbContext.Users.Add(user);
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
    }
}
