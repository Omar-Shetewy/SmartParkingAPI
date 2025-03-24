namespace SmartParking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GaragesController : ControllerBase
{
    private readonly IGarageService _garageService;
    private readonly IMapper _mapper;
    public GaragesController(IGarageService garageService, IMapper mapper)
    {
        _garageService = garageService;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("GetAllGarages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllGaragesAsync()
    {
        var garages = await _garageService.GetAll();
        if (garages.Count() == 0)
            return NoContent();
        var data = _mapper.Map<List<GarageDetailsDTO>>(garages);
        return Ok(data);
    }

    [HttpGet]
    [Route("GetGarageById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetGarageById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");
        var garage = await _garageService.GetBy(id);
        if (garage == null)
            return NoContent();
        var data = _mapper.Map<GarageDetailsDTO>(garage);
        return Ok(data);
    }

    [HttpGet]
    [Route("GetAllSpots/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllSpots(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");
        var spots = await _garageService.GetAllSpots(id);
        if (spots.Count() == 0)
            return NoContent();
        var data = _mapper.Map<List<SpotDetailsDTO>>(spots);
        return Ok(data);
    }

    [HttpPost]
    [Route("AddGarage")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGarageAsync([FromBody] GarageDTO garageDTO)
    {
        if (garageDTO == null)
            return BadRequest("Garage data is required");
        var garage = _mapper.Map<Garage>(garageDTO);
        var result = await _garageService.Add(garage);
        if (result == null)
            return BadRequest("Failed to add garage");
        return CreatedAtAction(nameof(GetGarageById), new { id = result.GarageId }, result);
    }

    [HttpPut]
    [Route("UpdateGarage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateGarage([FromBody] GarageDetailsDTO garageDTO)
    {
        if (garageDTO == null)
            return BadRequest("Garage data is required");
        var garage = _mapper.Map<Garage>(garageDTO);
        var result = _garageService.Update(garage);
        if (result == null)
            return BadRequest("Failed to update garage");
        return Ok(result);
    }

    [HttpDelete]
    [Route("DeleteGarage/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteGarage(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");
        var garage = await _garageService.GetBy(id);
        if (garage == null)
            return BadRequest($"Garage with ID:{id} not found");
        var result = _garageService.Delete(garage);
        if (result == null)
            return BadRequest("Failed to delete garage");
        return Ok(result);
    }
}
