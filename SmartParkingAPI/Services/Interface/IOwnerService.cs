namespace SmartParking.API.Services.Interface
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner> GetById(int id);
        Task<Owner> Add(Owner owner);
        Owner Update(Owner owner);
        Owner Delete(Owner owner);
        bool isValidOwner(int id);
    }
}
