using Domain.CourierAggregate;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Route("api/courier")]
[ApiController]
public class CourierController : ControllerBase
{
    private readonly ApplicationContext _applicationContext;

    public CourierController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    [HttpGet("couriers")]
    public async Task<IActionResult> GetCouriersAsync()
    {
        return Ok(await _applicationContext.Couriers.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourierAsync(Guid id)
    {
        return Ok(await _applicationContext.Couriers.FirstOrDefaultAsync(x => x.Id == id));
    }

    [HttpPost("addCourier")]
    public async Task<IActionResult> AddCourierAsync(string name)
    {
        var courier = new Courier().UpdateName(name);
        if (courier is null)
            return BadRequest();

        _applicationContext.Couriers.Add(courier);
        await _applicationContext.SaveChangesAsync();
        return Ok(courier);
    }

    [HttpPut("updateCourierName/{id}")]
    public async Task<IActionResult> UpdateCourierNameAsync([FromBody] string name, Guid id)
    {
        var courier = _applicationContext.Couriers.FirstOrDefault(x => x.Id == id);
        if (courier is null)
            return BadRequest();

        courier.UpdateName(name);
        await _applicationContext.SaveChangesAsync();
        return Ok(courier);
    }

    [HttpDelete("deleteCourier/{id}")]
    public async Task<IActionResult> DeleteCourierAsync(Guid id)
    {
        var courier = _applicationContext.Couriers.FirstOrDefault(x => x.Id == id);
        if (courier is null)
            return BadRequest();

        _applicationContext.Couriers.Remove(courier);
        await _applicationContext.SaveChangesAsync();
        return Ok(courier);
    }
}
