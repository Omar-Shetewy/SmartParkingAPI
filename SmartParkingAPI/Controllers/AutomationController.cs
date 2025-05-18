namespace SmartParking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutomationController : ControllerBase
{
    private readonly IGarageService _garageService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    public AutomationController(IConfiguration config, IGarageService garageService, IMapper mapper)
    {
        _config = config;
        _garageService = garageService;
        _mapper = mapper;
    }

    [HttpGet("Stream/urls")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetCameraUrls()
    {
        var serverIp = _config["MediaServerIp"];

        var response = new
        {
            Camera1 = new
            {
                RTSP = $"rtsp://{serverIp}:8554/cam1",
                HLS = $"http://{serverIp}:8888/cam1/index.m3u8"
            },
            Camera2 = new
            {
                RTSP = $"rtsp://{serverIp}:8554/cam2",
                HLS = $"http://{serverIp}:8888/cam2/index.m3u8"
            }
        };

        return Ok(new ApiResponse<object>(response, "Success", true));

    }

    [HttpPost]
    [Route("AddEntryCar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddEntryCarAsync([FromBody] EntryCarDTO entryCarDTO)
    {

        var isValidPlateNumber = await _garageService.isValidPlateNumber(entryCarDTO.PlateNumber);
        //if (!isValidPlateNumber)
        //    return BadRequest(new ApiResponse<object>(null, $"Invalid Plate Number {entryCarDTO.PlateNumber}", false));
        if (entryCarDTO == null)
            return BadRequest(new ApiResponse<object>(null, "Entry car data is required", false));
        var isValidGarage = await _garageService.isValidGarage(entryCarDTO.GarageId);
        if (!isValidGarage)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage ID {entryCarDTO.GarageId}", false));
        var entryCar = _mapper.Map<EntryCar>(entryCarDTO);
        var result = await _garageService.AddEntryCar(entryCar);
        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to add entry car", false));

        var entryCarDetails = _mapper.Map<EntryCarDetailsDTO>(result);

        return Ok(new ApiResponse<EntryCarDetailsDTO>(entryCarDetails, "Success", true));

    }

    [HttpPut]
    [Route("CarExit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CarExit([FromBody] EntryCarDTO entryCarDTO)
    {
        if (entryCarDTO == null)
            return BadRequest(new ApiResponse<object>(null, "Entry car data is required", false));
        var isValidGarage = await _garageService.isValidGarage(entryCarDTO.GarageId);
        if (!isValidGarage)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage ID {entryCarDTO.GarageId}", false));
        var entryCar = _mapper.Map<EntryCar>(entryCarDTO);
        var result = await _garageService.UpdateExitCar(entryCar.PlateNumber);
        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to add entry car", false));
        return Ok(new ApiResponse<EntryCar>(result, "Success", true));
    }
}
