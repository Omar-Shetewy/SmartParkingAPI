using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartParking.API.Data.DTO;
using SmartParking.API.Data.Models;

namespace SmartParking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutomationController : ControllerBase
{
    private readonly IGarageService _garageService;
    private readonly ISpotService _spotService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IHubContext<ParkingHub> _hub;
    public AutomationController(IConfiguration config, IGarageService garageService, IMapper mapper, ISpotService spotService, IHubContext<ParkingHub> hub)
    {
        _hub = hub;
        _config = config;
        _garageService = garageService;
        _mapper = mapper;
        _spotService = spotService;
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
        if (!_garageService.IsAvailableSpots(entryCarDTO.GarageId))
            return BadRequest(new ApiResponse<object>(null, "No Avaliable Spots", false));

        if (entryCarDTO == null)
            return BadRequest(new ApiResponse<object>(null, "Entry car data is required", false));

        var isValidGarage = await _garageService.isValidGarage(entryCarDTO.GarageId);
        if (!isValidGarage)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage ID {entryCarDTO.GarageId}", false));

        var entryCar = _mapper.Map<EntryCar>(entryCarDTO);

        var result = await _garageService.AddEntryCar(entryCar);
        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to add entry car", false));

        return Ok(new ApiResponse<object>(null, "Welcome :)", true));

    }

    [HttpPut]
    [Route("CarPosition")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CarPosition([FromBody] CarPositionDTO carPositionDTO)
    {
        if (carPositionDTO == null)
            return BadRequest(new ApiResponse<object>(null, "data is required", false));

        var isValidSpot = await _spotService.IsValidSpot(carPositionDTO.SpotId);
        if (!isValidSpot)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Spot ID {carPositionDTO.SpotId}", false));

        var carPosition = _mapper.Map<EntryCar>(carPositionDTO);

        var result = await _garageService.UpdateCarPosition(carPosition.PlateNumber, carPosition.SpotId);

        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Invalid PlateNumber", false));

        var isPlateNumberInApp = await _garageService.isPlateNumberInApp(carPositionDTO.PlateNumber);
        if (isPlateNumberInApp == null)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Plate Number {carPositionDTO.PlateNumber}", false));


        await _hub.Clients.User(isPlateNumberInApp.ToString())
          .SendAsync("ReceiveSpot", carPositionDTO.SpotId);
        return Ok(new ApiResponse<object>(null, "Success", true));

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
