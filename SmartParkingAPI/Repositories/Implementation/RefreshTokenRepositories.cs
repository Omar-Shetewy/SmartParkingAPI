namespace SmartParking.API.Repositories.Implementation;

public class RefreshTokenRepositories : IRefreshTokenRepositories
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepositories(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByIdAsync(RefreshTokenDTO token)
    {
        return await _context.RefreshTokens.Include(t => t.User).ThenInclude(r => r.Role).FirstOrDefaultAsync(t => t.UserId == token.Id);
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
