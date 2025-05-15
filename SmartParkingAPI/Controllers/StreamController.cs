using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartParking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly IConfiguration _config;
        public StreamController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("urls")]
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
    }
}
