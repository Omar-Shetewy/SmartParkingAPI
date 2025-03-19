﻿namespace SmartParking.API.Services;

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
}
