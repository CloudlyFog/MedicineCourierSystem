using Domain.MedicineAggregate;
using Domain.OrderAggregate;
using Domain.PrescriptionAggregate;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Route("api/medicine")]
[ApiController]
public class MedicineController : ControllerBase
{
    private readonly ApplicationContext _applicationContext;

    public MedicineController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    [HttpGet("medicines")]
    public async Task<IActionResult> GetPrescriptionsAsync()
    {
        return Ok(await _applicationContext.Medicines.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrescriptionAsync(Guid id)
    {
        return Ok(await _applicationContext.Medicines.FirstOrDefaultAsync(x => x.Id == id));
    }

    [HttpPost("addMedicine")]
    public async Task<IActionResult> AddMedicineAsync(string name, string description, double price)
    {
        var medicine = new Medicine().UpdateName(name).UpdateDescription(description).UpdatePrice(price);

        _applicationContext.Medicines.Add(medicine);
        await _applicationContext.SaveChangesAsync();
        return Ok(medicine);
    }

    [HttpPut("updateMedicineName/{id}")]
    public async Task<IActionResult> UpdateMedicineNameAsync([FromBody] string name, Guid id)
    {
        var medicine = _applicationContext.Medicines.FirstOrDefault(x => x.Id == id);
        if (medicine is null)
            return BadRequest();

        medicine.UpdateName(name);
        await _applicationContext.SaveChangesAsync();
        return Ok(medicine);
    }

    [HttpPut("updateMedicinePrice/{id}")]
    public async Task<IActionResult> UpdateMedicinePriceAsync([FromBody] double price, Guid id)
    {
        var medicine = _applicationContext.Medicines.FirstOrDefault(x => x.Id == id);
        if (medicine is null)
            return BadRequest();

        medicine.UpdatePrice(price);
        await _applicationContext.SaveChangesAsync();
        return Ok(medicine);
    }

    [HttpPut("updateMedicineDescription/{id}")]
    public async Task<IActionResult> UpdateMedicineDescriptionAsync([FromBody] string description, Guid id)
    {
        var medicine = _applicationContext.Medicines.FirstOrDefault(x => x.Id == id);
        if (medicine is null)
            return BadRequest();

        medicine.UpdateDescription(description);
        await _applicationContext.SaveChangesAsync();
        return Ok(medicine);
    }

    [HttpPut("updateMedicinePrescription/{id}")]
    public async Task<IActionResult> UpdateMedicinePrescriptionAsync([FromBody] Prescription prescription, Guid id)
    {
        var medicine = _applicationContext.Medicines
            .Include(x => x.Order)
            .Include(x => x.Prescription)
            .FirstOrDefault(x => x.Id == id);

        if (medicine is null)
            return BadRequest();

        medicine.UpdatePrescription(prescription);

        await _applicationContext.SaveChangesAsync();
        return Ok(medicine);
    }

    [HttpPut("updateMedicineOrder/{id}")]
    public async Task<IActionResult> UpdateMedicineOrderAsync([FromBody] Order order, Guid id)
    {
        var medicine = _applicationContext.Medicines
            .Include(x => x.Order)
            .Include(x => x.Prescription)
            .FirstOrDefault(x => x.Id == id);

        if (medicine is null)
            return BadRequest();

        medicine.UpdateOrder(order);

        await _applicationContext.SaveChangesAsync();
        return Ok(medicine);
    }

    [HttpDelete("deleteMedicine/{id}")]
    public async Task<IActionResult> DeleteMedicineAsync(Guid id)
    {
        var medicine = _applicationContext.Prescriptions.FirstOrDefault(x => x.Id == id);
        if (medicine is null)
            return BadRequest();

        _applicationContext.Prescriptions.Remove(medicine);
        await _applicationContext.SaveChangesAsync();
        return Ok(medicine);
    }
}
