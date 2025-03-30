namespace SmartParking.API.Controllers;

[ApiController]
[Route("api/ai/[controller]")]
public class ANBRController : ControllerBase
{
    private readonly IANBRService _anbrService;

    public ANBRController(IANBRService anbrService)
    {
        _anbrService = anbrService;
    }

    [HttpPost("PlateRecogantion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PlateRecogantion([FromBody] PlateRecordDTO request)
    {

        if (string.IsNullOrEmpty(request.ImageFile))
        {
            return BadRequest("No image data provided.");
        }
        try
        {
            byte[] imageBytes = Convert.FromBase64String(request.ImageFile);

            var plateNumber = await _anbrService.SendToAIModel(imageBytes);

            if (string.IsNullOrEmpty(plateNumber))
            {
                return BadRequest("AI model failed to extract the plate number.");
            }

            bool isValid = await _anbrService.ValidatePlateNumber(plateNumber);

            //await _anbrService.SavePlateData(imageBytes, plateNumber);

            return Ok(plateNumber);
        }
        catch (FormatException)
        {
            return BadRequest("Invalid base64 image data.");
        }
    }
}
