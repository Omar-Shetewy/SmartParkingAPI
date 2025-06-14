namespace SmartParking.API.Controllers;

[Route("api/Roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Route("GetAllRoles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAllRolesAsync()
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        var role = await _roleService.GetAllRolesAsync();

        if (role == null)
            return NotFound("There is no roles");

        return Ok(new ApiResponse<IEnumerable<Role>>(role, "Success", true));
    }

    [HttpGet]
    [Route("GetRoleById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetRoleByIdAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));
        
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var role = await _roleService.GetRoleByIdAsync(id);

        if (role == null)
            return NotFound("There is no rule for this id");

        return Ok(new ApiResponse<Role>(role, "Success", true));
    }

    [HttpPost]
    [Route("AddRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddRoleAsync(RoleDTO Role)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        if (Role == null)
            return BadRequest("Please Add A Role");

        var role = new Role
        {
            RoleName = Role.RoleName
        };

        await _roleService.AddRoleAsync(role);

        return Ok(new ApiResponse<Role>(role, "Success", true));
    }

    [HttpPut]
    [Route("UpdateRole/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateRoleAsync(int id,[FromBody] RoleDTO Role)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));
        
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id", false));

        var role = await _roleService.GetRoleByIdAsync(id);

        if (role == null)
            return NotFound("There is no rule for this id");

        role.RoleName = Role.RoleName;

        _roleService.UpdateRole(role);

        return Ok(new ApiResponse<Role>(role, "Success", true));
    }

    [HttpDelete]
    [Route("DeleteRole/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteRoleAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var role = await _roleService.GetRoleByIdAsync(id);

        if (role == null)
            return NotFound("Role not found");

        _roleService.DeleteRole(role);

        return Ok(new ApiResponse<Role>(role, "Success", true));
    }
}
