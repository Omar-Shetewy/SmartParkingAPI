using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartParking.API.Controllers;

[Route("api/PaymentMethods")]
[ApiController]
public class PaymentMethodsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentMethodsController(IMapper mapper, IPaymentMethodService paymentMethodService)
    {
        _mapper = mapper;
        _paymentMethodService = paymentMethodService;
    }

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAsync()
    {
        var methods = await _paymentMethodService.GetAll();

        if (methods.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<PaymentMethodDetailsDTO>>(methods);

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

        var method = await _paymentMethodService.GetById(id);

        if (method == null)
            return NotFound($"Payment method with id = {id} is not found.");

        var data = _mapper.Map<PaymentMethodDetailsDTO>(method);

        return Ok(data);
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] PaymentMethodDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var method = _mapper.Map<PaymentMethod>(dto);

        await _paymentMethodService.Add(method);

        var data = _mapper.Map<PaymentMethodDetailsDTO>(method);

        return Ok(data);
    }

    [HttpPut]
    [Route("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] PaymentMethodDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequest($"Invalid ID: {id}");

        var method = await _paymentMethodService.GetById(id);

        method.Name = dto.Name;

        _paymentMethodService.Update(method);

        var data = _mapper.Map<PaymentMethodDetailsDTO>(method);

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

        var method = await _paymentMethodService.GetById(id);

        if (method == null)
            return NotFound($"Payment method with ID {id} is not found.");

        _paymentMethodService.Delete(method);

        var data = _mapper.Map<PaymentMethodDetailsDTO>(method);

        return Ok(data);
    }
}
