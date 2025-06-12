namespace SmartParking.API.Repositories.Interface;

public interface IRefreshTokenRepositories
{

    Task<RefreshToken?> GetByIdAsync(int id);
    Task<RefreshToken?> GetByTokenAsync(RefreshTokenDTO token);
    Task AddAsync(RefreshToken token);
    Task SaveAsync();

}
