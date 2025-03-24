namespace SmartParking.API.Services.Interface;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role?> GetRoleByIdAsync(int id);
    Task<Role> AddRoleAsync(Role role);
    Role UpdateRole(Role role);
    Role DeleteRole(Role role);
}
