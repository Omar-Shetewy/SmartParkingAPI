namespace SmartParking.API.Services.Interface;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAll();
    Task<Reservation> GetById(int id);
    Task<Reservation> GetByUserId(int userId);
    Task<Reservation> Add(Reservation record);
    Reservation Update(Reservation record);
    Reservation Delete(Reservation record);
    Task<bool> isValidReservationRecord(int id);
}
