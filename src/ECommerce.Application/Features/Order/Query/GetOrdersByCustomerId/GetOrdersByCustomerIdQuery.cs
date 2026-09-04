using ECommerece.Domain.Entities;
using MediatR;
public record GetOrdersByCustomerIdQuery(int id) : IRequest<IReadOnlyList<Order>>;
