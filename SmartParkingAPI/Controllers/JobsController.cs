using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartParking.API.Controllers
{
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

            var job = await _jobService.GetById(id);

            if (job == null)
                return NotFound($"Job with id {id} is not found!");

            var data = _mapper.Map<JobDetailsDTO>(job);

            return Ok(data);
        }

        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] JobDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var job = _mapper.Map<Job>(dto);

            await _jobService.Add(job);

            var data = _mapper.Map<JobDetailsDTO>(job);

            return Ok(data);
        }

        [HttpPut]
        [Route("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] JobDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id < 1)
                return BadRequest($"Invalid ID: {id}");

            var job = await _jobService.GetById(id);

            if (job == null)
                return NotFound($"Job with id {id} is not found!");

            job.JobName = dto.JobName;

            _jobService.Update(job);

            var data = _mapper.Map<JobDetailsDTO>(job);

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

            var job = await _jobService.GetById(id);

            if (job == null)
                return NotFound($"Job with ID {id} is not found!");

            _jobService.Delete(job);

            var data = _mapper.Map<JobDetailsDTO>(job);

            return Ok(data);
        }
    }
}
