namespace SmartParking.API.Services.Implementation;

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Reservation>> GetAll()
    {
        return await _dbContext.ReservationRecords.ToListAsync();
    }

    public async Task<Reservation> GetById(int id)
    {
        return await _dbContext.ReservationRecords.FindAsync(id);
    }

    public async Task<Reservation> GetByUserId(int userId)
    {
        return await _dbContext.ReservationRecords.FirstOrDefaultAsync(r => r.UserId == userId);
    }

    public async Task<Reservation> Add(Reservation record)
    {
        await _dbContext.ReservationRecords.AddAsync(record);
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == record.GarageId).ReservedSpots++;
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == record.GarageId).AvailableSpots--;
        _dbContext.SaveChanges();

        return record;
    }

    public Reservation Update(Reservation record)
    {
        _dbContext.ReservationRecords.Update(record);
        _dbContext.SaveChanges();

        return record;
    }

    public Reservation Delete(Reservation record)
    {
        _dbContext.Remove(record);
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == record.GarageId).ReservedSpots--;
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == record.GarageId).AvailableSpots++;
        _dbContext.SaveChanges();

        return record;
    }

    public async Task<bool> isValidReservationRecord(int id)
    {
        return await _dbContext.ReservationRecords.AnyAsync(r => r.ReservationId == id);
    }
}
