using SmartParking.API.Services.Interface;

namespace SmartParking.API.Controllers;

[Route("api/ReservationRecords")]
[ApiController]
public class ReservationRecordsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IUserService _userService;
    private readonly IGarageService _garageService;
    private readonly IMapper _mapper;

    public ReservationRecordsController(IReservationService reservationService, IMapper mapper, IUserService userService, IGarageService garageService)
    {
        _reservationService = reservationService;
        _mapper = mapper;
        _userService = userService;
        _garageService = garageService;
    }

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAsync()
    {
        var records = await _reservationService.GetAll();

        if (records.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<ReservationRecordDetailsDTO>>(records);

        return Ok(data);
    }

    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");

        var record = await _reservationService.GetById(id);

        if (record == null)
            return NotFound($"User with id = {id} is not found.");

        var data = _mapper.Map<ReservationRecordDetailsDTO>(record);

        return Ok(data);
    }

    [HttpGet]
    [Route("GetByUserId/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        if (userId < 1)
            return BadRequest($"Invalid ID:{userId}");

        var isValidUser = await _userService.isValidUserAsync(userId);

        if (!isValidUser)
            return BadRequest($"Invalid User Id:{userId}");

        var record = await _reservationService.GetByUserId(userId);

        if (record == null)
            return NoContent();

        var data = _mapper.Map<ReservationRecordDetailsDTO>(record);

        return Ok(data);
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] ReservationRecordDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isValidUser = await _userService.isValidUserAsync(dto.UserId);

        if (!isValidUser)
            return BadRequest($"Invalid User Id:{dto.UserId}");

        var isValidGarage = await _garageService.isValidGarage(dto.GarageId);

        if (!isValidGarage)
            return BadRequest($"Invalid Garage Id:{dto.GarageId}");

        var recordByUserId = await _reservationService.GetByUserId(dto.UserId);

        if (recordByUserId != null)
            return BadRequest($"Invalid User ID: user ID {dto.UserId} is already assigned to another registration record");
           
        var record = _mapper.Map<ReservationRecord>(dto);

        await _reservationService.Add(record);

        var data = _mapper.Map<ReservationRecordDTO>(record);

        return Ok(data);
    }

    [HttpPut]
    [Route("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ReservationRecordUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequest($"Invalid ID: {id}");

        var record = await _reservationService.GetById(id);

        record.StartDate = dto.StartDate;
        record.EndDate = dto.EndDate;

        _reservationService.Update(record);

        var data = _mapper.Map<ReservationRecordDTO>(record);

        return Ok(data);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequest($"Invalid ID: {id}");

        var record = await _reservationService.GetById(id);

        if (record == null)
            return NotFound($"Record with ID {id} is not found.");

        _reservationService.Delete(record);

        var data = _mapper.Map<ReservationRecordDTO>(record);

        return Ok(data);
    }
}
