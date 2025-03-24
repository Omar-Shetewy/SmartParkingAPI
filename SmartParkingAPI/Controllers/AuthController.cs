
namespace SmartParking.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authServices;
        public AuthController(IAuthService authServices)
        {
            _authServices = authServices;
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
            return Ok(user);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogInAsync([FromBody] LoginDTO request)
        {
            var token = await _authServices.UserValidationAsync(request);

            if (token == null)
                return BadRequest("Invalid Email or Password!");

            return Ok(token);
        }

    }
}
