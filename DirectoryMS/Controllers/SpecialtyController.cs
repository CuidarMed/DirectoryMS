using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.Specialties;

namespace DirectoryMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpecialtyController : ControllerBase
    {
        private readonly ICreateSpecialtyService _createSpecialtyService;
        private readonly ISearchSpecialtyService _searchSpecialtyService;
        private readonly IDeleteSpecialtyService _deleteSpecialtyService;

        public SpecialtyController(
            ICreateSpecialtyService createSpecialtyService,
            ISearchSpecialtyService searchSpecialtyService,
            IDeleteSpecialtyService deleteSpecialtyService)
        {
            _createSpecialtyService = createSpecialtyService;
            _searchSpecialtyService = searchSpecialtyService;
            _deleteSpecialtyService = deleteSpecialtyService;
        }

        /// <summary>
        /// Obtiene todas las especialidades activas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllSpecialties()
        {
            try
            {
                var result = await _searchSpecialtyService.GetAllSpecialtiesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una especialidad por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialtyResponse>> GetSpecialtyById(long id)
        {
            try
            {
                var result = await _searchSpecialtyService.GetSpecialtyByIdAsync(id);
                if (result == null)
                    return NotFound(new { message = "Especialidad no encontrada" });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Crea una nueva especialidad
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSpecialty([FromBody] CreateSpecialtyRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                    return BadRequest(new { message = "El nombre de la especialidad es requerido" });

                var result = await _createSpecialtyService.CreateSpecialtyAsync(request);
                return CreatedAtAction(nameof(GetSpecialtyById), new { id = result.SpecialtyId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina (desactiva) una especialidad
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialty(long id)
        {
            try
            {
                var result = await _deleteSpecialtyService.DeleteSpecialtyAsync(id);
                if (!result)
                    return NotFound(new { message = "Especialidad no encontrada" });
                return Ok(new { message = "Especialidad eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}




