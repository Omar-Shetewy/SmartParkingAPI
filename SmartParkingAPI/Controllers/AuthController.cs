using SmartParking.API.Data.DTO;
using SmartParking.API.Helpers;

namespace SmartParking.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authServices;
        private readonly IUserService _userService;
        private readonly IEmailServices _emailServices;
        public AuthController(IAuthService authServices, IEmailServices emailServices, IUserService userService)
        {
            _authServices = authServices;
            _emailServices = emailServices;
            _userService = userService;
        }


        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO request)
        {
            var user = await _authServices.AddAsync(request);

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>(ModelState,"", false));

            if (user == null)
                return BadRequest(new ApiResponse<object>(null,"User already exists!",false));

            await _emailServices.SendVerificationCodeAsync(user.UserId);

            RegisterDetailsDTO userData = new RegisterDetailsDTO
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserId = user.UserId
            };

            return Ok(new ApiResponse<RegisterDetailsDTO>(userData, "Verify your email",true));
        }

        [HttpPost("Resend-verification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reverify([FromHeader] int userId)
        {
            var user = await _userService.GetByAsync(userId);
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>(ModelState,"", false));

            if (user == null)
                return BadRequest(new ApiResponse<object>(null, "User not found!", false));

            if (user.IsVerified)
                return BadRequest(new ApiResponse<object>(null, "User already verified", false));

            await _emailServices.SendVerificationCodeAsync(user.UserId);

            return Ok(new ApiResponse<object>(null, "Verification code resent successfully", true));

        }


        [HttpPost("VerifyEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDTO request)
        {
            var user = await _userService.GetByAsync(request.Id);

            if (user.IsVerified)
                return BadRequest(new ApiResponse<object>(null,"User already verified", false));

            var result = await _emailServices.VerifyCodeAsync(request.Id, request.Code);

            if (!result)
                return BadRequest(new ApiResponse<object>(null,"Invalid or expired code", false));

            return Ok(new ApiResponse<object>(null, "Email verified successfully", true));
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogInAsync([FromBody] LoginDTO request)
        {
            var token = await _authServices.UserValidationAsync(request);

            if (token == null)
                return BadRequest(new ApiResponse<object>(null,"Invalid Email or Password!", false));

            if (!token.IsVerified)
                return BadRequest(new ApiResponse<object>(null,"Please verify your email before logging in", false));

            //return Ok(token.Token);
            return Ok(new ApiResponse<string>(token.Token, "user successfully logged in", true));
        }

        [HttpPost("Forget-Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgetPassword(string Email)
        { 
            var user = await _userService.GetByAsync(Email);

            if (user == null)
                return BadRequest(new ApiResponse<object>(null, "User not found!", false));
            
            if (user.IsVerified)
                user.IsVerified = false;

            _userService.Update(user);

            await _emailServices.SendVerificationCodeAsync(user.UserId);

            return Ok(new ApiResponse<object>(null, "Verify Your Account Then Create Your New Password", true));

        }

        [HttpPost("update-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePassword(int id, string password)
        {
            var user = await _userService.GetByAsync(id);
            
            if (user == null)
                return BadRequest(new ApiResponse<object>(null,"User Not Found, Please Register First", false));

            var updated = _userService.UpdatePass(user, password);

            if (updated == null)
                return BadRequest(new ApiResponse<object>(null, "Password is the same as the old one!", false));

            return Ok(new ApiResponse<object>(null, "Password Updated Successfully", true));
        }
    }
}
