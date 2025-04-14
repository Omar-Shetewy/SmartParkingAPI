namespace SmartParking.API.Services.Interface
{
    public interface IAuthService
    {
        public Task<User?> AddAsync(RegisterDTO request);
        public Task<AuthResponseDTO?> UserValidationAsync(LoginDTO request);
    }
}
