﻿namespace SmartParking.API.Services.Interface;

public interface ICarService
{
    Task<IEnumerable<Car>> GetAll();
    Task<Car> GetByUserId(int userId);
    Task<Car> GetBy(int id);
    Task<Car> GetBy(string plateNumber);
    Task<Car> Add(Car car);
    Car Update(Car car);
    Car Delete(Car car);
}
