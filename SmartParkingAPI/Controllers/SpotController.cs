using Microsoft.OpenApi.Any;

namespace SmartParking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpotController: ControllerBase
{
    private readonly ISpotService _spotService;
    private readonly IMapper _mapper;

    public SpotController(ISpotService spotService, IMapper mapper)
    {
        _spotService = spotService;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("GetAllSpots")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllSpotsAsync()
    {
        var spots = await _spotService.GetAll();
        if (spots.Count() == 0)
            return NoContent();
        var data = _mapper.Map<List<Spot>>(spots);
        return Ok(data);
    }

    [HttpGet]
    [Route("GetSpotsByGarageId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSpotsByGarageId(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");
        var spots = await _spotService.GetByGarageId(id);
        if (spots.Count() == 0)
            return NoContent();
        var data = _mapper.Map<List<SpotDetailsDTO>>(spots);
        return Ok(data);
    }

    [HttpGet]
    [Route("GetSpotById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSpotById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");
        var spot = await _spotService.GetById(id);
        if (spot == null)
            return NoContent();
        var data = _mapper.Map<SpotDTO>(spot);
        return Ok(data);
    }

    [HttpPost]
    [Route("AddSpot")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSpotAsync([FromBody] SpotDTO spotDTO)
    {
        if (spotDTO == null)
            return BadRequest("Spot data is required");
        var spot = _mapper.Map<Spot>(spotDTO);
        var result = await _spotService.Add(spot);
        if (result == null)
            return BadRequest("Failed to add spot");
        return Ok(spot);
    }
}
