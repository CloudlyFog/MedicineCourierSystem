using Domain.PharmacistAggregate;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Route("api/pharmacist")]
[ApiController]
public class PharmacistController : ControllerBase
{
    private readonly ApplicationContext _applicationContext;

    public PharmacistController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    [HttpGet("pharmacists")]
    public async Task<IActionResult> GetPrescriptionsAsync()
    {
        return Ok(await _applicationContext.Pharmacists.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrescriptionAsync(Guid id)
    {
        return Ok(await _applicationContext.Pharmacists.FirstOrDefaultAsync(x => x.Id == id));
    }

    [HttpPost("addPharmacist")]
    public async Task<IActionResult> AddPharmacistAsync(string name)
    {
        var pharmacist = new Pharmacist().UpdateName(name);
        if (pharmacist is null)
            return BadRequest();

        _applicationContext.Pharmacists.Add(pharmacist);
        await _applicationContext.SaveChangesAsync();
        return Ok(pharmacist);
    }

    [HttpPut("updatePharmacistName/{id}")]
    public async Task<IActionResult> UpdatePharmacistNameAsync([FromBody] string name, Guid id)
    {
        var pharmacist = _applicationContext.Pharmacists.FirstOrDefault(x => x.Id == id);
        if (pharmacist is null)
            return BadRequest();

        pharmacist.UpdateName(name);
        await _applicationContext.SaveChangesAsync();
        return Ok(pharmacist);
    }

    [HttpDelete("deletePharmacist/{id}")]
    public async Task<IActionResult> DeletePharmacistAsync(Guid id)
    {
        var pharmacist = _applicationContext.Prescriptions.FirstOrDefault(x => x.Id == id);
        if (pharmacist is null)
            return BadRequest();

        _applicationContext.Prescriptions.Remove(pharmacist);
        await _applicationContext.SaveChangesAsync();
        return Ok(pharmacist);
    }
}
