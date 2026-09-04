using ECommerece.Domain.Entities;
using MediatR;
public record GeOrdertByIdQuery(int id) : IRequest<Order?>;