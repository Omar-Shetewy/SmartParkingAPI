namespace SmartParking.API.Controllers;

[Route("api/ReservationRecords")]
[ApiController]
public class ReservationRecordsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IUserService _userService;
    private readonly IGarageService _garageService;
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;

    public ReservationRecordsController(IReservationService reservationService, IMapper mapper, IUserService userService, IGarageService garageService, IPaymentService paymentService)
    {
        _reservationService = reservationService;
        _mapper = mapper;
        _userService = userService;
        _garageService = garageService;
        _paymentService = paymentService;
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

        return Ok(new ApiResponse<List<ReservationRecordDetailsDTO>>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var record = await _reservationService.GetById(id);

        if (record == null)
            return NotFound($"User with id = {id} is not found.");

        var data = _mapper.Map<ReservationRecordDetailsDTO>(record);

        return Ok(new ApiResponse<ReservationRecordDetailsDTO>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetByUserId/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByUserId(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var isValidUser = await _userService.isValidUserAsync(id);

        if (!isValidUser)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User Id:{id}", false));

        var record = await _reservationService.GetByUserId(id);

        if (record == null)
            return NoContent();

        var data = _mapper.Map<ReservationRecordDetailsDTO>(record);

        return Ok(new ApiResponse<ReservationRecordDetailsDTO>(data, "Success", true));
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] ReservationRecordDTO dto)
    {
        
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        var isValidUser = await _userService.isValidUserAsync(dto.UserId);

        if (!isValidUser)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User", false));

        var isValidGarage = await _garageService.isValidGarage(dto.GarageId);

        if (!isValidGarage)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Garage", false));

        var garage = await _garageService.GetBy(dto.GarageId);

        if (garage == null || garage.AvailableSpots <= 0)
            return BadRequest(new ApiResponse<object>(null, "No available spots in this garage", false));

        var record = _mapper.Map<ReservationRecord>(dto);

        await _reservationService.Add(record);

        var recordByUserId = await _reservationService.GetByUserId(dto.UserId);

        if (recordByUserId == null)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User is already assigned to another registration record", false));


        var data = _mapper.Map<ReservationRecordDetailsDTO>(record);

        return Ok(new ApiResponse<ReservationRecordDetailsDTO>(data, "Success", true));
    }

    [HttpPut]
    [Route("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ReservationRecordDetailsDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var record = await _reservationService.GetById(id);

        if (record == null)
            return NotFound($"Record with id {id} is not found!");

        record.StartDate = dto.StartDate;
        record.EndDate = dto.EndDate;

        _reservationService.Update(record);

        var data = _mapper.Map<ReservationRecordDTO>(record);

        return Ok(new ApiResponse<ReservationRecordDTO>(data, "Success", true));
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var record = await _reservationService.GetById(id);

        if (record == null)
            return NotFound($"Record with ID {id} is not found.");

        _reservationService.Delete(record);

        var data = _mapper.Map<ReservationRecordDTO>(record);

        return Ok(new ApiResponse<ReservationRecordDTO>(data, "Success", true));
    }
}
