using ECommerce.Application.Features.Product.DTOs;
using ECommerece.Domain.Entities;
using MediatR;
public record AddProductCommand(CreateProductDto productDto) : IRequest<Product>;