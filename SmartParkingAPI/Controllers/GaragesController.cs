namespace SmartParking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GaragesController : ControllerBase
{
    private readonly IGarageService _garageService;
    private readonly IMapper _mapper;
    private readonly IOwnerService _ownerService;
    public GaragesController(IGarageService garageService, IMapper mapper, IOwnerService ownerService)
    {
        _garageService = garageService;
        _mapper = mapper;
        _ownerService = ownerService;
    }


    [HttpGet]
    [Route("GetAllGarages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllGaragesAsync()
    {
        var garages = await _garageService.GetAll();
        if (garages.Count() == 0)
            return NoContent();
        var data = _mapper.Map<List<GarageDetailsDTO>>(garages);
        return Ok(new ApiResponse<List<GarageDetailsDTO>>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetGarageById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetGarageById(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID:{id}", false));
        var garage = await _garageService.GetBy(id);
        if (garage == null)
            return NoContent();
        var data = _mapper.Map<GarageDetailsDTO>(garage);
        return Ok(new ApiResponse<GarageDetailsDTO>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetGarage/{id}/Spots")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllSpots(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID:{id}", false));
        var spots = await _garageService.GetAllSpots(id);
        if (spots.Count() == 0)
            return NoContent();
        var data = _mapper.Map<List<SpotDetailsDTO>>(spots);
        return Ok(new ApiResponse<List<SpotDetailsDTO>>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetGarage/{id}/Cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllCars(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID:{id}", false));
        var cars = await _garageService.GetAllCars(id);
        if (cars.Count() == 0)
            return NoContent();
        var data = _mapper.Map<List<EntryCarDetailsDTO>>(cars);
        return Ok(new ApiResponse<List<EntryCarDetailsDTO>>(data, "Success", true));
    }

    [HttpPost]
    [Route("AddEntryCar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddEntryCarAsync([FromBody] EntryCarDTO entryCarDTO)
    {
        
        var isValidPlateNumber = await _garageService.isValidPlateNumber(entryCarDTO.PlateNumber);
        //if (!isValidPlateNumber)
        if (entryCarDTO == null)
            return BadRequest(new ApiResponse<object>(null, "Entry car data is required", false));
        var isValidGarage = await _garageService.isValidGarage(entryCarDTO.GarageId);
        if (!isValidGarage)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage ID {entryCarDTO.GarageId}", false));
        var entryCar = _mapper.Map<EntryCar>(entryCarDTO);
        var result = await _garageService.AddEntryCar(entryCar);
        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to add entry car", false));
        return Ok(new ApiResponse<EntryCar>(result, "Success", true));

    }

    [HttpPost]
    [Route("AddGarage")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGarageAsync([FromBody] GarageDTO garageDTO)
    {
        if (garageDTO == null)
            return BadRequest(new ApiResponse<object>(null, "Garage data is required", false));

        var isValidOwner = _ownerService.isValidOwner(garageDTO.OwnerId);

        if (!isValidOwner)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Owner ID {garageDTO.OwnerId}", false));

        var garage = _mapper.Map<Garage>(garageDTO);
        var result = await _garageService.Add(garage);
        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to add garage", false));
        return Ok(new ApiResponse<Garage>(result, "Success", true));
    }

    [HttpPut]
    [Route("UpdateGarage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateGarage([FromBody] GarageDTO garageDTO)
    {
        if (garageDTO == null)
            return BadRequest(new ApiResponse<object>(null, "Garage data is required", false));

        var isValidOwner = _ownerService.isValidOwner(garageDTO.OwnerId);

        if (!isValidOwner)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Owner ID {garageDTO.OwnerId}", false));

        var garage = _mapper.Map<Garage>(garageDTO);
        var result = _garageService.Update(garage);
        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to update garage", false));
        return Ok(new ApiResponse<Garage>(result, "Success", true));
    }

    [HttpDelete]
    [Route("DeleteGarage/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteGarage(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID:{id}", false));
        var garage = await _garageService.GetBy(id);
        if (garage == null)
            return BadRequest(new ApiResponse<object>(null, $"Garage with ID:{id} not found", false));
        var result = _garageService.Delete(garage);
        if (result == null)
            return BadRequest(new ApiResponse<object>(null, "Failed to delete garage", false));
        return Ok(new ApiResponse<Garage>(result, "Success", true));
    }
}
