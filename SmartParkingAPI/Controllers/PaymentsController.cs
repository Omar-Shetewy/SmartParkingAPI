
namespace SmartParking.API.Controllers;

[Route("api/Payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentsController(IMapper mapper, IPaymentService paymentService, IPaymentMethodService paymentMethodService)
    {
        _mapper = mapper;
        _paymentService = paymentService;
        _paymentMethodService = paymentMethodService;
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

        return Ok(data);
    }

    [HttpGet]
    [Route("GetByPaymentMethodId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByPaymentMethodId(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid ID:{id}");

        var isValidPaymentMethod = await _paymentMethodService.isValidPaymentMethod(id);

        if (!isValidPaymentMethod)
            return BadRequest($"Invalid Payment Method Id:{id}");

        var payments = await _paymentService.GetByPaymentMethodId(id);

        if (payments.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<PaymentDetailsDTO>>(payments);

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

        var payment = await _paymentService.GetById(id);

        if (payment == null)
            return NotFound($"Payment with id = {id} is not found.");

        var data = _mapper.Map<PaymentDetailsDTO>(payment);

        return Ok(data);
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] PaymentDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isValidPaymentMethod = await _paymentMethodService.isValidPaymentMethod(dto.PaymentMethodId);

        if (!isValidPaymentMethod)
            return BadRequest($"Invalid Payment Method ID:{dto.PaymentMethodId}");

        var payment = _mapper.Map<Payment>(dto);

        await _paymentService.Add(payment);

        var data = _mapper.Map<PaymentDetailsDTO>(payment);

        return Ok(data);
    }

    [HttpPut]
    [Route("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCarAsync(int id, [FromBody] PaymentDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequest($"Invalid ID: {id}");

        var isValidPaymentMethod = await _paymentMethodService.isValidPaymentMethod(dto.PaymentMethodId);

        if (!isValidPaymentMethod)
            return BadRequest($"Invalid Payment Method ID:{dto.PaymentMethodId}");

        var payment = await _paymentService.GetById(id);

        payment.Amount = dto.Amount;
        payment.PaymentStatus = dto.PaymentStatus;
        payment.PaymentMethodId = dto.PaymentMethodId;

        _paymentService.Update(payment);

        var data = _mapper.Map<PaymentDetailsDTO>(payment);

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

        var payment = await _paymentService.GetById(id);

        if (payment == null)
            return NotFound($"Payment with ID ({id}) is not found.");

        _paymentService.Delete(payment);

        var data = _mapper.Map<PaymentDetailsDTO>(payment);

        return Ok(data);
    }
}
