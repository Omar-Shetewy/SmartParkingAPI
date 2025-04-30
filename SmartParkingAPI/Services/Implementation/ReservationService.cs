using SmartParking.API.Services.Interface;

namespace SmartParking.API.Services.Implementation;

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ReservationRecord>> GetAll()
    {
        return await _dbContext.ReservationRecords.ToListAsync();
    }

    public async Task<ReservationRecord> GetById(int id)
    {
        return await _dbContext.ReservationRecords.FindAsync(id);
    }

    public async Task<ReservationRecord> GetByUserId(int userId)
    {
        return await _dbContext.ReservationRecords.FirstOrDefaultAsync(r => r.UserId == userId);
    }

    public async Task<ReservationRecord> Add(ReservationRecord record)
    {
        await _dbContext.ReservationRecords.AddAsync(record);
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == record.GarageId).ReservedSpots++;
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == record.GarageId).AvailableSpots--;
        _dbContext.SaveChanges();

        return record;
    }

    public ReservationRecord Update(ReservationRecord record)
    {
        _dbContext.ReservationRecords.Update(record);
        _dbContext.SaveChanges();

        return record;
    }

    public ReservationRecord Delete(ReservationRecord record)
    {
        _dbContext.Remove(record);
        _dbContext.SaveChanges();

        return record;
    }

    public async Task<bool> isValidReservationRecord(int id)
    {
        return await _dbContext.ReservationRecords.AnyAsync(r => r.ReservationRecordId == id);
    }
}
