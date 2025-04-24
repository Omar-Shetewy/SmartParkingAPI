namespace SmartParking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpotController : ControllerBase
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

        return Ok(new ApiResponse<List<Spot>>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetSpotById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSpotById(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID:{id}", false));

        var spot = await _spotService.GetById(id);

        if (spot == null)
            return NoContent();

        var data = _mapper.Map<SpotDTO>(spot);
        
        return Ok(new ApiResponse<SpotDTO>(data, "Success", true));
    }

    [HttpPost]
    [Route("AddSpot")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSpotAsync([FromBody] SpotDTO spotDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        if (spotDTO == null)
            return BadRequest(new ApiResponse<object>(null, "Spot data is required", false));

        var spot = _mapper.Map<Spot>(spotDTO);

        if (spot == null)
            return BadRequest(new ApiResponse<object>(null, "Invalid spot data", false));
        
        var result = await _spotService.Add(spot);

        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to add spot", false));

        return Ok(new ApiResponse<Spot>(result, "Spot added successfully", true));
    }
}
