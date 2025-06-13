namespace SmartParking.API.Repositories.Implementation;

public class RefreshTokenRepositories : IRefreshTokenRepositories
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepositories(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByIdAsync(int id)
    {
        return await _context.RefreshTokens.Include(t => t.User).ThenInclude(r => r.Role).FirstOrDefaultAsync(i => i.Id == id); // eagerly loaded
    }

    public async Task<RefreshToken?> GetByTokenAsync(RefreshTokenDTO token)
    {
        return await _context.RefreshTokens.Include(t => t.User).ThenInclude(r => r.Role).FirstOrDefaultAsync(t => t.Token == token.Token);
    }

    public async Task AddAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
