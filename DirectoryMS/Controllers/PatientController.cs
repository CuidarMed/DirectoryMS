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
            var result = await _createPatientService.createaPatient(patient);
            return new JsonResult(result);
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
