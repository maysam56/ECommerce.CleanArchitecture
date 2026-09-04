using ECommerce.Application.Features.Customer.DTOs;
using ECommerce.Application.Interfaces.IServices;
using ECommerece.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{

    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

   

    // Get Customer By Id

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        var customer = await _mediator.Send(new GetCustomerByIdQuery(id));

        if (customer == null) 
            return NotFound($"Customer with ID {id} not found.");

        return Ok(customer);
    }
    //-------------------------------------------------------------

    // Create Customer

    [HttpPost]
    public async Task<ActionResult<Customer>> Create([FromBody] CreateCustomerDto dto)
    {
       var customer = await _mediator.Send(new RegisterCustomerCommand(dto)); 

        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }
    //----------------------------------------------
    // Upgrade To Vip

    [HttpPost("{id}/upgrade-vip")]
    public async Task<IActionResult> UpgradeToVip(int id)
    {

        await _mediator.Send(new UpgradeToVidCommand(id));

        return Ok(new { message = "Customer upgraded to VIP successfully." });
    }

}
