namespace SmartParking.API.Controllers;

[Route("api/Cars")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public CarsController(IUserService userService, ICarService carService, IMapper mapper)
    {
        _userService = userService;
        _carService = carService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAllCars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllCarsAsync()
    {
        var cars = await _carService.GetAll();

        if (cars.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<CarDetailsDTO>>(cars);

        return Ok(new ApiResponse<List<CarDetailsDTO>>(data, "", true));
    }

    [HttpGet]
    [Route("GetCarsByUserId/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCarsByUserId(int userId)
    {
        if (userId < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{userId}", false));

        var isValidUser = await _userService.isValidUserAsync(userId);

        if (!isValidUser)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{userId}", false));

        var cars = await _carService.GetByUserId(userId);

        if (cars.Count() == 0)
            return NoContent();

        var data = _mapper.Map<CarDetailsDTO>(cars);

        return Ok(new ApiResponse<CarDetailsDTO>(data, "", true));
    }

    [HttpGet]
    [Route("GetCarById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCarByIdAsync(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{id}", false));

        var car = await _carService.GetBy(id);

        if (car == null)
            return NotFound(new ApiResponse<object>(null, $"User with id = {id} is not found", false));

        var data = _mapper.Map<CarDetailsDTO>(car);

        return Ok(new ApiResponse<CarDetailsDTO>(data, "", true));
    }

    [HttpPost]
    [Route("AddNewCar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNewCarAsync([FromBody] CarDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        var isValidUser = await _userService.isValidUserAsync(dto.UserId);

        if (!isValidUser)
            return BadRequest(new ApiResponse<object>(null,$"Invalid User Id:{dto.UserId}", false));

        var carWithPlateNumber = await _carService.GetBy(dto.PlateNumber);

        if (carWithPlateNumber != null)
            return BadRequest(new ApiResponse<object>(null,$"Invalid Plate Number: {dto.PlateNumber}", false));

        var car = _mapper.Map<Car>(dto);
        await _carService.Add(car);

        return Ok(new ApiResponse<Car>(car, "", true));

    }

    [HttpPut]
    [Route("UpdateCar/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCarAsync(int id, [FromBody] CarDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{id}", false));

        var isValidUser = await _userService.isValidUserAsync(dto.UserId);

        if (!isValidUser)
            return BadRequest(new ApiResponse<object>(null,$"Invalid User Id:{dto.UserId}", false));

        var car = await _carService.GetBy(id);

        if (car == null)
            return NotFound(new ApiResponse<object>(null,$"Car with id {id} is not found!", false));


        car.PlateNumber = dto.PlateNumber;
        car.Model = dto.Model;
        car.Type = dto.Type;
        car.UserId = dto.UserId;

        _carService.Update(car);

        return Ok(new ApiResponse<Car>(car, "Updated Successfully", true));

    }

    [HttpDelete]
    [Route("DeleteCar/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCarAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{id}", false));

        var car = await _carService.GetBy(id);

        if (car == null)
            return NotFound(new ApiResponse<object>(null, $"Car with id {id} is not found!", false));

        _carService.Delete(car);

        return Ok(new ApiResponse<Car>(car, "Deleted Successfully", true));
    }
}
