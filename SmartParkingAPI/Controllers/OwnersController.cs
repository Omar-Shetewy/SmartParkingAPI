using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartParking.API.Controllers;

[Route("api/Owners")]
[ApiController]
public class OwnersController : ControllerBase
{
    private readonly IOwnerService _ownerService;
    private readonly IMapper _mapper;

    public OwnersController(IOwnerService ownerService, IMapper mapper)
    {
        _ownerService = ownerService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAsync()
    {
        var owners = await _ownerService.GetAll();

        if (owners.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<OwnerDetailsDTO>>(owners);

        return Ok(data);
    }

    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");

        var owner = await _ownerService.GetById(id);

        if (owner == null)
            return NotFound($"Owner with id = {id} is not found.");

        var data = _mapper.Map<OwnerDetailsDTO>(owner);

        return Ok(data);
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] OwnerDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var owner = _mapper.Map<Owner>(dto);

        await _ownerService.Add(owner);

        var data = _mapper.Map<OwnerDetailsDTO>(owner);

        return Ok(data);
    }

    [HttpPut]
    [Route("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] OwnerDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequest($"Invalid ID: {id}");

        var owner = await _ownerService.GetById(id);

        if (owner == null)
            return NotFound($"owner with id {id} is not found!");

        owner.FirstName = dto.FirstName;
        owner.LastName = dto.LastName;
        owner.Email = dto.Email;
        owner.Address = dto.Address;
        owner.PhoneNumber = dto.PhoneNumber;
        owner.Gender = dto.Gender;

        _ownerService.Update(owner);

        var data = _mapper.Map<OwnerDetailsDTO>(owner);

        return Ok(data);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequest($"Invalid ID: {id}");

        var owner = await _ownerService.GetById(id);

        if (owner == null)
            return NotFound($"Owner with ID {id} is not found.");

        _ownerService.Delete(owner);

        var data = _mapper.Map<OwnerDetailsDTO>(owner);

        return Ok(data);
    }
}
