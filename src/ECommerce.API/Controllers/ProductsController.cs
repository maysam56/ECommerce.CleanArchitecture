using ECommerce.Application.Features.Product.DTOs;
using ECommerece.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Get all Product

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductQuery());
        return Ok(products);
    }
  
    //Get Product By Id 

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null) 
            return NotFound($"Product with ID {id} not found.");
            
        return Ok(product);
    }

    //Create Product

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] CreateProductDto dto)
    {
        var product = await _mediator.Send(new AddProductCommand(dto));
       return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    // Update Product

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        
        await _mediator.Send(new UpdateProductCommand(id , product)); 
        return NoContent();
    }

    //Delete Product

    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}
