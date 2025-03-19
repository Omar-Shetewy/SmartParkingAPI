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

        return Ok(data);
    }

    [HttpGet]
    [Route("GetCarsByUserId/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCarsByUserId(int userId)
    {
        if (userId < 1)
            return BadRequest($"Invalid ID:{userId}");

        var isValidUser = await _userService.isValidUser(userId);

        if (!isValidUser)
            return BadRequest($"Invalid User Id:{userId}");

        var cars = await _carService.GetByUserId(userId);

        if (cars.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<CarDetailsDTO>>(cars);

        return Ok(data);
    }

    [HttpGet]
    [Route("GetCarById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCarByIdAsync(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");

        var car = await _carService.GetBy(id);

        if (car == null)
            return NotFound($"User with id = {id} is not found.");

        var data = _mapper.Map<CarDetailsDTO>(car);

        return Ok(data);
    }

    [HttpPost]
    [Route("AddNewCar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNewCarAsync([FromBody] CarDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isValidUser = await _userService.isValidUser(dto.UserId);

        if (!isValidUser)
            return BadRequest($"Invalid User Id:{dto.UserId}");

        var carWithPlateNumber = await _carService.GetBy(dto.PlateNumber);

        if (carWithPlateNumber != null)
            return BadRequest($"Invalid Plate Number: {dto.PlateNumber}");

        var car = _mapper.Map<Car>(dto);

        await _carService.Add(car);

        return Ok(car);
    }

    [HttpPut]
    [Route("UpdateCar/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarAsync(int id, [FromBody] CarDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequest($"Invalid ID: {id}");

        var isValidUser = await _userService.isValidUser(dto.UserId);

        if (!isValidUser)
            return BadRequest($"Invalid User Id:{dto.UserId}");

        var car = await _carService.GetBy(id);

        car.PlateNumber = dto.PlateNumber;
        car.Model = dto.Model;
        car.Type = dto.Type;
        car.UserId = dto.UserId;

        _carService.Update(car);

        return Ok(car);
    }

    [HttpDelete]
    [Route("DeleteCar/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCarAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequest($"Invalid ID: {id}");

        var car = await _carService.GetBy(id);

        if (car == null)
            return NotFound($"Car with ID {id} is not found.");

        _carService.Delete(car);

        return Ok(car);
    }
}
