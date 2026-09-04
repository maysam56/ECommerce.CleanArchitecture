using ECommerece.Domain.Entities;
using MediatR;
public record GetCustomerByIdQuery(int CustomerId) : IRequest<Customer?>;
