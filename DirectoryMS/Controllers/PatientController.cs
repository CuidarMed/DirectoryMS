using Application.DTOs.Patients;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryMS.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ICreatePatientService _createPatientService;
        private readonly ISearchPatientService _searchPatientService;
        private readonly IUpdatePatientService _updatePatientService;

        public PatientController(ICreatePatientService createPatientService, ISearchPatientService searchPatientService, IUpdatePatientService updatePatientService)
        {
            _createPatientService = createPatientService;
            _searchPatientService = searchPatientService;
            _updatePatientService = updatePatientService;
        }

        [HttpPost]
        public async Task<IActionResult> createPatient([FromBody] CreatePatientRequest patient)
        {
            try
            {
                if (patient == null)
                {
                    return BadRequest(new { message = "El paciente no puede ser nulo." });
                }

                var result = await _createPatientService.createaPatient(patient);
                return Ok(result);
            }
            catch (Application.Exceptions.ConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al crear el paciente: {ex.Message}", details = ex.ToString() });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllPatients()
        {
            try
            {
                var result = await _searchPatientService.getAllPatients();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener los pacientes: {ex.Message}", details = ex.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getPacientById(long id)
        {
            var result = await _searchPatientService.getPatientById(id);
            return new JsonResult(result);
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> getPatientByUserId(long userId)
        {
            var result = await _searchPatientService.getPatientByUserId(userId);
            return new JsonResult(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> updatePatient(long id, [FromBody] UpdatePatientRequest request)
        {
            var result = await _updatePatientService.UpdatePatient(id, request);
            return new JsonResult(result);
        }
    }
}
