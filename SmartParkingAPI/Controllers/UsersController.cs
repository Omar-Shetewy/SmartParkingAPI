namespace SmartParkingAPI.Controllers;

[Route("api/Users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userServices;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userServices = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userServices.GetAll();

        if (users.Count() == 0)
        {
            return NotFound("No Users Found!");
        }

        return Ok(users);
    }

    [HttpGet]
    [Route("GetUserById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");

        var user = await _userServices.GetBy(id);

        if (user == null)
            return NoContent();

        return Ok(user);
    }

    [HttpPost]
    [Route("Registration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegistrationAsync([FromBody] RegisterationDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userObj = await _userServices.GetBy(dto.Email);

        if (userObj != null)
            return BadRequest("User already exists with the same email address and password");

        var user = _mapper.Map<User>(dto);

        await _userServices.Add(user);

        return Ok(user);
    }

    [HttpPost]
    [Route("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LoginAsync(LoginDTO dto)
    {
        var user = await _userServices.GetBy(dto.Email, dto.Password);

        if (user == null)
            return NotFound($"User with email: {dto.Email} and password: {dto.Password} is not found!");

        return Ok(user);    
    }
}
