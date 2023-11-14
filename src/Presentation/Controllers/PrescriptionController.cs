using Domain.PharmacistAggregate;
using Domain.PrescriptionAggregate;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Route("api/prescription")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly ApplicationContext _applicationContext;

    public PrescriptionController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    [HttpGet("prescriptions")]
    public async Task<IActionResult> GetPrescriptionsAsync()
    {
        return Ok(await _applicationContext.Prescriptions.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrescriptionAsync(Guid id)
    {
        return Ok(await _applicationContext.Prescriptions.FirstOrDefaultAsync(x => x.Id == id));
    }

    [HttpPost("addPrescription")]
    public async Task<IActionResult> AddPrescriptionAsync(string snils, DateTime dateBegin, DateTime dateEnd)
    {
        var prescription = new Prescription().UpdateSnils(snils).UpdateDate(dateBegin, dateEnd);
        if (prescription is null)
            return BadRequest();

        _applicationContext.Prescriptions.Add(prescription);
        await _applicationContext.SaveChangesAsync();
        return Ok(prescription);
    }

    [HttpPut("updatePrescriptionPharmacist/{id}")]
    public async Task<IActionResult> UpdatePrescriptionAsync([FromBody] Pharmacist pharmacist, Guid id)
    {
        var prescription = _applicationContext.Prescriptions.Include(x => x.Pharmacist).FirstOrDefault(x => x.Id == id);
        if (prescription is null)
            return BadRequest();

        prescription.UpdatePharmacist(pharmacist);

        await _applicationContext.SaveChangesAsync();
        return Ok(prescription);
    }

    [HttpPut("updatePrescriptionSnils/{id}")]
    public async Task<IActionResult> UpdatePrescriptionSnilsAsync([FromBody] string snils, Guid id)
    {
        var prescription = _applicationContext.Prescriptions.FirstOrDefault(x => x.Id == id);
        if (prescription is null)
            return BadRequest();

        prescription.UpdateSnils(snils);
        await _applicationContext.SaveChangesAsync();
        return Ok(prescription);
    }

    [HttpDelete("deletePrescription")]
    public async Task<IActionResult> DeletePrescriptionAsync(Guid id)
    {
        var prescription = _applicationContext.Prescriptions.FirstOrDefault(x => x.Id == id);
        if (prescription is null)
            return BadRequest();

        _applicationContext.Prescriptions.Remove(prescription);
        await _applicationContext.SaveChangesAsync();
        return Ok(prescription);
    }
}
