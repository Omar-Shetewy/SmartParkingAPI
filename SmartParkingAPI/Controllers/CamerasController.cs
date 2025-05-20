namespace SmartParking.API.Controllers;

[Route("api/Cameras")]
[ApiController]
public class CamerasController : ControllerBase
{
    private readonly ICameraService _cameraService;
    private readonly IGarageService _garageService;
    private readonly IMapper _mapper;

    public CamerasController(IMapper mapper, ICameraService cameraService, IGarageService garageService)
    {
        _mapper = mapper;
        _cameraService = cameraService;
        _garageService = garageService;
    }

    [HttpGet]
    [Route("GetAllCameras")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllCarsAsync()
    {
        var cameras = await _cameraService.GetAll();

        if (cameras.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<CameraDetailsDTO>>(cameras);

        return Ok(new ApiResponse<List<CameraDetailsDTO>>(data, "", true));
    }

    [HttpGet]
    [Route("GetCameraByGarageId/{garageId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCameraByGarageId(int garageId)
    {
        var isValidGarageId = await _garageService.isValidGarage(garageId);

        if (!isValidGarageId || garageId < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage Id:{garageId}", false));

        var cameras = await _cameraService.GetByGarageId(garageId);

        if (cameras.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<CameraDetailsDTO>>(cameras);

        return Ok(new ApiResponse<List<CameraDetailsDTO>>(data, "", true));
    }

        [HttpGet]
        [Route("GetCameraById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCameraById(int id)
        {
            if (id < 1)
                return BadRequest(new ApiResponse<object>(null, $"Invalid Camera Id:{id}", false));

        var camera = await _cameraService.GetBy(id);

        if (camera == null)
            return NotFound(new ApiResponse<object>(null, $"Camera with id = {id} is not found", false));

        var data = _mapper.Map<CameraDetailsDTO>(camera);

        return Ok(new ApiResponse<CameraDetailsDTO>(data, "", true));
    }

    [HttpPost]
    [Route("AddNewCamera")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNewCarAsync([FromBody] CameraDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        var isValidGarageId = await _garageService.isValidGarage(dto.GarageId);

        if (!isValidGarageId || dto.GarageId < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage Id:{dto.GarageId}", false));

        var camera = _mapper.Map<Camera>(dto);
        await _cameraService.Add(camera);

            var data = _mapper.Map<CameraDetailsDTO>(camera);

            return Ok(new ApiResponse<CameraDetailsDTO>(data, "", true));
        }

    [HttpPut]
    [Route("UpdateCamera/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCarAsync(int id, [FromBody] CameraDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        var isValidGarageId = await _garageService.isValidGarage(dto.GarageId);

        if (!isValidGarageId || dto.GarageId < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage Id:{dto.GarageId}", false));

        var camera = await _cameraService.GetBy(id);

        if (camera == null)
            return NotFound(new ApiResponse<object>(null, $"Camera with id {id} is not found!", false));

        camera.Name = dto.Name;
        camera.GarageId = dto.GarageId;

        _cameraService.Update(camera);

        return Ok(new ApiResponse<Camera>(camera, "Updated Successfully", true));

    }

    [HttpDelete]
    [Route("DeleteCamera/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCameraAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Camera Id:{id}", false));

        var camera = await _cameraService.GetBy(id);

        if (camera == null)
            return NotFound(new ApiResponse<object>(null, $"Camera with id {id} is not found!", false));

        _cameraService.Delete(camera);

        return Ok(new ApiResponse<Camera>(camera, "Deleted Successfully", true));
    }
}
