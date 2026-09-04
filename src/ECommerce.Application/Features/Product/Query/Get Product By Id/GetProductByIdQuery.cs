using ECommerece.Domain.Entities;
using MediatR;

public record GetProductByIdQuery(int id) : IRequest<Product?>;
