using SmartParking.API.Data.DTO;
using SmartParking.API.Data.Models;
using SmartParkingAPI.Data.Models;

namespace SmartParking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutomationController : ControllerBase
{
    private readonly IGarageService _garageService;
    private readonly ISpotService _spotService;
    private readonly ICarService _carService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IHubContext<ParkingHub> _hub;
    public AutomationController(IConfiguration config, IGarageService garageService, IUserService userService, IMapper mapper, ISpotService spotService, ICarService carService, IHubContext<ParkingHub> hub)
    {
        _userService = userService;
        _carService = carService;
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
        if (entryCarDTO == null)
            return BadRequest(new ApiResponse<object>(null, "Entry car data is required", false));

        if (!_garageService.IsAvailableSpots(entryCarDTO.GarageId))
            return BadRequest(new ApiResponse<object>(null, "No Avaliable Spots", false));

        var isValidGarage = await _garageService.isValidGarage(entryCarDTO.GarageId);

        if (!isValidGarage)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage", false));
        var allcars = await _garageService.GetAllCars(entryCarDTO.GarageId);
        if (allcars != null && allcars.Any(c => c.PlateNumber == entryCarDTO.PlateNumber && c.ExitTime == null))
            return BadRequest(new ApiResponse<object>(null, "This car is already in the garage", false));

        var entryCar = _mapper.Map<EntryCar>(entryCarDTO);

        var result = await _garageService.AddEntryCar(entryCar);

        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to add entry car", false));
        var userid = await _garageService.GetUserUsingplate(entryCarDTO.PlateNumber);
        if (userid != null)
        {
            var user = await _userService.GetByAsync(userid.Value);
            await _hub.Clients.User(userid.ToString())
                .SendAsync("SendAlert", $"Welcome! {user.FirstName}", "your car is in good hands.");
        }
        return Ok(new ApiResponse<object>(null, "Welcome :)", true));

    }
    [HttpPost("SendAlert")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendAlert([FromBody] AlertDTO alertdto)
    {
        var entryCar = await _garageService.GetEntrycarBySpotId(alertdto.SpotId);
        if (entryCar == null)
            return BadRequest(new ApiResponse<object>(null, "No car found in this spot", false));
        var car = await _carService.GetBy(entryCar.PlateNumber);
        if (car != null)
        {
            var user = await _userService.GetByAsync(car.UserId);
            await _hub.Clients.User(user.UserId.ToString())
                 .SendAsync("SendAlert", $"Alert! {user.FirstName}, Someone near your car.", "Your safety is our priority. Check on app, Please.");
        }
        return Ok(new ApiResponse<object>(null, "Success", true));
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
            return BadRequest(new ApiResponse<object>(null, $"This spot is not avaliable", false));

        var carPosition = _mapper.Map<EntryCar>(carPositionDTO);

        var result = await _garageService.UpdateCarPosition(carPosition.PlateNumber, carPosition.SpotId);

        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Invalid PlateNumber", false));

        var userid = await _garageService.GetUserUsingplate(carPositionDTO.PlateNumber);
        if (userid != null)
        {
            var user = await _userService.GetByAsync(userid.Value);
            var spot = _spotService.GetById(carPositionDTO.SpotId);
            await _hub.Clients.User(userid.ToString())
                    .SendAsync("ReceiveSpot", spot.Result.Code);
            await _hub.Clients.User(userid.ToString())
                 .SendAsync("SendAlert", $"{user.FirstName} Enjoy your time", $"your car is in {spot.Result.Code}, We’ll notify you if anything changes.");
        }


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
        var car = await _carService.GetBy(entryCar.PlateNumber);
        if (car != null)
        {
            var user = await _userService.GetByAsync(car.UserId);
            await _hub.Clients.User(car.UserId.ToString())
                 .SendAsync("SendAlert", $"Thanks {user.FirstName} for using our service!.", "Thanks for parking with us, We hope to see you soon.");

            await _hub.Clients.User(car.UserId.ToString())
                 .SendAsync("ReceiveSpot", "");

        }

        return Ok(new ApiResponse<object>(null, "Success", true));
    }
}
