﻿using ApbdApp.DTOs;
using ApbdApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace ApbdApp.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequest request)
    {
        try
        {
            await _service.AddPrescriptionAsync(request);
            return Ok("Prescription added.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
