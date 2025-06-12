namespace SmartParking.API.Services.Interface
{
    public interface IAuthService
    {
        public Task<User?> AddAsync(RegisterDTO request);
        public Task<AuthResponseDTO?> LoginAsync(LoginDTO request);
        public Task LogoutAsync(int id);
        Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO token);
    }
}
