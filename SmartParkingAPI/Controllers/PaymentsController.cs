namespace SmartParking.API.Controllers;

[Route("api/Payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPaymentService _paymentService;
    private readonly IReservationService _ReservationService;
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentsController(IMapper mapper, IPaymentService paymentService, IPaymentMethodService paymentMethodService, IReservationService reservationService)
    {
        _mapper = mapper;
        _paymentService = paymentService;
        _ReservationService = reservationService;
        _paymentMethodService = paymentMethodService;
        _ReservationService = reservationService;
    }

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAsync()
    {
        var payments = await _paymentService.GetAll();

        if (payments.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<PaymentDetailsDTO>>(payments);

        return Ok(new ApiResponse<List<PaymentDetailsDTO>>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetByPaymentMethodId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByPaymentMethodId(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID:{id}", false));

        var isValidPaymentMethod = await _paymentMethodService.isValidPaymentMethod(id);

        if (!isValidPaymentMethod)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Payment Method Id:{id}", false));

        var payments = await _paymentService.GetByPaymentMethodId(id);

        if (payments.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<PaymentDetailsDTO>>(payments);

        return Ok(new ApiResponse<List<PaymentDetailsDTO>>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetByReservationRecordId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByReservationRecordIdAsync(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID:{id}", false));

        var isValidReservationRecord = await _ReservationService.isValidReservationRecord(id);

        if (!isValidReservationRecord)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Reservation Record Id:{id}", false));

        var payments = await _paymentService.GetByReservationRecordId(id);

        if (payments.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<PaymentDetailsDTO>>(payments);

        return Ok(new ApiResponse<List<PaymentDetailsDTO>>(data, "Success", true));
    }

    [HttpGet]
    [Route("GetById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID:{id}", false));

        var payment = await _paymentService.GetById(id);

        if (payment == null)
            return NotFound($"Payment with id = {id} is not found.");

        var data = _mapper.Map<PaymentDetailsDTO>(payment);

        return Ok(new ApiResponse<PaymentDetailsDTO>(data, "Success", true));
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] PaymentDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        var isValidPaymentMethod = await _paymentMethodService.isValidPaymentMethod(dto.PaymentMethodId);

        if (!isValidPaymentMethod)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Payment Method Id:{dto.PaymentMethodId}", false));

        var isValidReservationRecord = await _ReservationService.isValidReservationRecord(dto.ReservationRecordId);

        if (!isValidReservationRecord)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Reservation Record Id:{dto.PaymentMethodId}", false));

        var paymentByReservationRecord = _paymentService.GetByReservationRecordId(dto.ReservationRecordId);

        if (paymentByReservationRecord != null)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{dto.ReservationRecordId}", false));

        var payment = _mapper.Map<Payment>(dto);

        await _paymentService.Add(payment);

        var data = _mapper.Map<PaymentDetailsDTO>(payment);

        return Ok(new ApiResponse<PaymentDetailsDTO>(data, "Success", true));
    }

    [HttpPut]
    [Route("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] PaymentUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState, "", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var isValidPaymentMethod = await _paymentMethodService.isValidPaymentMethod(dto.PaymentMethodId);

        if (!isValidPaymentMethod)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Payment Method Id:{dto.PaymentMethodId}", false));

        var payment = await _paymentService.GetById(id);

        if (payment == null)
            return NotFound($"Payment with id {id} is not found!");

        payment.Amount = dto.Amount;
        payment.PaymentStatus = dto.PaymentStatus;
        payment.PaymentMethodId = dto.PaymentMethodId;

        _paymentService.Update(payment);

        var data = _mapper.Map<PaymentDetailsDTO>(payment);

        return Ok(new ApiResponse<PaymentDetailsDTO>(data, "Success", true));
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

        var payment = await _paymentService.GetById(id);

        if (payment == null)
            return NotFound($"Payment with ID ({id}) is not found.");

        _paymentService.Delete(payment);

        var data = _mapper.Map<PaymentDetailsDTO>(payment);

        return Ok(new ApiResponse<PaymentDetailsDTO>(data, "Success", true));
    }
}
