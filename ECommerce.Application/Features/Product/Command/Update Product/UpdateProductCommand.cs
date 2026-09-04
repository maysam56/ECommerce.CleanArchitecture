using ECommerece.Domain.Entities;
using MediatR;
public record UpdateProductCommand(int id, Product product) : IRequest;