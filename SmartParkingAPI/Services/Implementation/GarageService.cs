namespace SmartParking.API.Services.Implementation;

public class GarageService : IGarageService
{
    private readonly ApplicationDbContext _dbContext;

    public GarageService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Garage> Add(Garage garage)
    {
        await _dbContext.Garages.AddAsync(garage);
        _dbContext.SaveChanges();
        return garage;
    }


    public async Task<EntryCar> UpdateExitCar(string PlateNumber)
    {
        var entryCar = await _dbContext.EntryCars.FirstOrDefaultAsync(e => e.PlateNumber == PlateNumber);
        if (entryCar != null)
        {
            entryCar.ExitTime = DateTime.Now;
            entryCar.IsPaid = true;
            _dbContext.EntryCars.Update(entryCar);
            await _dbContext.SaveChangesAsync();
        }
        return entryCar;
    }

    public Garage Delete(Garage garage)
    {
        _dbContext.Garages.Remove(garage);
        _dbContext.SaveChanges();
        return garage;
    }

    public async Task<IEnumerable<Garage>> GetAll()
    {
        return await _dbContext.Garages.ToListAsync();
    }

    public async Task<Garage> GetBy(int id)
    {
        return await _dbContext.Garages.FindAsync(id);
    }

    public async Task<Garage> GetBy(string name)
    {
        return await _dbContext.Garages.FirstOrDefaultAsync(g => g.Name == name);
    }

    public Garage Update(Garage garage)
    {
        _dbContext.Update(garage);
        _dbContext.SaveChanges();
        return garage;
    }


    public async Task<bool> isValidGarage(int id)
    {
        return await _dbContext.Garages.AnyAsync(g => g.GarageId == id);
    }

    public async Task<IEnumerable<Spot>> GetAllSpots(int garageId)
    {
        return await _dbContext.Spots.Where(s => s.GarageId == garageId).ToListAsync();
    }

    public async Task<IEnumerable<EntryCar>> GetAllCars(int garageId)
    {
        return await _dbContext.EntryCars.Where(s => s.GarageId == garageId).ToListAsync();
    }

    public async Task<EntryCar> AddEntryCar(EntryCar entryCar)
    {
        await _dbContext.EntryCars.AddAsync(entryCar);
        await _dbContext.SaveChangesAsync();
        return entryCar;
    }

    public async Task<bool> isValidPlateNumber(string plateNumber)
    {
        var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.PlateNumber == plateNumber);
        var EntryCar = await _dbContext.EntryCars.FirstOrDefaultAsync(c => c.PlateNumber == plateNumber );

        if (car == null)

            return false;

        bool Reserve = await _dbContext.ReservationRecords.AnyAsync(r => r.UserId == car.UserId);
        if (Reserve) return false;


        return true;

    }
}