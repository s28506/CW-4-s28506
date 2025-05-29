using ApbdApp.Exceptions;
using ApbdApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApbdApp.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        try
        {
            var patientDetails = await _patientService.GetPatientDetailsAsync(id);
            return Ok(patientDetails);
        }
        catch (PatientNotFoundException exception)
        {
            return NotFound();
        }
    }
}

