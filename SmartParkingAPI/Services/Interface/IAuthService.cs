namespace SmartParking.API.Services.Interface
{
    public interface IAuthService
    {
        public Task<User?> AddAsync(RegisterDTO request);
        public Task<AuthResponseDTO?> LoginAsync(LoginDTO request);
        Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO token);
    }
}
