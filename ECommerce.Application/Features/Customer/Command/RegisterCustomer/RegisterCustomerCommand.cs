using ECommerce.Application.Features.Customer.DTOs;
using ECommerece.Domain.Entities;
using MediatR;

public record RegisterCustomerCommand(CreateCustomerDto Dto) : IRequest<Customer>;

