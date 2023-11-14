using Domain.OrderAggregate;
using Domain.PrescriptionAggregate;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Presentation.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ApplicationContext _applicationContext;

    public OrderController(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetPrescriptionsAsync()
    {
        return Ok(await _applicationContext.Orders.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrescriptionAsync(Guid id)
    {
        return Ok(await _applicationContext.Orders.FirstOrDefaultAsync(x => x.Id == id));
    }

    [HttpPost("addOrder")]
    public async Task<IActionResult> AddOrderAsync(Paymentmethod paymentMethod, string street, string region, string house, string city)
    {
        var order = new Order().UpdatePaymentMethod(paymentMethod).UpdateAddress(new Address()
        {
            Street = street,
            Region = region,
            City = city,
            House = house
        });

        _applicationContext.Orders.Add(order);
        await _applicationContext.SaveChangesAsync();
        return Ok(order);
    }

    [HttpPut("updateOrderPaymentMethod/{id}")]
    public async Task<IActionResult> UpdateOrderPaymentMethodAsync([FromBody] Paymentmethod paymentMethod, Guid id)
    {
        var order = _applicationContext.Orders.FirstOrDefault(x => x.Id == id);
        if (order is null)
            return BadRequest();

        order.UpdatePaymentMethod(paymentMethod);
        await _applicationContext.SaveChangesAsync();
        return Ok(order);
    }

    [HttpPut("updateOrderStatus/{id}")]
    public async Task<IActionResult> UpdateOrderStatusAsync([FromBody] Status status, Guid id)
    {
        var order = _applicationContext.Orders.FirstOrDefault(x => x.Id == id);
        if (order is null)
            return BadRequest();

        order.UpdateStatus(status);
        await _applicationContext.SaveChangesAsync();
        return Ok(order);
    }

    [HttpDelete("deleteOrder/{id}")]
    public async Task<IActionResult> DeleteOrderAsync(Guid id)
    {
        var order = _applicationContext.Prescriptions.FirstOrDefault(x => x.Id == id);
        if (order is null)
            return BadRequest();

        _applicationContext.Prescriptions.Remove(order);
        await _applicationContext.SaveChangesAsync();
        return Ok(order);
    }
}
