
using SmartParking.API.Services.Interface;

namespace SmartParking.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authServices;
        private readonly IEmailServices _emailServices;
        public AuthController(IAuthService authServices, IEmailServices emailServices)
        {
            _authServices = authServices;
            _emailServices = emailServices;
        }


        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO request)
        {
            var user = await _authServices.AddAsync(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return BadRequest("User already exists!");

            await _emailServices.SendVerificationCodeAsync(user.UserId);

            return Ok(user.UserId);
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDTO request)
        {

            var result = await _emailServices.VerifyCodeAsync(request.Id, request.Code);

            if (!result)
                return BadRequest("Invalid or expired code.");

            return Ok("Email verified successfully.");
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogInAsync([FromBody] LoginDTO request)
        {
            var token = await _authServices.UserValidationAsync(request);

            if (token == null)
                return BadRequest("Invalid Email or Password!");

            if (!token.IsVerified)
                return BadRequest("Please verify your email before logging in.");

            return Ok(token.Token);
        }

    }
}
