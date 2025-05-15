using SmartParking.API.Services.Interface;
using System.Security.Cryptography;

namespace SmartParking.API.Services.Implementation
{
    public class AuthServicie : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthServicie(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        /*
         The ? makes the method’s contract explicit: "I might return a User, or I might return null—handle it accordingly." 
         It’s part of C#’s nullable reference types feature (introduced in C# 8.0) to improve code safety by reducing 
         unexpected null reference exceptions.
         */
        public async Task<User?> AddAsync(RegisterDTO request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return null;
            }

            var user = _mapper.Map<User>(request);
            var HashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.RoleId = 1;
            user.PasswordHash = HashedPassword;

            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<AuthResponseDTO?> UserValidationAsync(LoginDTO request)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email);

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var token = GenerateToken(user);

            return new AuthResponseDTO { Token = token, IsVerified = user.IsVerified};
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken TokenDesciptor = new(
                issuer: _configuration.GetValue<string>("JWT:Issuer"),
                audience: _configuration.GetValue<string>("JWT:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(TokenDesciptor);
        }

        private string GenerateResfreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        //private async Task<string>? GenerateAndSaveRefreshTokenAsync()
        //{
        //    var refreshToken = GenerateResfreshToken();
        //    User. = refreshToken;
        //}
    }
}
