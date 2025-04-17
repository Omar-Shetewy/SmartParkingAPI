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
                return BadRequest(ModelState);

            if (user == null)
                return BadRequest("User already exists!");

            await _emailServices.SendVerificationCodeAsync(user.UserId);

            RegisterDetailsDTO reg = new RegisterDetailsDTO
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserId = user.UserId
            };

            return Ok(reg);
        }

        [HttpPost("Resend-verification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reverify([FromHeader] int userId)
        {
            var user = await _userService.GetByAsync(userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return BadRequest("User not found!");

            if (user.IsVerified)
                return BadRequest("User already verified");

            await _emailServices.SendVerificationCodeAsync(user.UserId);

            return Ok("Verification code resent successfully.");
        }


        [HttpPost("VerifyEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDTO request)
        {
            var user = await _userService.GetByAsync(request.Id);

            if (user.IsVerified)
                return BadRequest("User already verified");

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

        [HttpPost("Forget-Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ForgetPassword(string Email)
        { 
            var user = await _userService.GetByAsync(Email);

            if (user == null)
                return BadRequest("User not found!");
            
            if (user.IsVerified)
                user.IsVerified = false;

            _userService.Update(user);

            await _emailServices.SendVerificationCodeAsync(user.UserId);

            return Ok("Verify Your Account Then Create Your New Password");
        }

        [HttpPost("update-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePassword(int id, string password)
        {
            var user = await _userService.GetByAsync(id);
            
            if (user == null)
                return BadRequest("User Not Found, Please Register First");

            var updated = _userService.UpdatePass(user, password);

            if (updated == null)
                return BadRequest("Password is the same as the old one!");

            return Ok("Password Updated Successfully");
        }
    }
}
