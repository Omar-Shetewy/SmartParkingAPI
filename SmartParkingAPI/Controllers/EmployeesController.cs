namespace SmartParking.API.Controllers;

[Route("api/Employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IEmployeeService _employeeService;
    private readonly IUserService _userService;
    private readonly IJobService _jobService;

    public EmployeesController(IMapper mapper, IEmployeeService employeeService, IUserService userService, IJobService jobService)
    {
        _mapper = mapper;
        _employeeService = employeeService;
        _userService = userService;
        _jobService = jobService;
    }

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAsync()
    {
        var employees = await _employeeService.GetAll();

        if (employees.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<EmployeeDetailsDTO>>(employees);

        return Ok(new ApiResponse<List<EmployeeDetailsDTO>>(data, "", true));

    }

    [HttpGet]
    [Route("GetByJobId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByJobId(int id)
    {
        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));


        var isValidJob = await _jobService.isValidJob(id);

        if (!isValidJob)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Job Id:{id}", false));

        var employees = await _employeeService.GetByJobId(id);

        if (employees.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<EmployeeDetailsDTO>>(employees);

        return Ok(new ApiResponse<List<EmployeeDetailsDTO>>(data, "", true));
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

        var employee = await _employeeService.GetById(id);

        if (employee == null)
            return NotFound($"Employee with id {id} is not found!");

        var data = _mapper.Map<EmployeeDetailsDTO>(employee);

        return Ok(new ApiResponse<EmployeeDetailsDTO>(data, "", true));

    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] EmployeeDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        var isValidJob = await _jobService.isValidJob(dto.JobId);

        if (!isValidJob)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Job ID:{dto.JobId}", false));

        var isValidUser = await _userService.isValidUserAsync(dto.UserId);

        if (!isValidUser)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User ID:{dto.UserId}", false));

        var employee = _mapper.Map<Employee>(dto);

        await _employeeService.Add(employee);

        var data = _mapper.Map<EmployeeDetailsDTO>(employee);

        return Ok(new ApiResponse<EmployeeDetailsDTO>(data, "", true));
    }

    [HttpPut]
    [Route("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var isValidJob = await _jobService.isValidJob(dto.JobId);

        if (!isValidJob)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Job Id:{dto.JobId}", false));

        var isValidUser = await _userService.isValidUserAsync(dto.UserId);

        if (!isValidUser)
            return BadRequest(new ApiResponse<object>(null, $"Invalid User ID:{dto.UserId}", false));

        var employee = await _employeeService.GetById(id);

        if (employee == null)
            return NotFound($"Employee with id {id} is not found!");

        employee.Salary = dto.Salary;
        employee.JobId = dto.JobId;
        employee.UserId = dto.UserId;

        _employeeService.Update(employee);

        var data = _mapper.Map<EmployeeDetailsDTO>(employee);

        return Ok(new ApiResponse<EmployeeDetailsDTO>(data, "", true));
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(ModelState,"", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid Id:{id}", false));

        var employee = await _employeeService.GetById(id);

        if (employee == null)
            return NotFound($"Employee with ID ({id}) is not found!");

        _employeeService.Delete(employee);

        var data = _mapper.Map<EmployeeDetailsDTO>(employee);

        return Ok(new ApiResponse<EmployeeDetailsDTO>(data, "", true));
    }
}
