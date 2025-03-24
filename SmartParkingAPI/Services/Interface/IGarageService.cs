namespace SmartParking.API.Services.Interface;

public interface IGarageService
{
    Task<IEnumerable<Garage>> GetAll();
    Task<Garage> GetBy(int id);
    Task<Garage> GetBy(string name);

    Task<IEnumerable<Spot>> GetAllSpots(int garageId);
    Task<Garage> Add(Garage garage);
    Garage Update(Garage garage);
    Garage Delete(Garage garage);
    Task<bool> isValidGarage(int id);

}
