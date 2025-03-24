using SmartParking.API.Services.Interface;
using System.Threading.Tasks;

namespace SmartParking.API.Services.Implementation;

public class RoleService : IRoleService
{
    private readonly ApplicationDbContext _dbContext;

    public RoleService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role> AddRoleAsync(Role role)
    {
        await _dbContext.AddAsync(role);
        _dbContext.SaveChanges();
        return role;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        return await _dbContext.Roles.FindAsync(id);
    }

    public Role UpdateRole(Role role)
    {
        _dbContext.Update(role);
        _dbContext.SaveChanges();
        return role;
    }
    public Role DeleteRole(Role role)
    {
        _dbContext.Remove(role);
        _dbContext.SaveChanges();

        return role;
    }
}
