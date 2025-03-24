using Microsoft.Extensions.Options;

namespace SmartParkingAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{

    //    optionsBuilder.UseSqlite("Data Source=SmartParkingDB.db");

    //    base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Spot>()
            .HasOne(s => s.Garage)
            .WithMany(g => g.Spots)
            .HasForeignKey(s => s.GarageId);
    }

    public DbSet<Spot> Spots { get; set; }
    public DbSet<Garage> Garages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<ReservationRecord> ReservationRecords { get; set; }
}
