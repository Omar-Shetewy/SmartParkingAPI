namespace SmartParking.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly NotificationService _notifier;
    private readonly GarageService _garageService;
    private readonly ApplicationDbContext _dbContext;

    public NotificationsController(NotificationService notifier,ApplicationDbContext dbContext, GarageService garageService)
    {

        _notifier = notifier;
        _dbContext = dbContext;
        _garageService = garageService;
    }

    [HttpPost("PushNotification")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PersonNearCar(string plateNumber)
    {
        var userId = await _garageService.isPlateNumberInApp(plateNumber);
        if (userId == null) return NoContent();

        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null || string.IsNullOrEmpty(user.FcmToken))
            return NotFound("User or token not found");

        await _notifier.SendNotificationAsync(user.FcmToken,
            "Car Alert!",
            $"Someone is near your car. Check it now!");

        return Ok(new ApiResponse<object>(null, "Success", true));
    }
}
