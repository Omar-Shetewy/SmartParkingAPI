namespace SmartParkingAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Spot>()
            .HasOne(s => s.Garage)
            .WithMany(g => g.Spots)
            .HasForeignKey(s => s.GarageId);

        modelBuilder.Entity<Camera>()
            .HasOne(c => c.Garage)
            .WithMany(g => g.Cameras)
            .HasForeignKey(c => c.GarageId);

        modelBuilder.Entity<Gate>()
            .HasOne(g => g.Garage)
            .WithMany(g => g.Gates)
            .HasForeignKey(g => g.GarageId);

        modelBuilder.Entity<Spot>()
            .HasOne(s => s.Sensor)
            .WithOne()
            .HasForeignKey<Spot>(s => s.SensorId);

    }

    public DbSet<Spot> Spots { get; set; }
    public DbSet<Camera> Cameras { get; set; }
    public DbSet<Gate> Gates { get; set; }
    public DbSet<Garage> Garages { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
}
