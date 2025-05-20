using Microsoft.AspNetCore.Mvc;
using Tutorial5.Services;

namespace Tutorial5.Controllers;

[Route("api/Prescription")]
[ApiController]
public class PrescriptionController :ControllerBase
{
    private readonly IDbService _dbService;
    public PrescriptionController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("")]
    public async Task<IActionResult> xd()
    {
        return Ok();
    }
    [HttpGet("getPatientData/{patientId}")]

    public async Task<IActionResult> GetPatientData(int patientId)
    {
        var books = await _dbService.GetPatientData(patientId);
        return Ok(books);
    }
}