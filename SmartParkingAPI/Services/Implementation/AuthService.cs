using SmartParking.API.Data.Models;
using SmartParking.API.Services.Interface;
using System.Security.Cryptography;

namespace SmartParking.API.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepositories _refreshTokenRepositories;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IMapper mapper, IRefreshTokenRepositories refreshTokenRepositories)
        {
            _refreshTokenRepositories = refreshTokenRepositories;
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

        public async Task<AuthResponseDTO?> AuthenticateAsync(LoginDTO request)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed || !user.IsVerified )
            {
                return null;
            }

            var token = GenerateToken(user);
            var RefreshToken = GenerateResfreshToken();

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = RefreshToken,
                CreatedOn = DateTime.UtcNow,
                ExpireOn = DateTime.UtcNow.AddDays(7),
                UserId = user.UserId
            });

            _context.SaveChanges();
            return new AuthResponseDTO { Token = token, RefreshToken = RefreshToken, UserId = user.UserId, IsVerified = user.IsVerified};
        }

        public async Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO token)
        {
            var oldToken = await _refreshTokenRepositories.GetByIdAsync(token);
            if (oldToken == null || !oldToken.IsActive)
                return null;

            oldToken.RevokedOn = DateTime.UtcNow;

            var newToken = new RefreshToken
            {
                Token = GenerateResfreshToken(),
                CreatedOn = DateTime.UtcNow,
                ExpireOn = DateTime.UtcNow.AddDays(7),
                UserId = oldToken.UserId
            };

            oldToken.User.RefreshTokens.Add(newToken);
            await _refreshTokenRepositories.SaveAsync();

            var accessToken = GenerateToken(oldToken.User);
            return new TokenDTO
            {
                Token = accessToken,
                RefreshToken = newToken.Token
            };
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
    }
}
