namespace SmartParking.API.Services.Implementatiglon;

public class CameraService : ICameraService
{
    private readonly ApplicationDbContext _dbContext;

    public CameraService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Camera>> GetAll()
    {
        return await _dbContext.Cameras.ToListAsync();
    }

    public async Task<IEnumerable<Camera>> GetByGarageId(int garageId)
    {
        return await _dbContext.Cameras.Where(c => c.GarageId == garageId).ToListAsync();
    }

    public async Task<Camera> GetBy(int id)
    {
        return await _dbContext.Cameras.FindAsync(id);
    }

    public async Task<Camera> Add(Camera camera)
    {
        await _dbContext.Cameras.AddAsync(camera);
        _dbContext.SaveChanges();

        return camera;
    }
    public Camera Update(Camera camera)
    {
        _dbContext.Cameras.Update(camera);
        _dbContext.SaveChanges();

        return camera;
    }

    public Camera Delete(Camera camera)
    {
        _dbContext.Remove(camera);
        _dbContext.SaveChanges();

        return camera;
    }
}
