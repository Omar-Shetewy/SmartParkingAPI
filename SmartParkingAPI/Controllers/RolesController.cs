using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartParking.API.Services.Interface;
using System.Threading.Tasks;

namespace SmartParking.API.Controllers
{
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

            var roles = await _roleService.GetAllRolesAsync();

            if (roles == null)
            {
                return NotFound("There is no roles");
            }

            return Ok(roles);
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

            var role = await _roleService.GetRoleByIdAsync(id);

            if (role == null)
            {
                return NotFound("There is no rule for this id");
            }

            return Ok(role);
        }

        [HttpPost]
        [Route("AddRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddRoleAsync(RoleDTO role)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>(ModelState,"", false));

            if (role == null)
                return BadRequest("Please Add A Role");

            var Role = new Role
            {
                RoleName = role.RoleName
            };

            await _roleService.AddRoleAsync(Role);

            return Ok(Role);
        }

        [HttpPut]
        [Route("UpdateRole/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateRoleAsync(int id,[FromBody] RoleDTO role)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>(ModelState,"", false));

            var Role = await _roleService.GetRoleByIdAsync(id);

            if (Role == null)
            {
                return NotFound("There is no rule for this id");
            }

            Role.RoleName = role.RoleName;

            _roleService.UpdateRole(Role);
            
            return Ok(Role);
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
                return BadRequest($"Invalid Id");

            var role = await _roleService.GetRoleByIdAsync(id);

            if (role == null)
            {
                return NotFound("Role not found");
            }

            _roleService.DeleteRole(role);

            return Ok(role);
        }
    }
}
