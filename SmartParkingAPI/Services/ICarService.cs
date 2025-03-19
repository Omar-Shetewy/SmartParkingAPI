namespace SmartParking.API.Services
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAll();
        Task<IEnumerable<Car>> GetByUserId(int userId);
        Task<Car> GetBy(int id);
        Task<Car> GetBy(string plateNumber);
        Task<Car> Add(Car car);
        Car Update(Car car);
        Car Delete(Car car);
    }
}
