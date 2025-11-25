using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.Doctors;

namespace DirectoryMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly ICreateDoctorService _createDoctorService;
        private readonly ISearchDoctorService _searchDoctorService;
        private readonly IUpdateDoctorService _updateDoctorService;

        public DoctorController(
            ICreateDoctorService createDoctorService,
            ISearchDoctorService searchDoctorService,
            IUpdateDoctorService updateDoctorService)
        {
            _createDoctorService = createDoctorService;
            _searchDoctorService = searchDoctorService;
            _updateDoctorService = updateDoctorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            var result = await _createDoctorService.CreateDoctorAsync(request);
            return CreatedAtAction(nameof(GetDoctorById), new { id = result.DoctorId }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var result = await _searchDoctorService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResponse>> GetDoctorById(long id)
        {
            var result = await _searchDoctorService.GetByIdAsync(id);
            return Ok(result);   
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<DoctorResponse>> GetDoctorByUserId(long userId)
        {
            var result = await _searchDoctorService.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<DoctorResponse>> UpdateDoctor(long id, [FromBody] UpdateDoctorRequest request)
        {
            var result = await _updateDoctorService.UpdateDoctorAsync(id, request);
            return Ok(result);
        }
    }
}
