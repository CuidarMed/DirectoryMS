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

        /// <summary>
        /// Crea un nuevo doctor
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            try
            {
                var result = await _createDoctorService.CreateDoctorAsync(request);
                return CreatedAtAction(nameof(GetDoctorById), new { id = result.DoctorId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene todos los doctores
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var result = await _searchDoctorService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene un doctor por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResponse>> GetDoctorById(long id)
        {
            try
            {
                var result = await _searchDoctorService.GetByIdAsync(id);

                if (result == null)
                    return NotFound(new { message = "Doctor no encontrado" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un doctor existente (actualización parcial)
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<DoctorResponse>> UpdateDoctor(long id, [FromBody] UpdateDoctorRequest request)
        {
            try
            {
                var result = await _updateDoctorService.UpdateDoctorAsync(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
