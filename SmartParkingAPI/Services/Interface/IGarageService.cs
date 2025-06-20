﻿namespace SmartParking.API.Services.Interface;

public interface IGarageService
{
    Task<IEnumerable<Garage>> GetAll();
    Task<Garage> GetBy(int id);
    Task<Garage> GetBy(string name);
    Task<IEnumerable<Spot>> GetAllSpots(int garageId);
    Task<IEnumerable<EntryCar>> GetAllCars(int garageId);
   Task<EntryCar> GetEntrycarBySpotId(int id);
    Task<EntryCar> AddEntryCar(EntryCar entryCar);
    Task<EntryCar> UpdateCarPosition(string PlateNumber,int? spotId);
    Task<EntryCar> UpdateExitCar(int? spotId);
    bool IsAvailableSpots(int GarageId);
    Task<Garage> Add(Garage garage);
    Garage Update(Garage garage);
    Garage Delete(Garage garage);
    Task<bool> isValidGarage(int id);
    Task<int?> GetUserUsingplate(string plateNumber);

}
