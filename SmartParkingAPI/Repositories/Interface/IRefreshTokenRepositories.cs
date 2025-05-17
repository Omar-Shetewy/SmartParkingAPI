namespace SmartParking.API.Repositories.Interface;

public interface IRefreshTokenRepositories
{

    Task<RefreshToken?> GetByIdAsync(RefreshTokenDTO token);
    Task<RefreshToken?> GetByTokenAsync(RefreshTokenDTO token);
    Task AddAsync(RefreshToken token);
    Task SaveAsync();

}
