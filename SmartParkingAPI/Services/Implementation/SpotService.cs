
namespace SmartParking.API.Services.Implementation;

public class SpotService : ISpotService
{
    private readonly ApplicationDbContext _dbContext;

    public SpotService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Spot> Add(Spot spot)
    {
        await _dbContext.Spots.AddAsync(spot);
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == spot.GarageId).TotalSpots++;
        _dbContext.Garages.FirstOrDefault(g => g.GarageId == spot.GarageId).AvailableSpots++;
        _dbContext.SaveChanges();
        return spot;
    }

    public async Task<IEnumerable<Spot>> GetAll()
    {
        return await _dbContext.Spots.ToListAsync();

    }
    public async Task<Spot> GetById(int id)
    {
        return await _dbContext.Spots.FindAsync(id);
    }

    public async Task<IEnumerable<Spot>> GetByGarageId(int garageId)
    {
        return await _dbContext.Spots.Where(s => s.GarageId == garageId).ToListAsync();
    }

}


