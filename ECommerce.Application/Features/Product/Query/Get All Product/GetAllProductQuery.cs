using ECommerece.Domain.Entities;
using MediatR;

public record GetAllProductQuery() : IRequest<IReadOnlyList<Product>>;