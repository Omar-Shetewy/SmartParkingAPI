namespace SmartParking.API.Controllers;

[Route("api/Jobs")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly IMapper _mapper;

    public JobsController(IJobService jobService, IMapper mapper)
    {
        _jobService = jobService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAsync()
    {
        var jobs = await _jobService.GetAll();

        if (jobs.Count() == 0)
            return NoContent();

        var data = _mapper.Map<List<JobDetailsDTO>>(jobs);

        return Ok(new ApiResponse<List<JobDetailsDTO>>(data, "Success", true));
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

        var job = await _jobService.GetById(id);

        if (job == null)
            return NotFound(new ApiResponse<object>(null, $"Job with id {id} is not found!", false));

        var data = _mapper.Map<JobDetailsDTO>(job);

        return Ok(new ApiResponse<JobDetailsDTO>(data, "Success", true));
    }

    [HttpPost]
    [Route("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] JobDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(null,"Is Invalid", false));

        var job = _mapper.Map<Job>(dto);

        await _jobService.Add(job);

        var data = _mapper.Map<JobDetailsDTO>(job);

        return Ok(new ApiResponse<JobDetailsDTO>(data, "Success", true));
    }

    [HttpPut]
    [Route("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] JobDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(null,"Is Invalid", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID: {id}", false));

        var job = await _jobService.GetById(id);

        if (job == null)
            return NotFound(new ApiResponse<object>(null, $"Job with id {id} is not found!", false));

        job.JobName = dto.JobName;

        _jobService.Update(job);

        var data = _mapper.Map<JobDetailsDTO>(job);

        return Ok(new ApiResponse<JobDetailsDTO>(data, "Success", true));
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<object>(null,"Invalid", false));

        if (id < 1)
            return BadRequest(new ApiResponse<object>(null, $"Invalid ID: {id}", false));

        var job = await _jobService.GetById(id);

        if (job == null)
            return NotFound(new ApiResponse<object>(null, $"Job with ID {id} is not found!", false));

        _jobService.Delete(job);

        var data = _mapper.Map<JobDetailsDTO>(job);

        return Ok(new ApiResponse<JobDetailsDTO>(data, "Success", true));
    }
}
