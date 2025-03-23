using SmartParking.API.Services.Interface;

namespace SmartParking.API.Services.Implementation;

public class CarService : ICarService
{
    private readonly ApplicationDbContext _dbContext;

    public CarService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Car>> GetAll()
    {
        return await _dbContext.Cars.ToListAsync();
    }

    public async Task<IEnumerable<Car>> GetByUserId(int userId)
    {
        return await _dbContext.Cars.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<Car> GetBy(int id)
    {
        return await _dbContext.Cars.FindAsync(id);
    }

    public async Task<Car> GetBy(string plateNumber)
    {
        return await _dbContext.Cars.FirstOrDefaultAsync(c => c.PlateNumber == plateNumber);
    }

    public async Task<Car> Add(Car car)
    {
        await _dbContext.Cars.AddAsync(car);
        _dbContext.SaveChanges();

        return car;
    }

    public Car Update(Car car)
    {
        _dbContext.Cars.Update(car);
        _dbContext.SaveChanges();

        return car;
    }

    public Car Delete(Car car)
    {
        _dbContext.Remove(car);
        _dbContext.SaveChanges();

        return car;
    }
}
