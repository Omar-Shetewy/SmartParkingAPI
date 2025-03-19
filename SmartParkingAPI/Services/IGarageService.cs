﻿namespace SmartParking.API.Services;

public interface IGarageService
{
    Task<IEnumerable<Garage>> GetAll();
    Task<Garage> GetBy(int id);
    Task<Garage> GetBy(string name);
    Task<Garage> Add(Garage garage);
    Garage Update(Garage garage);
    Garage Delete(Garage garage);
}
