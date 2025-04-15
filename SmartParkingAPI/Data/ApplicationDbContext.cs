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

        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = 1,
            RoleName = "User"
        },
        new Role
        {
            Id = 2,
            RoleName = "Admin"
        });
        modelBuilder.Entity<User>()
            .HasMany(u => u.VerificationCodes)
            .WithOne(vc => vc.User)
            .HasForeignKey(vc => vc.UserId);
    }

    public DbSet<Spot> Spots { get; set; }
    public DbSet<Garage> Garages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<ReservationRecord> ReservationRecords { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<UserVerificationCode> UserVerificationCodes { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Employee> Employees { get; set; }

    //public DbSet<PlateRecord> PlateRecords { get; set; }
}
