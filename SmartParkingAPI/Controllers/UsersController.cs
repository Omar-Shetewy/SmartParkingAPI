﻿namespace SmartParkingAPI.Controllers;

[Authorize(Roles = "User,Admin")]
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

    // User output should be handeled
    [HttpGet]
    [Route("GetAllUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userServices.GetAllAsync();

        if (users == null)
            return NotFound("No Users Found!");

        var data = _mapper.Map<IEnumerable<UserDTO>>(users);

        return Ok(new ApiResponse<IEnumerable<UserDTO>>(data, "Success", true));
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    [Route("GetUserById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var user = await _userServices.GetByAsync(id);

        if (user == null)
            return NoContent();

        return Ok(new ApiResponse<User>(user, "Success", true));
    }

    //[Authorize(Roles = "Admin, User")]
    [HttpPut]
    [Route("UpdateUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        var user = await _userServices.GetByAsync(id);

        if (user == null || dto.RoleId < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{id}", false));

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.PhoneNumber = dto.PhoneNumber;
        user.RoleId = dto.RoleId;

        _userServices.Update(user);

        return Ok(new ApiResponse<UserDTO>(dto, "Success", true));
    }

    [HttpPut]
    [Route("UpdateUserRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRoleAsync(int id, [FromBody] SingleRoleDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        var user = await _userServices.GetByAsync(id);

        if (user == null || dto.RoleId < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{id}", false));

        user.RoleId = dto.RoleId;

        _userServices.Update(user);

        var Id = dto.RoleId;

        return Ok(new ApiResponse<int>(Id, "Success", true));

    }

    [HttpDelete]
    [Route("DeleteUserById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var user = await _userServices.GetByAsync(id);

        if (user == null)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{id}", false));

        _userServices.Delete(user);

        return Ok(new ApiResponse<User>(user, "Success", true));
    }
}
