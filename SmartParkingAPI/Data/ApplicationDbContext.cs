namespace SmartParking.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
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

        modelBuilder.Entity<ReservationRecord>()
        .Property(e => e.ReservationRecordId)
        .HasColumnName("ReservationRecordId");

    }

    public DbSet<Spot> Spots { get; set; }
    public DbSet<Garage> Garages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<ReservationRecord> ReservationRecords { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<UserVerificationCode> UserVerificationCodes { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EntryCar> EntryCars { get; set; }
    public DbSet<Camera> Cameras { get; set; }

}
