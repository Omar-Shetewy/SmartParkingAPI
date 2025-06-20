﻿namespace SmartParking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GaragesController : ControllerBase
{
    private readonly IGarageService _garageService;
    private readonly IMapper _mapper;
    public GaragesController(IGarageService garageService, IMapper mapper)
    {
        _garageService = garageService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAllGarages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllGaragesAsync()
    {
        try
        {
            var garages = await _garageService.GetAll();
            if (garages.Count() == 0)
                return NoContent();
            var data = _mapper.Map<List<GarageDetailsDTO>>(garages);
            return Ok(new ApiResponse<List<GarageDetailsDTO>>(data, "Success", true));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(null, ex.Message, false));
        }
    }

    [HttpGet]
    [Route("GetGarageById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetGarageById(int id)
    {
        try
        {
            if (id < 1)
                return BadRequest(new ApiResponse<object>(null, $"Invalid Id", false));
            var garage = await _garageService.GetBy(id);
            if (garage == null)
                return NoContent();
            var data = _mapper.Map<GarageDetailsDTO>(garage);
            return Ok(new ApiResponse<GarageDetailsDTO>(data, "Success", true));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(null, ex.Message, false));
        }

    }

    [HttpGet]
    [Route("GetGarage/{id}/Spots")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllSpots(int id)
    {
        try
        {
            if (id < 1)
                return BadRequest(new ApiResponse<object>(null, $"Invalid Id", false));
            var spots = await _garageService.GetAllSpots(id);
            if (spots.Count() == 0)
                return NoContent();
            var data = _mapper.Map<List<SpotDetailsDTO>>(spots);
            return Ok(new ApiResponse<List<SpotDetailsDTO>>(data, "Success", true));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(null, ex.Message, false));
        }
    }

    [HttpGet]
    [Route("GetGarage/{id}/Cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllCars(int id)
    {
        try
        {
            if (id < 1)
                return BadRequest(new ApiResponse<object>(null, $"Invalid Id", false));
            var cars = await _garageService.GetAllCars(id);
            if (cars.Count() == 0)
                return NoContent();
            var data = _mapper.Map<List<EntryCarDetailsDTO>>(cars);
            return Ok(new ApiResponse<List<EntryCarDetailsDTO>>(data, "Success", true));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(null, ex.Message, false));
        }
    }

    [HttpPost]
    [Route("AddGarage")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddGarageAsync([FromBody] GarageDTO garageDTO)
    {
        try
        {
            if (garageDTO == null)
                return BadRequest(new ApiResponse<object>(null, "Garage data is required", false));

            var garage = _mapper.Map<Garage>(garageDTO);
            var result = await _garageService.Add(garage);

            if (result == null)
                return BadRequest(new ApiResponse<object>(null, "Failed to add garage", false));

            return Ok(new ApiResponse<Garage>(result, "Success", true));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(null, ex.Message, false));
        }
    }

    [HttpPut]
    [Route("UpdateGarage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateGarage([FromBody] GarageDTO garageDTO)
    {
        try
        {
            if (garageDTO == null)
                return BadRequest(new ApiResponse<object>(null, "Garage data is required", false));

            var garage = _mapper.Map<Garage>(garageDTO);
            var result = _garageService.Update(garage);

            if (result == null)
                return BadRequest(new ApiResponse<object>(null, "Failed to update garage", false));

            return Ok(new ApiResponse<Garage>(result, "Success", true));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(null, ex.Message, false));
        }
    }

    [HttpDelete]
    [Route("DeleteGarage/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteGarage(int id)
    {
        try
        {
            if (id < 1)
                return BadRequest(new ApiResponse<object>(null, $"Invalid Id", false));
            var garage = await _garageService.GetBy(id);
            if (garage == null)
                return BadRequest(new ApiResponse<object>(null, $"Garage with Id", false));
            var result = _garageService.Delete(garage);
            if (result == null)
                return BadRequest(new ApiResponse<object>(null, "Failed to delete garage", false));
            return Ok(new ApiResponse<Garage>(result, "Success", true));
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<object>(null, ex.Message, false));
        }
    }
}

