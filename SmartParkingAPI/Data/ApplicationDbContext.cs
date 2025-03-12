using Microsoft.EntityFrameworkCore;
using SmartParkingAPI.Data.Models;

namespace SmartParkingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
