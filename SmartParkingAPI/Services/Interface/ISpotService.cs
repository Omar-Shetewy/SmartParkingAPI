namespace SmartParking.API.Services.Interface;

public interface ISpotService
{
    Task<IEnumerable<Spot>> GetAll();
    Task<Spot> GetById(int id);
    Task<IEnumerable<Spot>> GetByGarageId(int garageId);
    Task<Spot> Add(Spot spot);
}
