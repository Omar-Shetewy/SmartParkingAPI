namespace SmartParking.API.Services.Interface
{
    public interface ICameraService
    {
        Task<IEnumerable<Camera>> GetAll();
        Task<IEnumerable<Camera>> GetByGarageId(int garageId);
        Task<Camera> GetBy(int id);
        Task<Camera> Add(Camera camera);
        Camera Update(Camera camera);
        Camera Delete(Camera camera);
    }
}
