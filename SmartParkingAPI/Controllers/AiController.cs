namespace SmartParking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    private readonly IAiService _AiService;

    public AiController(IAiService AiService)
    {
        _AiService = AiService;
    }

    [HttpPost("PlateRecognation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PlateRecogantion([FromBody] PlateRecordDTO request)
    {

        if (string.IsNullOrEmpty(request.ImageFile))
        {
            // Handle the case where the image data is not provided
            return BadRequest(new ApiResponse<object>(null, "No image data provided.", false));
        }
        try
        {
            byte[] imageBytes = Convert.FromBase64String(request.ImageFile);

            var aiResponse = await _AiService.SendToAIModel(imageBytes);

            if (string.IsNullOrEmpty(aiResponse))
            {
                // Handle the case where the AI model fails to extract the plate number
                return BadRequest(new ApiResponse<object>(null, "AI model failed to extract the plate number.", false));
            }

            // Extract values from AI response
            var aiResult = JsonSerializer.Deserialize<Dictionary<string, string>>(aiResponse);
            if (aiResult == null || !aiResult.ContainsKey("Plate Number"))
            {
                return BadRequest(new ApiResponse<object>(null, "Invalid response format.", false));
            }

            string plateNumber = aiResult["Plate Number"];

            bool isValid = await _AiService.ValidatePlateNumber(plateNumber);

            //await _anbrService.SavePlateData(imageBytes, plateNumber);
            
            return Ok(new ApiResponse<string>(plateNumber, "Plate number recognized successfully", true));
        }
        catch (FormatException)
        {
            return BadRequest(new ApiResponse<object>(null, "Invalid base64 image data.", false));
        }
    }
}
