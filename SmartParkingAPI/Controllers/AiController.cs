namespace SmartParking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    private readonly IAiService _anbrService;

    public AiController(IAiService anbrService)
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

            var aiResponse = await _anbrService.SendToAIModel(imageBytes);

            if (string.IsNullOrEmpty(aiResponse))
            {
                return BadRequest("AI model failed to extract the plate number.");
            }

            // Extract values from AI response
            var aiResult = JsonSerializer.Deserialize<Dictionary<string, string>>(aiResponse);
            if (aiResult == null || !aiResult.ContainsKey("Plate Number"))
            {
                return BadRequest("Invalid AI response.");
            }

            string plateNumber = aiResult["Plate Number"];

            bool isValid = await _anbrService.ValidatePlateNumber(plateNumber);

            //await _anbrService.SavePlateData(imageBytes, plateNumber);

            return Ok(isValid);
        }
        catch (FormatException)
        {
            return BadRequest("Invalid base64 image data.");
        }
    }
}
