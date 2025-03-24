namespace SmartParking.API.Services
{
    public interface IAuthService
    {
        public Task<User?> AddAsync(RegisterDTO request);
        public Task<string?> UserValidationAsync(LoginDTO request);
    }
}
