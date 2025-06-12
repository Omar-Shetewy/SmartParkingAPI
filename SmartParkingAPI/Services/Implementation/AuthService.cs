using SmartParking.API.Services.Interface;
using System.Security.Cryptography;

namespace SmartParking.API.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenRepositories _refreshTokenRepositories;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, IMapper mapper, IUserService userServise, IRefreshTokenRepositories refreshTokenRepositories)
        {
            _refreshTokenRepositories = refreshTokenRepositories;
            //_userRepository = user;
            _configuration = configuration;
            _userService = userServise;
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

        public async Task<AuthResponseDTO?> LoginAsync(LoginDTO request)
        {
            var user = await _context.Users.Include(u => u.Role).Include(u => u.RefreshToken).FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed || !user.IsVerified )
            {
                return null;
            }

            var token = GenerateToken(user);
            var refreshToken = GenerateResfreshToken();
            if (user.RefreshToken == null)
            {
                _context.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    CreatedOn = DateTime.UtcNow,
                    ExpireOn = DateTime.UtcNow.AddDays(7),
                    UserId = user.UserId
                });
            }
            else
            {
                var refrshTokenDTO = new RefreshTokenDTO
                {
                    Id = user.UserId,
                    Token = user.RefreshToken.Token
                };

                var refreshDTO = await RefreshTokenAsync(refrshTokenDTO);
            }

                _context.SaveChanges();
            return new AuthResponseDTO { Token = token, RefreshToken = user.RefreshToken.Token, UserId = user.UserId, IsVerified = user.IsVerified};
        }


        public async Task LogoutAsync(int id)
        {
            var user = _context.Users.FindAsync(id);

            user.Result.RefreshToken.RevokedOn = DateTime.UtcNow;

            _context.Users.Update(user.Result);
        }

        public async Task<TokenDTO> RefreshTokenAsync(RefreshTokenDTO token)
        {
            var newToken = await _refreshTokenRepositories.GetByIdAsync(token.Id);

            newToken.Token = GenerateResfreshToken();
            newToken.CreatedOn = DateTime.UtcNow;
            newToken.ExpireOn = DateTime.UtcNow.AddDays(7);

            await _refreshTokenRepositories.SaveAsync();

            var accessToken = GenerateToken(newToken.User);
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
                new Claim(ClaimTypes.Role, user.Role.RoleName) // Think about struct
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken TokenDesciptor = new(
                issuer: _configuration.GetValue<string>("JWT:Issuer"),
                audience: _configuration.GetValue<string>("JWT:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(10),
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
