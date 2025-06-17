using SmartParking.API.Data.Models;

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

    public bool IsAvailableSpots(int GarageId)
    {
        if (_dbContext.Garages.FirstOrDefault(g => g.GarageId == GarageId).AvailableSpots <= 0)
            return false;

        return true;
    }

    public async Task<EntryCar> UpdateExitCar(string PlateNumber)
    {
        var entryCar = await _dbContext.EntryCars.FirstOrDefaultAsync(e => e.PlateNumber == PlateNumber);
        if (entryCar != null)
        {
            entryCar.ExitTime = DateTime.Now;
            entryCar.IsPaid = true;
            _dbContext.EntryCars.Update(entryCar);
            _dbContext.Garages.FirstOrDefault(g => g.GarageId == entryCar.GarageId).AvailableSpots++;

            await _dbContext.SaveChangesAsync();
        }
        return entryCar;
    }

    public async Task<EntryCar> UpdateCarPosition(string PlateNumber, int? spotId)
    {
        var entryCar = await _dbContext.EntryCars.FirstOrDefaultAsync(e => e.PlateNumber == PlateNumber);
        if (entryCar != null)
        {
            entryCar.SpotId = spotId;

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
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == entryCar.GarageId).AvailableSpots--;

        await _dbContext.SaveChangesAsync();
        return entryCar;
    }

    public async Task<int?> GetUserUsingplate(string plateNumber)
    {
        var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.PlateNumber == plateNumber);

        if (car == null)
            return null;
        var userId = car.UserId;

        var entryCar = await _dbContext.EntryCars.FirstOrDefaultAsync(e => e.PlateNumber == plateNumber);
        if (entryCar != null)
        {
            var Reserve = _dbContext.ReservationRecords.FirstOrDefault(r => r.UserId == userId);
            if (Reserve != null)
            {
                Reserve.EndDate = DateTime.Now;
                _dbContext.ReservationRecords.Update(Reserve);
            }
            entryCar.InApp = true;

            _dbContext.EntryCars.Update(entryCar);
            _dbContext.Garages.FirstOrDefault(g => g.GarageId == entryCar.GarageId).ReservedSpots--;
            _dbContext.Garages.FirstOrDefault(g => g.GarageId == entryCar.GarageId).AvailableSpots++;
            await _dbContext.SaveChangesAsync();
        }

        return userId;

    }
}
