using ECommerce.Application.Features.Order.DTOs;
using ECommerce.Application.Interfaces.IRepository;
using ECommerce.Application.Interfaces.IServices;
using ECommerece.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //---------------------------------------------------------


    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {

        var order = await _mediator.Send(new GeOrdertByIdQuery(id));
        if (order == null) return NotFound();
        return Ok(order);
    }
    //---------------------------------------------------------
    // Get Customer Orders

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<Order>>> GetCustomerOrders(int customerId)
    {

        var orders = await _mediator.Send(new GetOrdersByCustomerIdQuery(customerId));
        return Ok(orders);
    }

    //---------------------------------------------------------

    // Cancel Order

    [HttpPost("cancel/{id}")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        await _mediator.Send(new CancelOrderCommand(id));

        return Ok(new
        {
            message = "Order cancelled successfully"
        });
    }
    //---------------------------------------------------------

       [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CreateOrderDto request)
    {
        var result = await _mediator.Send(new CheckoutOrderCommand(request));

        return Ok(result);
    }
    




}
